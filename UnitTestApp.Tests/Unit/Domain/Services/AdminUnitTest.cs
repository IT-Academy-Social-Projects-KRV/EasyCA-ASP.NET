using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System;
using CrudMicroservice.Domain.Errors;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestApp.Tests.Unit.Domain.Services
{
    public class AdminUnitTest
    {
        private readonly Mock<IGenericRepository<EuroProtocol>> _euroProtocols = new Mock<IGenericRepository<EuroProtocol>>();
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        private readonly AdminService _service;

        public AdminUnitTest()
        {
            _userManager = GetMockUserManager();
            _service = new AdminService(_userManager.Object, _mapper.Object, _euroProtocols.Object);
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task AddInspector_ResultSuccess()
        {
            //Arrange
            InspectorRequestApiModel inspector = new InspectorRequestApiModel()
            {
                Email = "pinkevychdima@gmail.com",
                FirstName = "Sasha",
                LastName = "Sasha",
                Password = "Qwerty211@",
                ConfirmPassword = "Qwerty211@"
            };

            _mapper.Setup(repo => repo.Map<User>(It.IsAny<InspectorRequestApiModel>())).Returns(new User()
            {
                Email = "pinkevychdima@gmail.com",
            });

            _userManager.Setup(repo => repo.CreateAsync(It.IsAny<User>(), "Qwerty211@")).ReturnsAsync(IdentityResult.Success);

            _userManager.Setup(repo => repo.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var result = await _service.AddInspector(inspector);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task AddInspector_ResultFailed()
        {
            //Arrange
            InspectorRequestApiModel inspector = new InspectorRequestApiModel()
            {
                Email = "pinkevychdima@gmail.com",
                FirstName = "Sasha",
                LastName = "Sasha",
                Password = "Qwerty211@",
                ConfirmPassword = "Qwerty211@"
            };

            _mapper.Setup(repo => repo.Map<User>(It.IsAny<InspectorRequestApiModel>())).Returns(new User()
            {
                Email = "pinkevychdima@gmail.com",
            });

            _userManager.Setup(repo => repo.CreateAsync(It.IsAny<User>(), "Qwerty211@")).ReturnsAsync(IdentityResult.Failed());

            _userManager.Setup(repo => repo.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            Func<Task> act = () => _service.AddInspector(inspector);

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetAllInspectors_ReturnsSuccess()
        {
            //Arrange
            _userManager.Setup(repo => repo.GetUsersInRoleAsync("inspector")).ReturnsAsync(new List<User>()
            {
                new User(),
            });

            _mapper.Setup(repo => repo.Map<List<UserResponseApiModel>>(It.IsAny<List<User>>())).Returns(new List<UserResponseApiModel>()
            {
                new UserResponseApiModel()
                {
                    Email = "pinkevychdima@gmail.com"
                },
            });

            //Act
            var result = await _service.GetAllInspectors();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponseApiModel>>(result.ToList());
        }

        [Fact]
        public async Task GetAllInspectors_ReturnsFailed()
        {
            //Arrange
            _userManager.Setup(repo => repo.GetUsersInRoleAsync("inspector")).ReturnsAsync(new List<User>());

            _mapper.Setup(repo => repo.Map<List<UserResponseApiModel>>(It.IsAny<List<User>>())).Returns(new List<UserResponseApiModel>()
            {
                new UserResponseApiModel()
                {
                    Email = "pinkevychdima@gmail.com"
                },
            });

            //Act
            Func<Task> act = () => _service.GetAllInspectors();

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetAllEuroProtocols_ReturnsSuccess()
        {
            //Arrange
            _euroProtocols.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<EuroProtocol>()
            {
                new EuroProtocol(),
            });

            _mapper.Setup(repo => repo.Map<List<EuroProtocolResponseApiModel>>(It.IsAny<List<User>>())).Returns(new List<EuroProtocolResponseApiModel>()
            {
                new EuroProtocolResponseApiModel()
                {
                    SerialNumber="123",
                },
            });

            //Act
            var result = await _service.GetAllEuroProtocols();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<EuroProtocolResponseApiModel>>(result.ToList());
        }

        [Fact]
        public async Task GetAllEuroProtocols_ReturnsFailed()
        {
            //Arrange
            _euroProtocols.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<EuroProtocol>());

            _mapper.Setup(repo => repo.Map<List<EuroProtocolResponseApiModel>>(It.IsAny<List<User>>())).Returns(new List<EuroProtocolResponseApiModel>()
            {
                new EuroProtocolResponseApiModel()
                {
                    SerialNumber="123",
                },
            });

            //Act
            Func<Task> act = () => _service.GetAllEuroProtocols();

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task DeleteInspector_ReturnsSuccess()
        {
            //Arrange
            DeleteInspectorRequestApiModel inspector = new DeleteInspectorRequestApiModel()
            {
                Email = "pinkevychdima@gmail.com"
            };

            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());
            _userManager.Setup(repo=>repo.RemoveFromRoleAsync(It.IsAny<User>() , It.IsAny<string>()));
            _userManager.Setup(repo => repo.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            var result = await _service.DeleteInspector(inspector);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteInspector_ReturnsFailed()
        {
            //Arrange
            DeleteInspectorRequestApiModel inspector = new DeleteInspectorRequestApiModel()
            {
                Email = "pinkevychdima@gmail.com"
            };

            _userManager.Setup(repo => repo.FindByEmailAsync(It.IsAny<string>()));
            _userManager.Setup(repo => repo.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()));
            _userManager.Setup(repo => repo.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));

            //Act
            Func<Task> act = () => _service.DeleteInspector(inspector);

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }
    }
}
