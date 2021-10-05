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
using MongoDB.Driver;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.Services
{
    public class EuroProtocolUnitTest
    {
        private readonly Mock<IGenericRepository<EuroProtocol>> _euroProtocols = new Mock<IGenericRepository<EuroProtocol>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly EuroProtocolService _euroProtocolService;
        private readonly EuroProtocol _euroProtocolSuccess = new EuroProtocol() { SerialNumber = "0112345", SideA = new Side() { Email = "kosmin@gmail.com" } };

        public EuroProtocolUnitTest()
        {
            _euroProtocolService = new EuroProtocolService(_mapper.Object, _euroProtocols.Object);
        }

        private EuroProtocolRequestModel EuroModel()
        {
            EuroProtocolRequestModel euroModel = new EuroProtocolRequestModel()
            {
                RegistrationDateTime = DateTime.Now,
                SerialNumber = "0012345",
                Address = new AddressOfAccidentRequestModel()
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
                SideA = new SideRequestModel()
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
                SideB = new SideRequestModel()
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
                Witnesses = new List<WitnessRequestModel>()
                {
                    new WitnessRequestModel()
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
        
        private SideRequestModel SideB()
        {
            SideRequestModel sideB = new SideRequestModel()
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
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestModel>())).Returns(new EuroProtocol());

            //Act
            var result = await _euroProtocolService.RegistrationEuroProtocol(It.IsAny<EuroProtocolRequestModel>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_SideBDoesntExists_ReturnsNotFound()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<Side>(It.IsAny<Expression<Func<SideRequestModel, bool>>>())).Returns(new Side() { IsGulty = false });
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);
            _euroProtocols.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<EuroProtocol, bool>>>(), It.IsAny<UpdateDefinition<EuroProtocol>>())).ReturnsAsync(mockResult.Object);
            
            //Act
            Func<Task> act = () => _euroProtocolService.RegisterSideBEuroProtocol(It.IsAny<SideRequestModel>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_ReturnSuccess()
        {
            //Arrange
            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);
            _mapper.Setup(repo => repo.Map<Side>(It.IsAny<Expression<Func<SideRequestModel, bool>>>())).Returns(new Side() { IsGulty = false });
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
            _mapper.Setup(s => s.Map<List<EuroProtocolResponseModel>>(protocol)).Returns(new List<EuroProtocolResponseModel>()
            {
                new EuroProtocolResponseModel ()
                {
                    SerialNumber = protocol.SerialNumber,
                    SideA = new SideResponseModel() {Email = protocol.SideA.Email}
                }
            });

            //Act
            var result = await _euroProtocolService.FindAllEuroProtocolsByEmail(_euroProtocolSuccess.SideA.Email);

            //Assert
            Assert.IsType<List<EuroProtocolResponseModel>>(result.ToList());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateEuroProtocol_UpdateReturnBadRequest()
        {
            //Arrange
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestModel>())).Returns(new EuroProtocol()
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
            _mapper.Setup(repo => repo.Map<EuroProtocol>(It.IsAny<EuroProtocolRequestModel>()))
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
