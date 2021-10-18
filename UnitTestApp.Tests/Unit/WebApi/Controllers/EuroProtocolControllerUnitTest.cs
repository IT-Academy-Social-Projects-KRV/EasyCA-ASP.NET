using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.WebApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class EuroProtocolControllerUnitTest
    {
        private readonly Mock<IEuroProtocolService> _euroProtocolService = new Mock<IEuroProtocolService>();
        private readonly EuroProtocolController euroProtocolController;
        
        public EuroProtocolControllerUnitTest()
        {
            euroProtocolController = new EuroProtocolController(_euroProtocolService.Object);
        }

        [Fact]
        public async Task RegisterEuroProtocol_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.RegistrationEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK,true,"Success"));
           
            //Act
            var result = await euroProtocolController.RegisterEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;
          
            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task RegisterEuroProtocol_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.RegistrationEuroProtocol(null)).ThrowsAsync(new NullReferenceException());
           
            //Act
            Func<Task> act = () =>euroProtocolController.RegisterEuroProtocol(null);
           
            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.RegisterSideBEuroProtocol(It.IsAny<SideRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));
           
            //Act
            var result = await euroProtocolController.RegisterSideBEuroProtocol(It.IsAny<SideRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;
           
            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task RegisterSideBEuroProtocol_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.RegisterSideBEuroProtocol(It.IsAny<SideRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest,"Failed"));

            //Act
            Func<Task> act = () => euroProtocolController.RegisterSideBEuroProtocol(It.IsAny<SideRequestApiModel>());

            //Assert
           var result=await Assert.ThrowsAsync<RestException>(act);
           Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
           Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task FindAllEuroProtocolsByEmail_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.FindAllEuroProtocolsByEmail(It.IsAny<string>())).ReturnsAsync(new List<EuroProtocolSimpleResponseApiModel>() { });
           
            //Act
            var result = await euroProtocolController.FindAllEuroProtocolsByEmail(It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as List<EuroProtocolSimpleResponseApiModel>;
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<EuroProtocolSimpleResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task FindAllEuroProtocolsByEmail_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.FindAllEuroProtocolsByEmail(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => euroProtocolController.FindAllEuroProtocolsByEmail(It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task UpdateEuroProtocol_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.UpdateEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await euroProtocolController.UpdateEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task UpdateEuroProtocol_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.UpdateEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => euroProtocolController.UpdateEuroProtocol(It.IsAny<EuroProtocolRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetAllCircumstances_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.GetAllCircumstances()).ReturnsAsync(new List<CircumstanceResponseApiModel>() { });

            //Act
            var result = await euroProtocolController.GetAllCircumstances() as OkObjectResult;
            var resultValue = result.Value as List<CircumstanceResponseApiModel>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<CircumstanceResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task GetAllCircumstances_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.GetAllCircumstances()).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => euroProtocolController.GetAllCircumstances();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetEuroProtocolBySerialNumber_ResultSuccess()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.GetEuroProtocolBySerialNumber(It.IsAny<string>())).ReturnsAsync(new EuroProtocolFullResponseApiModel { });

            //Act
            var result = await euroProtocolController.GetEuroProtocolBySerialNumber(It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as EuroProtocolFullResponseApiModel;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<EuroProtocolFullResponseApiModel>(resultValue);
        }

        [Fact]
        public async Task GetEuroProtocolBySerialNumber_ResultFailed()
        {
            //Arrange
            _euroProtocolService.Setup(repo => repo.GetEuroProtocolBySerialNumber(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => euroProtocolController.GetEuroProtocolBySerialNumber(It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
