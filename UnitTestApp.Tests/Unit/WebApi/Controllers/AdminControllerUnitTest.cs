using System;
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

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class AdminControllerUnitTest
    {
        private readonly Mock<IAdminService> _adminService = new Mock<IAdminService>();
        private readonly AdminController adminController;

        public AdminControllerUnitTest()
        {
            adminController = new AdminController(_adminService.Object);
        }

        [Fact]
        public async Task AddInspector_ResultSuccess()
        {
            //Arrange
            _adminService.Setup(repo => repo.AddInspector(It.IsAny<InspectorRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await adminController.AddInspectors(It.IsAny<InspectorRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task AddInspector_ResultFailed()
        {
            //Arrange
            _adminService.Setup(repo => repo.AddInspector(null)).ThrowsAsync(new NullReferenceException());

            //Act
            Func<Task> act = () => adminController.AddInspectors(null);

            //Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact]
        public async Task GetAllInspectors_ReturnsSuccess()
        {
            //Arrange
            _adminService.Setup(repo => repo.GetAllInspectors()).ReturnsAsync(new List<UserResponseApiModel>());

            //Act
            var result = await adminController.GetAllInspectors() as OkObjectResult;
            var resultValue = result.Value as List<UserResponseApiModel>;

            //Assert
            Assert.IsType<List<UserResponseApiModel>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllInspectors_ReturnsFailed()
        {
            //Arrange
            _adminService.Setup(repo => repo.GetAllInspectors()).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => adminController.GetAllInspectors();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetAllEuroProtocols_ReturnsSuccess()
        {
            //Arrange
            _adminService.Setup(repo => repo.GetAllEuroProtocols()).ReturnsAsync(new List<EuroProtocolResponseApiModel>());

            //Act
            var result = await adminController.GetAllEuroProtocols() as OkObjectResult;
            var resultValue = result.Value as List<EuroProtocolResponseApiModel>;

            //Assert
            Assert.IsType<List<EuroProtocolResponseApiModel>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllEuroProtocols_ReturnsFailed()
        {
            //Arrange
            _adminService.Setup(repo => repo.GetAllEuroProtocols()).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => adminController.GetAllEuroProtocols();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task DeleteInspector_ReturnsSuccess()
        {
            //Arrange
            _adminService.Setup(repo => repo.DeleteInspector(It.IsAny<DeleteInspectorRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await adminController.DeleteInspector(It.IsAny<DeleteInspectorRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task DeleteInspector_ReturnsFailed()
        {
            //Arrange
            _adminService.Setup(repo => repo.DeleteInspector(It.IsAny<DeleteInspectorRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => adminController.DeleteInspector(It.IsAny<DeleteInspectorRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
