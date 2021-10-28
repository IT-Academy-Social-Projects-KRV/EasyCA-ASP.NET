using AuthMicroservice.Data.Entities;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using AuthMicroservice.Domain.Errors;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.Services
{
    public class AuthServiceUnitTest
    {
        private readonly Mock<UserManager<User>> _userManager;
        private readonly Mock<SignInManager<User>> _signInManager;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IJwtService> _jwtService;
        private readonly Mock<IEmailService> _emailService;
        private readonly AuthService _authService;

        public AuthServiceUnitTest()
        {
            _userManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _signInManager = new Mock<SignInManager<User>>(_userManager.Object, Mock.Of<IHttpContextAccessor>(), 
                Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);
            _mapper = new Mock<IMapper>();
            _jwtService = new Mock<IJwtService>();
            _emailService = new Mock<IEmailService>();
            _authService = new AuthService(_userManager.Object, _signInManager.Object, _jwtService.Object, _mapper.Object, _emailService.Object);
        }


        private RegisterRequestApiModel GetRegisterModel()
        {
            return new RegisterRequestApiModel()
            {
                Email = "test@gmail.com",
                FirstName = "Jane",
                LastName = "Doe",
                Password = "Qwerty211@",
                ConfirmPassword = "Qwerty211@",
                ClientURI = "http://localhost:4200/"
            };
        }

        private LoginRequestApiModel GetLoginModel()
        {
            return new LoginRequestApiModel()
            {
                Email = "test@gmail.com",
                Password = "Qwerty211@"
            };
        }

        private ForgotPasswordRequestApiModel GetForgotPasswordModel()
        {
            return new ForgotPasswordRequestApiModel()
            {
                Email = "test@gmail.com",
                NewPassword = "Qwerty212@",
                ConfirmPassword = "Qwerty212@",
                PasswordURI = "http://localhost:4200/restorePassword"
            };
        }

        private ResendConfirmationRequestApiModel GetResendConfirmationModel()
        {
            return new ResendConfirmationRequestApiModel()
            {
                Email = "test@gmail.com",
                ResendConfirmationURI = "http://localhost:4200/emailVerify"
            };
        }

        private User GetUser()
        {
            return new User()
            {
                FirstName = "Jane",
                LastName = "Doe",
                PersonalDataId = "11111111",
                RefreshToken = new RefreshToken()
            };
        }

        [Fact]
        public async Task RegisterUser_WithProperModel_Returns_ResponseApiModel()
        {
            // Arrange
            var registerRequest = GetRegisterModel();
            _mapper.Setup(x => x.Map<User>(It.IsAny<RegisterRequestApiModel>())).Returns(GetUser());
            _emailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = true });
            _userManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterUser(registerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RegisterUser_WithNullValue_Throws_NullReferenceException()
        {
            // Arrange
            _mapper.Setup(x => x.Map<User>(null)).Throws(new NullReferenceException());

            // Act
            Func<Task> act = () => _authService.RegisterUser(null);

            // Assert
            await Assert.ThrowsAsync<NullReferenceException>(act);
        }

        [Fact]
        public async Task RegisterUser_WithEmailNotSent_Throws_RestException_BadRequest()
        {
            // Arrange
            var registerRequest = GetRegisterModel();
            _mapper.Setup(x => x.Map<User>(It.IsAny<RegisterRequestApiModel>()))
                .Returns(new User()
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    PersonalDataId = "11111111",
                    RefreshToken = new RefreshToken()
                });

            _emailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = false });
            _userManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            Func<Task> act = () => _authService.RegisterUser(registerRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_WithIdentityResultFailed_Throws_RestException_BadRequest()
        {
            // Arrange
            var registerRequest = GetRegisterModel();
            _mapper.Setup(x => x.Map<User>(It.IsAny<RegisterRequestApiModel>()))
                .Returns(new User()
                {
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    PersonalDataId = "11111111",
                    RefreshToken = new RefreshToken()
                });

            _emailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = true });
            _userManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            // Act
            Func<Task> act = () => _authService.RegisterUser(registerRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task LoginUser_WithRightCredentials_WithConfirmedEmail_Returns_AuthenticateResponseApiModel()
        {
            // Arrange
            var loginRequest = GetLoginModel();
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(true);
            _userManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>() { "participant" });
            _jwtService.Setup(x => x.CreateJwtToken(It.IsAny<User>())).ReturnsAsync("token");
            _jwtService.Setup(x => x.CreateRefreshToken()).Returns(new RefreshToken());

            // Act
            var result = await _authService.LoginUser(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthenticateResponseApiModel>(result);
        }

        [Fact]
        public async Task LoginUser_WithRightCredentials_WhenUserNotFound_Throws_RestException_NotFound()
        {
            // Arrange
            var loginRequest = GetLoginModel();
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            // Act
            Func<Task> act = () => _authService.LoginUser(loginRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task LoginUser_WithRightCredentials_WithUnconfirmedEmail_Throws_RestException_Unauthorized()
        {
            // Arrange
            var loginRequest = GetLoginModel();
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(x => x.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(false);
            _userManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>() { "participant" });
            _jwtService.Setup(x => x.CreateJwtToken(It.IsAny<User>())).ReturnsAsync("token");
            _jwtService.Setup(x => x.CreateRefreshToken()).Returns(new RefreshToken());

            // Act
            Func<Task> act = () => _authService.LoginUser(loginRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task LoginUser_WithWrongCredentials_Throws_RestException_BadRequest()
        {
            // Arrange
            var loginRequest = GetLoginModel();
            _signInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            Func<Task> act = () => _authService.LoginUser(loginRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ConfirmEmailAsync_WithValidEmail_Returns_Response_Ok()
        {
            // Arrange
            var email = "test@gmail.com";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9DeyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQDSflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ConfirmEmailAsync(email, token);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task ConfirmEmailAsync_WithNonExistingEmail_Throws_RestException_Unauthorized()
        {
            // Arrange
            var email = "test@gmail.com";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9DeyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQDSflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            _userManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            Func<Task> act = () => _authService.ConfirmEmailAsync(email, token);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task ConfirmEmailAsync_WithWrongToken_Throws_RestException_BadRequest()
        {
            // Arrange
            var email = "test@gmail.com";
            var token = "badtoken";
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            // Act
            Func<Task> act = () => _authService.ConfirmEmailAsync(email, token);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ForgotPassword_WithValidEmail_WithMatchingPasswords_Returns_Response_Ok()
        {
            // Arrange
            var forgotPasswordRequest = GetForgotPasswordModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _emailService.Setup(repo => repo.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = true });

            // Act
            var result = await _authService.ForgotPassword(forgotPasswordRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task ForgotPassword_WithInvalidEmail_Throws_RestException_NotFound()
        {
            // Arrange
            var forgotPasswordRequest = GetForgotPasswordModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            // Act
            Func<Task> act = () => _authService.ForgotPassword(forgotPasswordRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ForgotPassword_WithValidEmail_WithMismatchedPasswords_Throws_RestException_BadRequest()
        {
            // Arrange
            var forgotPasswordRequest = GetForgotPasswordModel();
            forgotPasswordRequest.ConfirmPassword = "Qwerty213@";
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());

            // Act
            Func<Task> act = () => _authService.ForgotPassword(forgotPasswordRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Passwords do not match", result.Message);
        }

        [Fact]
        public async Task ForgotPassword_WithValidData_WithRestoreLinkNotBeingSent_Throws_RestException_BadRequest()
        {
            // Arrange
            var forgotPasswordRequest = GetForgotPasswordModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _emailService.Setup(repo => repo.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = false });

            // Act
            Func<Task> act = () => _authService.ForgotPassword(forgotPasswordRequest);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Couldn't send a password reset link", result.Message);
        }

        [Fact]
        public async Task RestorePassword_WithValidData_Returns_Response_Ok()
        {
            // Arrange
            var encodedPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("Qwerty212@"));
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("token"));
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RestorePassword(encodedPassword, encodedToken, "test@gmail.com");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task RestorePassword_WithInvalidEmail_Throws_RestException_NotFound()
        {
            // Arrange
            var encodedPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("Qwerty212@"));
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("token"));
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            // Act
            Func<Task> act = () => _authService.RestorePassword(encodedPassword, encodedToken, "test@gmail.com");

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task RestorePassword_WithValidData_WithPasswordNotBeingReset_Throws_RestException_BadRequest()
        {
            // Arrange
            var encodedPassword = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("Qwerty212@"));
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("token"));
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { }));

            // Act
            Func<Task> act = () => _authService.RestorePassword(encodedPassword, encodedToken, "test@gmail.com");

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task ResendConfirmation_WithValidData_Returns_Response_Ok() 
        {
            // Arrange
            var resendModel = GetResendConfirmationModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(false);
            _userManager.Setup(repo => repo.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _emailService.Setup(repo => repo.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = true });

            // Act
            var result = await _authService.ResendConfirmation(resendModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task ResendConfirmation_WithInvalidEmail_Throws_RestException_NotFound()
        {
            // Arrange
            var resendModel = GetResendConfirmationModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult<User>(null));

            // Act
            Func<Task> act = () => _authService.ResendConfirmation(resendModel);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task ResendConfirmation_WithAlreadyConfirmedEmail_Throws_RestException_BadRequest()
        {
            // Arrange
            var resendModel = GetResendConfirmationModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            Func<Task> act = () => _authService.ResendConfirmation(resendModel);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("This email address has already been confirmed", result.Message);
        }

        [Fact]
        public async Task ResendConfirmation_WithValidData_WithConfirmationLinkNotBeingSent_Throws_RestException_BadRequest()
        {
            // Arrange
            var resendModel = GetResendConfirmationModel();
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(GetUser());
            _userManager.Setup(repo => repo.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(false);
            _userManager.Setup(repo => repo.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _emailService.Setup(repo => repo.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ResponseApiModel<HttpStatusCode>() { Success = false });

            // Act
            Func<Task> act = () => _authService.ResendConfirmation(resendModel);

            // Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Couldn't send the confirmation link", result.Message);
        }
    }
}
