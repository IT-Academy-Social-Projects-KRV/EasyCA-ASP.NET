using System;
using System.Web;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class CAServiceControllerUnitTest
    {
        private readonly Mock<ICarAccidentService> _carAccidentService = new Mock<ICarAccidentService>();
        private readonly CarAccidentController _carAccidentController;
                
        public CAServiceControllerUnitTest()
        {
            _carAccidentController = new CarAccidentController(_carAccidentService.Object);
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "anuviswan"),

            }, "mock"));
            
            _carAccidentController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        }

        [Fact]
        public async Task CarAccidentRegistration_ReturnSuccess()
        {
            //Arrange
            _carAccidentService.Setup(repo => repo.RegistrationCarAccidentProtocol(It.IsAny<CarAccidentRequestApiModel>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));
            
            //Act
            var result = await (_carAccidentController.CarAccidentRegistration(It.IsAny<CarAccidentRequestApiModel>())) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CarAccidentRegistration_ReturnFailed()
        {
            //Arrange
            _carAccidentService.Setup(repo => repo.RegistrationCarAccidentProtocol(null, It.IsAny<string>())).Throws(new NullReferenceException());

            //Act
            Func<Task> act = () => _carAccidentController.CarAccidentRegistration(null);

            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact]
        public async Task FindAllCarAccidentProtocolsByInvolvedId_ReturnSuccess()
        {
            //Arrange
            _carAccidentService.Setup(repo => repo.FindAllCarAccidentProtocolsByInvolvedId(It.IsAny<string>())).ReturnsAsync(new List<CarAccidentResponseApiModel>() { });
            
            //Act
            var result = await _carAccidentController.FindAllCarAccidentProtocolsByInvolvedId() as OkObjectResult;
            var resultValue = result.Value as List<CarAccidentResponseApiModel>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<CarAccidentResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task FindAllCarAccidentProtocolsByInvolvedI_ReturnFailed()
        {
            //Arrange
            _carAccidentService.Setup(repo => repo.FindAllCarAccidentProtocolsByInvolvedId(It.IsAny<string>())).ThrowsAsync(new NullReferenceException());

            //Act
            Func<Task> act = () => _carAccidentController.FindAllCarAccidentProtocolsByInvolvedId();

            //Assert
            var result = await Assert.ThrowsAsync<NullReferenceException>(act);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateCarAccidentProtocol_ReturnSuccess()
        { 
            //Arrange
            _carAccidentService.Setup(repo=>repo.UpdateCarAccidentProtocol(It.IsAny<CarAccidentRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await _carAccidentController.UpdateCarAccidentProtocol(It.IsAny<CarAccidentRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task UpdateCarAccidentProtocol_ReturnFailed()
        {
            //Arrange
            _carAccidentService.Setup(repo => repo.UpdateCarAccidentProtocol(It.IsAny<CarAccidentRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => _carAccidentController.UpdateCarAccidentProtocol(It.IsAny<CarAccidentRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
