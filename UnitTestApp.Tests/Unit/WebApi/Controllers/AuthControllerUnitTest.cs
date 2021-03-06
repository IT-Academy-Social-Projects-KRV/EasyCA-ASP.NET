using System.Net;
using System.Threading.Tasks;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using AuthMicroservice.Domain.Errors;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class AuthControllerUnitTest
    {
        private readonly Mock<IJwtService> _jwtService;
        private readonly Mock<IAuthService> _authService;
        private readonly AuthController _controller;

        public AuthControllerUnitTest()
        {
            _jwtService = new Mock<IJwtService>();
            _authService = new Mock<IAuthService>();
            _controller = new AuthController(_authService.Object, _jwtService.Object);
        }

        [Fact]
        public async Task Login_WithRightCredentials_Returns_AuthenticateResponseApiModel()
        {
            //Arrange
            _authService.Setup(x => x.LoginUser(It.IsAny<LoginRequestApiModel>())).ReturnsAsync(new AuthenticateResponseApiModel("email"));

            //Act
            var result = await _controller.Login(It.IsAny<LoginRequestApiModel>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<AuthenticateResponseApiModel>(okObjectResult.Value);
            Assert.Equal("email", model.Email);
        }

        [Fact]
        public async Task Login_WithRightCredentials_WithUnconfirmedEmail_Throws_RestException_Unauthorized()
        {
            //Arrange
            _authService.Setup(x => x.LoginUser(It.IsAny<LoginRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.Unauthorized, "Email not confirmed"));

            //Act
            Task act() => _controller.Login(It.IsAny<LoginRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("Email not confirmed", result.Message);
        }

        [Fact]
        public async Task Login_WithWrongCredentials_Throws_RestException_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.LoginUser(It.IsAny<LoginRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Wrong credentials"));

            //Act
            Task act() => _controller.Login(It.IsAny<LoginRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Wrong credentials", result.Message);
        }

        [Fact]
        public async Task RefreshToken_WithValidToken_Returns_AuthenticateResponseApiModel()
        {
            //Arrange
            _jwtService.Setup(x => x.RefreshTokenAsync(It.IsAny<string>())).ReturnsAsync(new AuthenticateResponseApiModel());

            //Act
            var result = await _controller.RefreshToken(new RefreshTokenRequestApiModel() { RefreshToken = It.IsAny<string>() });

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<AuthenticateResponseApiModel>(okObjectResult.Value);
        }

        [Fact]
        public async Task RefreshToken_WithInvalidToken_Returns_BadRequest()
        {
            //Arrange
            _jwtService.Setup(x => x.RefreshTokenAsync(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Token is expired"));

            //Act
            Task act() =>  _controller.RefreshToken(new RefreshTokenRequestApiModel() { RefreshToken = It.IsAny<string>() });

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Token is expired", result.Message);
        }

        [Fact]
        public async Task Register_WithValidData_Returns_Ok()
        {
            //Arrange
            _authService.Setup(x => x.RegisterUser(It.IsAny<RegisterRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success!"));

            //Act
            var result = await _controller.Register(It.IsAny<RegisterRequestApiModel>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ResponseApiModel<HttpStatusCode>>(okObjectResult.Value);
            Assert.Equal(HttpStatusCode.OK, model.Data);
            Assert.True(model.Success);
            Assert.Equal("Success!", model.Message);
        }

        [Fact]
        public async Task Register_WithInvalidData_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.RegisterUser(It.IsAny<RegisterRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Error!"));

            //Act
            Task act() => _controller.Register(It.IsAny<RegisterRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error!", result.Message);
        }

        [Fact]
        public async Task ConfirmEmail_WithValidData_Returns_Ok()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success!"));

            //Act
            var result = await _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ResponseApiModel<HttpStatusCode>>(okObjectResult.Value);
            Assert.Equal(HttpStatusCode.OK, model.Data);
            Assert.True(model.Success);
            Assert.Equal("Success!", model.Message);
        }

        [Fact]
        public async Task ConfirmEmail_WithWrongEmail_Returns_Unauthorized()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.Unauthorized, "User not found"));

            //Act
            Task act() => _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
            Assert.Equal("User not found", result.Message);
        }

        [Fact]
        public async Task ConfirmEmail_WithWrongToken_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Wrong token!"));

            //Act
            Task act() => _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Wrong token!", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_WithValidData_Returns_Ok()
        {
            //Arrange
            _authService.Setup(x => x.ForgotPassword(It.IsAny<ForgotPasswordRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success!"));

            //Act
            var result = await _controller.ForgotPassword(It.IsAny<ForgotPasswordRequestApiModel>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ResponseApiModel<HttpStatusCode>>(okObjectResult.Value);
            Assert.Equal(HttpStatusCode.OK, model.Data);
            Assert.True(model.Success);
            Assert.Equal("Success!", model.Message);
        }

        [Fact]
        public async Task ForgotPassword_WithWrongToken_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Wrong token!"));

            //Act
            Task act() => _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Wrong token!", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_WithInvalidEmail_Returns_NotFound()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Not found!"));

            //Act
            Task act() => _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Not found!", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_WithNotEqualPasswords_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Passwords not match"));

            //Act
            Task act() => _controller.ConfirmEmail(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Passwords not match", result.Message);
        }

        [Fact]
        public async Task RestorePassword_WithValidData_Returns_Ok()
        {
            //Arrange
            _authService.Setup(x => x.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success!"));

            //Act
            var result = await _controller.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ResponseApiModel<HttpStatusCode>>(okObjectResult.Value);
            Assert.Equal(HttpStatusCode.OK, model.Data);
            Assert.True(model.Success);
            Assert.Equal("Success!", model.Message);
        }

        [Fact]
        public async Task RestorePassword_WithInvalidPassword_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Error!"));

            //Act
            Task act() => _controller.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error!", result.Message);
        }

        [Fact]
        public async Task RestorePassword_WithInvalidEmail_Returns_NotFound()
        {
            //Arrange
            _authService.Setup(x => x.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Not found!"));

            //Act
            Task act() => _controller.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Not found!", result.Message);
        }

        [Fact]
        public async Task RestorePassword_WithInvalidToken_Returns_NotFound()
        {
            //Arrange
            _authService.Setup(x => x.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Error!"));

            //Act
            Task act() => _controller.RestorePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error!", result.Message);
        }

        [Fact]
        public async Task ResendConfirmation_WithValidData_Returns_Ok()
        {
            //Arrange
            _authService.Setup(x => x.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success!"));

            //Act
            var result = await _controller.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>());

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ResponseApiModel<HttpStatusCode>>(okObjectResult.Value);
            Assert.Equal(HttpStatusCode.OK, model.Data);
            Assert.True(model.Success);
            Assert.Equal("Success!", model.Message);
        }

        [Fact]
        public async Task ResendConfirmation_WithInvalidEmail_Returns_NotFound()
        {
            //Arrange
            _authService.Setup(x => x.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Error!"));

            //Act
            Task act() => _controller.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Error!", result.Message);
        }

        [Fact]
        public async Task ResendConfirmation_EmailAlreadyConfirmed_Returns_BadRequest()
        {
            //Arrange
            _authService.Setup(x => x.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Error!"));

            //Act
            Task act() => _controller.ResendConfirmation(It.IsAny<ResendConfirmationRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Error!", result.Message);
        }
    }
}
