using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Services;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.Services
{
    public class CAServiceUnitTest
    {
        private readonly Mock<IGenericRepository<CarAccident>> _carAccidentList = new Mock<IGenericRepository<CarAccident>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly CarAccidentService _carAccidentService;
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly CarAccident _carAccidentProtocolSuccess = new CarAccident()
        {
            SerialNumber = "DA128846",
            InspectorId = "KH128846",
            RegistrationDateTime = DateTime.Now,
            SideOfAccident = new SideCA()
            { 
                Email = "dyatel@mail.ru",
                DriverLicenseSerial = "PVH138844"
            }
        };
       
        public CAServiceUnitTest()
        {
            _userManager = GetMockUserManager();
            _carAccidentService = new CarAccidentService(_mapper.Object, _carAccidentList.Object);
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        public CarAccidentRequestApiModel CarAccidentProtocol()
        {
            CarAccidentRequestApiModel carAccidentProtocol = new CarAccidentRequestApiModel()
            {
                RegistrationDateTime = DateTime.Now,
                SerialNumber = "AD125588",
                InspectorId = "СФ126644",
                Address = new AddressOfAccidentRequestApiModel()
                {
                    City = "Rivne",
                    District = "Rivnenskiy",
                    Street = "Kyivska, 40",
                    CrossStreet = "Danyla Galits'kogo",
                    CoordinatesOfLatitude = "50.615506",
                    CoordinatesOfLongitude = "26.285078",
                    IsInCity = true,
                    IsIntersection = true
                },
                SideOfAccident = new SideCARequestApiModel()
                {
                    Email = "dolboDyatel@yandex.ru",
                    TransportId = "614b081a3d312f200439de85",
                    DriverLicenseSerial = "ПВК138846",
                },
                AccidentCircumstances = "On 19.10.2021 at 07.35 due to violation of speed regime driver Dolbo Dyatlov hit another car to rear bamper",
                TrafficRuleId = "215.1 para.2b",
                DriverExplanation = "Proshu ponyat i prostit",
                Witnesses = new List<WitnessRequestApiModel>()
                {
                    new WitnessRequestApiModel()
                    {
                        FirstName = "Gromadianyn",
                        LastName = "Zakonosluhnianiy",
                        WitnessAddress = "over the hiils and faraway",
                        PhoneNumber = "+380674445577",
                        IsVictim = false
                    }
                },
                Evidences = new List<EvidenceCARequestApiModel>()
                {
                    new EvidenceCARequestApiModel()
                    {
                        PhotoSchema = "Photoschema1.jpg",
                    }
                },
                CourtDTG = new DateTime(2021, 11, 4, 12, 0,0 ),
                IsDocumentTakenOff = false,
                IsClosed = false
            };

            return carAccidentProtocol;
        }

        [Fact]
        public async Task RegistrationCarAccidentProtocol_ReturnSuccess()
        {
            //Arrange
            _carAccidentList.Setup(repo => repo.GetLastItem(It.IsAny<Predicate<CarAccident>>())).ReturnsAsync(new CarAccident() 
            { 
                SerialNumber = "11111111",
                SideOfAccident = new SideCA(),
                Witnesses = new List<Witness>(),
                Evidences= new List<EvidenceCA>(),
                Address = new AddressOfAccident()
            });
            _mapper.Setup(repo => repo.Map<CarAccident>(It.IsAny<CarAccidentRequestApiModel>())).Returns(new CarAccident());

            //Act
            var result = await _carAccidentService.RegistrationCarAccidentProtocol(CarAccidentProtocol(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RegistrationCarAccidentProtocol_ReturnFailed()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<CarAccident>(null)).Throws(new NullReferenceException());

            //Act
            Func<Task> act = () => _carAccidentService.RegistrationCarAccidentProtocol(null, null);

            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact]
        public async Task FindAllCarAccidentProtocolsByInvolvedId_ReturnSuccess()
        {
            //Arrange
            var carAccident = new CarAccident()
            {
                SerialNumber = "DA148812"
            };
            _carAccidentList.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<CarAccident, bool>>>())).ReturnsAsync(new List<CarAccident>());
            _mapper.Setup(x => x.Map<List<CarAccidentResponseApiModel>>(carAccident)).Returns(new List<CarAccidentResponseApiModel>()
            { 
                new CarAccidentResponseApiModel()
                { 
                    SerialNumber = "DA148812"
                }
            });

            //Act

            var result = await _carAccidentService.FindAllCarAccidentProtocolsByInvolvedId(_carAccidentProtocolSuccess.SerialNumber);

            //Assert
            Assert.IsType<List<CarAccidentResponseApiModel>>(result.ToList());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindAllCarAccidentProtocolsByInvolvedId_ReturnFailed()
        {
            //Arrange
            _carAccidentList.Setup(x => x.GetAllByFilterAsync(It.IsAny<Expression<Func<CarAccident, bool>>>())).ReturnsAsync((List<CarAccident>)null);

            //Act
            Func<Task> act = () => _carAccidentService.FindAllCarAccidentProtocolsByInvolvedId(It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task UpdateCarAccidentProtocol_ReturnSuccess()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<CarAccident>(It.IsAny<CarAccidentRequestApiModel>())).Returns(new CarAccident()
            {
                IsClosed = false,
                IsDocumentTakenOff = false,
                Witnesses = new List<Witness>() { new Witness() { FirstName = "Ivan", LastName = "Shvornikov" } },
                Evidences = new List<EvidenceCA>() { new EvidenceCA() { PhotoSchema = "Shema.jpg" } },
                CourtDTG = new DateTime()
            });
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);
            _carAccidentList.Setup(x => x.UpdateAsync(It.IsAny<Expression<Func<CarAccident, bool>>>(), It.IsAny<UpdateDefinition<CarAccident>>())).ReturnsAsync(mockResult.Object);

            //Act
            var result = await _carAccidentService.UpdateCarAccidentProtocol(CarAccidentProtocol());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateCarAccidentProtocol_ReturnFailed()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<CarAccident>(It.IsAny<CarAccidentRequestApiModel>())).Returns(new CarAccident()
            {
                IsClosed = false,
                IsDocumentTakenOff = false,
                Witnesses = new List<Witness>() { new Witness() { FirstName = "Ivan", LastName = "Shvornikov" } },
                Evidences = new List<EvidenceCA>() { new EvidenceCA() { PhotoSchema = "Shema.jpg" } },
                CourtDTG = new DateTime()
            });
            var mockResult = new Mock<UpdateResult>();
            _carAccidentList.Setup(x => x.UpdateAsync(It.IsAny<Expression<Func<CarAccident, bool>>>(), It.IsAny<UpdateDefinition<CarAccident>>())).ReturnsAsync(mockResult.Object);

            //Act
            Func<Task> act = () => _carAccidentService.UpdateCarAccidentProtocol(CarAccidentProtocol());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

    }
}
