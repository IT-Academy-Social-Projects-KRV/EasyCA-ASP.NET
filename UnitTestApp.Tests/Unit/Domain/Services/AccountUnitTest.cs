using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Helpers;
using CrudMicroservice.Domain.Services;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace UnitTestApp.Tests.Unit.Domain.Services
{
    public class AccountUnitTest
    {
        private readonly Mock<IGenericRepository<PersonalData>> _personalData = new Mock<IGenericRepository<PersonalData>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly AccountService _service;
        private readonly Mock<IHelper> _helper = new Mock<IHelper>();

        public AccountUnitTest()
        {
            _userManager = GetMockUserManager();
            _service = new AccountService(_userManager.Object, _mapper.Object, _personalData.Object, _helper.Object);
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }


        [Fact]
        public async Task CreatePersonalData_UserNotFound_TrowsException()
        {
            //Arrange
            User user = null;

            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            // Act
            Func<Task> result = () => _service.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>(), It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task CreatePersonalData_PersonalDataIdIsNull_ThrowsException()
        {
            //Arrange
            User user = new User()
            {
                PersonalDataId = "123",
                Id = "7d13eff6-71ca-4cc7-9b36-859f78ff39dd"
            };
            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            // Act
            Func<Task> result = () => _service.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>(), user.Id);

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }
        [Fact]
        public async Task CreatePersonalData_ResultSuccess()
        {
            //Arrange
            User user = new User()
            {
                PersonalDataId = null,
                Id = "7d13eff6-71ca-4cc7-9b36-859f78ff39dd"
            };
            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            _mapper.Setup(x => x.Map<PersonalData>(It.IsAny<PersonalDataRequestApiModel>())).Returns(new PersonalData()
            {
                Id = "123"
            });
            _personalData.Setup(x => x.CreateAsync(It.IsAny<PersonalData>()));
            _userManager.Setup(x => x.UpdateAsync(It.IsAny<User>()));

            // Act
            var result = await _service.CreatePersonalData(It.IsAny<PersonalDataRequestApiModel>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
        }

        [Fact]
        public async Task UpdatePersonalData_ReturnSuccess()
        {
            // Arrange
            User user = new User()
            {
                PersonalDataId = "123",
                Id = "7d13eff6-71ca-4cc7-9b36-859f78ff39dd"
            };
            UserRequestApiModel data = new UserRequestApiModel()
            {
                PersonalData = new PersonalDataRequestApiModel() { }
            };

            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            _mapper.Setup(x => x.Map<PersonalData>(It.IsAny<PersonalDataRequestApiModel>())).Returns(new PersonalData()
            {
                IPN = "321",
                Id = "123"
            });

            var replaceResult = new Mock<ReplaceOneResult>();
            replaceResult.Setup(x => x.IsAcknowledged).Returns(true);

            _personalData.Setup(x => x.ReplaceAsync(It.IsAny<Expression<Func<PersonalData, bool>>>(), It.IsAny<PersonalData>()))
                .ReturnsAsync(replaceResult.Object);

            _userManager.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _service.UpdatePersonalData(data, It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
        }


        [Fact]
        public async Task UpdatePersonalData_UserNotFound_ThrowsException()
        {
            // Arrange
            User user = null;

            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            // Act
            Func<Task> result = () => _service.UpdatePersonalData(It.IsAny<UserRequestApiModel>(), It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task UpdatePersonalData_PersonalDataIdIsNull_ThrowsException()
        {
            // Arrange
            User user = new User()
            {
                PersonalDataId = null,
                Id = "7d13eff6-71ca-4cc7-9b36-859f78ff39dd"
            };
            UserRequestApiModel data = new UserRequestApiModel()
            {
                PersonalData = new PersonalDataRequestApiModel() { }
            };

            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            _mapper.Setup(x => x.Map<PersonalData>(It.IsAny<PersonalDataRequestApiModel>())).Returns(new PersonalData()
            {
                IPN = "321",
            });

            // Act
            Func<Task> result = () => _service.UpdatePersonalData(data, user.Id);

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task UpdatePersonalData_ResultNotAcknowledged_ThrowsException()
        {
            // Arrange
            User user = new User()
            {
                PersonalDataId = "123",
                Id = "7d13eff6-71ca-4cc7-9b36-859f78ff39dd"
            };
            UserRequestApiModel data = new UserRequestApiModel()
            {
                PersonalData = new PersonalDataRequestApiModel() { }
            };

            _helper.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            _mapper.Setup(x => x.Map<PersonalData>(It.IsAny<PersonalDataRequestApiModel>())).Returns(new PersonalData()
            {
                IPN = "321"
            });

            var replaceResult = new Mock<ReplaceOneResult>();
            replaceResult.Setup(x => x.IsAcknowledged).Returns(false);
            _personalData.Setup(x => x.ReplaceAsync(It.IsAny<Expression<Func<PersonalData, bool>>>(), It.IsAny<PersonalData>()))
                .ReturnsAsync(replaceResult.Object);

            // Act
            Func<Task> result = () => _service.UpdatePersonalData(data, user.Id);

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task GetPersonalData_PersonalDataNotFound_ThrowsException()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Thomas",
                PersonalDataId = null
            });

            // Act
            Func<Task> result = () => _service.GetPersonalData(It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task GetPersonalData_IfUserNotFound_ThrowsException()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            Func<Task> act = () => _service.GetPersonalData(It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetPersonalData_ReturnsSuccess()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                Id = "ID123",
                FirstName = "Thomas",
                PersonalDataId = "7777777"
            });

            _personalData.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>()))
                .ReturnsAsync(new PersonalData()
                {
                    IPN = "123321"
                });

            _mapper.Setup(x => x.Map<PersonalDataResponseApiModel>(It.IsAny<PersonalData>()))
                .Returns(new PersonalDataResponseApiModel()
                {
                    IPN = "123321"
                });

            // Act
            var result = await _service.GetPersonalData(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PersonalDataResponseApiModel>(result);
            Assert.Equal("123321", result.IPN);
        }

        [Fact]
        public async Task GetUserById_IfUserDoesNotExist_ReturnsUnathorized()
        {
            // Arrange
            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act 
            Func<Task> act = () => _service.GetUserById(It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetUserById_IfUserExists_ReturnsSuccess()
        {
            // Arrange

            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Eugene"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>()))
                .ReturnsAsync(new PersonalData()
                {
                    IPN = "123"
                });

            _mapper.Setup(repo => repo.Map<PersonalDataResponseApiModel>(It.IsAny<PersonalData>()))
                .Returns(new PersonalDataResponseApiModel()
                {
                    IPN = "123"
                });

            _mapper.Setup(repo => repo.Map<UserResponseApiModel>(It.IsAny<User>()))
                .Returns(new UserResponseApiModel()
                {
                    FirstName = "Eugene"
                });

            // Act
            var result = await _service.GetUserById(It.IsAny<string>());

            // Arrange

            Assert.IsType<UserResponseApiModel>(result);
            Assert.NotNull(result);
            Assert.Equal("Eugene", result.FirstName);
        }

        [Fact]
        public async Task ChangePassword_UserNotFound_ThrowsException()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            Func<Task> result = () => _service.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task ChangePassword_ResultIsNotSucceeded_ThrowsException()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Eugene"
            });

            _userManager.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            Func<Task> result = () => _service.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(result);
        }

        [Fact]
        public async Task ChangePassword_ReturnsSuccess()
        {
            // Arrange
            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Eugene"
            });

            _userManager.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);


            // Act
            var result = await _service.ChangePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);

        }

        [Fact]
        public async Task GetUserByEmail_IfUserDoesNotExist_ReturnsUnathorized()
        {
            // Arrange
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            Func<Task> act = () => _service.GetUserByEmail(It.IsAny<string>());

            // Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsSuccess()
        {
            // Arrange
            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                LastName = "Statcham"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>()))
                .ReturnsAsync(new PersonalData()
                {
                    JobPosition = "The transporter"
                });

            _mapper.Setup(repo => repo.Map<PersonalDataResponseApiModel>(It.IsAny<PersonalData>()))
                .Returns(new PersonalDataResponseApiModel()
                {
                    JobPosition = "The transporter"
                });

            _mapper.Setup(repo => repo.Map<UserResponseApiModel>(It.IsAny<User>()))
                .Returns(new UserResponseApiModel()
                {
                    LastName = "Statcham"
                });

            // Act
            var result = await _service.GetUserByEmail(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponseApiModel>(result);
            Assert.Equal("Statcham", result.LastName);
        }
    }
}

