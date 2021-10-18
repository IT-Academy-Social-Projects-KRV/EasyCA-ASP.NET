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
    public class EuroProtocolUnitTest
    {
        private readonly Mock<IGenericRepository<EuroProtocol>> _euroProtocols = new Mock<IGenericRepository<EuroProtocol>>();
        private readonly Mock<IGenericRepository<Circumstance>> _circumstances = new Mock<IGenericRepository<Circumstance>>();
        private readonly Mock<IGenericRepository<Transport>> _transport = new Mock<IGenericRepository<Transport>>();
        private readonly Mock<IGenericRepository<PersonalData>> _personalData = new Mock<IGenericRepository<PersonalData>>();
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly EuroProtocolService _euroProtocolService;
        private readonly EuroProtocol _euroProtocolSuccess = new EuroProtocol() { SerialNumber = "0112345", SideA = new Side() { Email = "kosmin@gmail.com" } };

        public EuroProtocolUnitTest()
        {
            _userManager = GetMockUserManager();
            _euroProtocolService = new EuroProtocolService(_mapper.Object, _euroProtocols.Object,
                _circumstances.Object, _transport.Object, _personalData.Object, _userManager.Object);
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        private EuroProtocolRequestApiModel EuroModel()
        {
            EuroProtocolRequestApiModel euroModel = new EuroProtocolRequestApiModel()
            {
                RegistrationDateTime = DateTime.Now,
                SerialNumber = "0012345",
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
                SideA = new SideRequestApiModel()
                {
                    Email = "stets@gmail.com",
                    TransportId = "614b081a3d312f200439de85",
                    Circumstances = new List<int>() { 0 },
                    Evidences = new List<Evidence>()
                    {
                        new Evidence()
                        {
                            Explanation = "Vinovat, durak, ispravlius",
                            PhotoSchema = "Photoschema.jpg",
                            Attachments = new List<string>()
                            {
                                "Attachments.doc"
                            }
                        }
                    },
                    DriverLicenseSerial = "ПВК128894",
                    Damage = "damaged left front door",
                    IsGulty = true,
                    ProtocolSerial = "0012345"
                },
                SideB = new SideRequestApiModel()
                {
                    Email = "kosmin@gmail.com",
                    TransportId = "612900355b12fb0159889153",
                    Circumstances = new List<int>() { 2 },
                    Evidences = new List<Evidence>()
                    {
                        new Evidence()
                        {
                            Explanation = "Tsey Olen naihav meni na ruku",
                            PhotoSchema = "Photoschema1.jpg",
                            Attachments = new List<string>()
                            {
                                "Attachments1.doc"
                            }
                        }
                    },
                    DriverLicenseSerial = "ПSК138826",
                    Damage = "broken arm",
                    IsGulty = false,
                    ProtocolSerial = "0012345"
                },
                IsClosed = false,
                Witnesses = new List<WitnessRequestApiModel>()
                {
                    new WitnessRequestApiModel()
                    {
                        FirstName = "Dmytro",
                        LastName = "Pinkevich",
                        WitnessAddress = "over the hiils and faraway",
                        PhoneNumber = "+380674445577",
                        IsVictim = false
                    }
                }
            };
            return euroModel;
        }
        
        private SideRequestApiModel SideB()
        {
            SideRequestApiModel sideB = new SideRequestApiModel()
            {
                Email = "kosmin@gmail.com",
                TransportId = "612900355b12fb0159889153",
                Circumstances = new List<int>() { 2 },
                Evidences = new List<Evidence>()
                    {
                        new Evidence()
                        {
                            Explanation = "Tsey Olen naihav meni na ruku",
                            PhotoSchema = "Photoschema1.jpg",
                            Attachments = new List<string>()
                            {
                                "Attachments1.doc"
                            }
                        }
                    },
                DriverLicenseSerial = "ПSК138826",
                Damage = "broken arm",
                IsGulty = false,
                ProtocolSerial = "0012345"
            };
            return sideB;

        }

        [Fact]
        public async Task RegistrationEuroProtocol_Success()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestApiModel>())).Returns(new EuroProtocol());

            //Act
            var result = await _euroProtocolService.RegistrationEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_SideBDoesntExists_ReturnsNotFound()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<Side>(It.IsAny<Expression<Func<SideRequestApiModel, bool>>>())).Returns(new Side() { IsGulty = false });
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);
            _euroProtocols.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>(), It.IsAny<UpdateDefinition<EuroProtocol>>())).ReturnsAsync(mockResult.Object);
            
            //Act
            Func<Task> act = () => _euroProtocolService.RegisterSideBEuroProtocol(It.IsAny<SideRequestApiModel>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_ReturnSuccess()
        {
            //Arrange
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);
            _mapper.Setup(repo => repo.Map<Side>(It.IsAny<Expression<Func<SideRequestApiModel, bool>>>())).Returns(new Side() { IsGulty = false });
            _euroProtocols.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>(), It.IsAny<UpdateDefinition<EuroProtocol>>())).ReturnsAsync(mockResult.Object);
            
            //Act
            var result = await _euroProtocolService.RegisterSideBEuroProtocol(SideB());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task FindAllEuroProtocolsByEmail_EmailNotFound_ReturnsBadRequest()
        {
            //Arrange
            _euroProtocols.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>())).ReturnsAsync((List<EuroProtocol>)null);

            //Act
            Func<Task> act = () => _euroProtocolService.FindAllEuroProtocolsByEmail(It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }
        
        [Fact]
        public async Task FindAllEuroProtocolsByEmail_ReturnSuccess()
        {
            //Arrange
            var protocol = new EuroProtocol()
            {
                SerialNumber = "0012345",
                SideA = new Side() { Email = "kosmin@gmail.com" }
            };
            _euroProtocols.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>())).ReturnsAsync(new List<EuroProtocol>());
            _mapper.Setup(s => s.Map<List<EuroProtocolSimpleResponseApiModel>>(protocol)).Returns(new List<EuroProtocolSimpleResponseApiModel>()
            {
                new EuroProtocolSimpleResponseApiModel ()
                {
                    SerialNumber = protocol.SerialNumber,
                    Address = new AddressOfAccident() {City = "Rivne" }
                }
            });

            //Act
            var result = await _euroProtocolService.FindAllEuroProtocolsByEmail(_euroProtocolSuccess.SideA.Email);

            //Assert
            Assert.IsType<List<EuroProtocolSimpleResponseApiModel>>(result.ToList());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateEuroProtocol_UpdateReturnBadRequest()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestApiModel>())).Returns(new EuroProtocol()
            {
                IsClosed = false,
                SideA = new Side() { IsGulty = true, Circumstances = new List<int>(), Evidences = new List<Evidence>() },
                SideB = new Side() { IsGulty = true, Circumstances = new List<int>(), Evidences = new List<Evidence>() },
                Address = new AddressOfAccident() { IsInCity = true },
                Witnesses = new List<Witness>(),
                RegistrationDateTime = new DateTime(),
                SerialNumber = "",
                Id = ""
            });
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);
            _euroProtocols.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>(), It.IsAny<UpdateDefinition<EuroProtocol>>())).ReturnsAsync(mockResult.Object);

            //Act
            Func<Task> act = () => _euroProtocolService.UpdateEuroProtocol(EuroModel());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task UpdateEuroProtocol_ReturnsSuccess()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestApiModel>()))
                        .Returns(new EuroProtocol()
                        {
                            IsClosed = false,
                            SideA = new Side() { IsGulty = true, Circumstances = new List<int>(), Evidences = new List<Evidence>() },
                            SideB = new Side() { IsGulty = true, Circumstances = new List<int>(), Evidences = new List<Evidence>() },
                            Address = new AddressOfAccident() { IsInCity = true },
                            Witnesses = new List<Witness>(),
                            RegistrationDateTime = new DateTime(),
                            SerialNumber = "",
                            Id = ""
                        });
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);

            _euroProtocols.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>(), It.IsAny<UpdateDefinition<EuroProtocol>>())).ReturnsAsync(mockResult.Object);
            
            //Act
            var result = await _euroProtocolService.UpdateEuroProtocol(EuroModel());
            
            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }
    }
}
