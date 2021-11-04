using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class AccountControllerUnitTest
    {
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();
        private readonly AccountController accountController;
        private readonly ChangePasswordRequestApiModel changePasswordRequestApiModel;

        public AccountControllerUnitTest()
        {
            accountController = new AccountController(_accountService.Object);
            changePasswordRequestApiModel = new ChangePasswordRequestApiModel()
            {
                Password = "testPassword",
                OldPassword = "testOldPassword"
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "anuviswan"),

            }, "mock"));

            accountController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        }

        [Fact]
        public async Task UpdatePersonalData_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.UpdatePersonalData(It.IsAny<UserRequestApiModel>(),It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await accountController.UpdatePersonalData(It.IsAny<UserRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdatePersonalData_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.UpdatePersonalData(It.IsAny<UserRequestApiModel>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.UpdatePersonalData(It.IsAny<UserRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetPersonalData_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetPersonalData(It.IsAny<string>())).ReturnsAsync(new PersonalDataResponseApiModel());

            //Act
            var result = await accountController.GetPersonalData() as OkObjectResult;
            var resultValue = result.Value as PersonalDataResponseApiModel;

            //Assert
            Assert.IsType<PersonalDataResponseApiModel>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPersonalData_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetPersonalData(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => accountController.GetPersonalData();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetUserById_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserById(It.IsAny<string>())).ReturnsAsync(new UserResponseApiModel());

            //Act
            var result = await accountController.GetUserById() as OkObjectResult;
            var resultValue = result.Value as UserResponseApiModel;

            //Assert
            Assert.IsType<UserResponseApiModel>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUserById_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserById(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.GetUserById();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetUserById_Id_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserById(It.IsAny<string>())).ReturnsAsync(new UserResponseApiModel());

            //Act
            var result = await accountController.GetUserById(It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as UserResponseApiModel;

            //Assert
            Assert.IsType<UserResponseApiModel>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUserById_Id_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserById(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.GetUserById(It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new UserResponseApiModel());

            //Act
            var result = await accountController.GetUserByEmail(It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as UserResponseApiModel;

            //Assert
            Assert.IsType<UserResponseApiModel>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.GetUserByEmail(It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task CreatePersonalData_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await accountController.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreatePersonalData_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task ChangePassword_ReturnsSuccess()
        {
            //Arrange
            _accountService.Setup(repo => repo.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));
          
            //Act
            var result = await accountController.ChangePassword(changePasswordRequestApiModel) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ChangePassword_ReturnsFailed()
        {
            //Arrange
            _accountService.Setup(repo => repo.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => accountController.ChangePassword(changePasswordRequestApiModel);

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
