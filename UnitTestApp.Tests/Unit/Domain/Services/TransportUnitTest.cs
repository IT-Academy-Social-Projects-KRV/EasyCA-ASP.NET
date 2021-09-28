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
    public class TransportUnitTest
    {
        private readonly Mock<IGenericRepository<Transport>> _transports = new Mock<IGenericRepository<Transport>>();
        private readonly Mock<IGenericRepository<TransportCategory>> _transportCategories = new Mock<IGenericRepository<TransportCategory>>();
        private readonly Mock<IGenericRepository<PersonalData>> _personalData = new Mock<IGenericRepository<PersonalData>>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<UserManager<User>> _userManager = new Mock<UserManager<User>>();
        private readonly TransportService _service;
        private readonly Transport transportSuccess = new Transport() { Id = "6146083728317e03692f4501", UserId = "fb5c4325-26e7-403f-9983-efd0889c6a5c" };
        public TransportUnitTest()
        {
            _userManager = GetMockUserManager();
            _service = new TransportService(_mapper.Object, _userManager.Object, _transports.Object, _transportCategories.Object, _personalData.Object);
        }
        private UpdateTransportRequestModel Model()
        {
            UpdateTransportRequestModel model = new UpdateTransportRequestModel()
            {
                Id = "324324",
                ProducedBy = "BMW",
                CarPlate = "AS1245DF",
                Model = "X5",
                CategoryName = "B",
                VINCode = "2123123",
                Color = "White",
                YearOfProduction = 2020,
                InsuaranceNumber = new Insuarance()
                {
                    CompanyName = "wwww",
                    SerialNumber = "123"
                }
            };
            return model;
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async Task AddTransport_WithUnexistingCategory_ReturnsNotFound()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync((TransportCategory)null);

            //Act
            Func<Task> act = () => _service.AddTransport(It.IsAny<AddTransportRequestModel>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task AddTransport_WithExistingCategory_ReturnsSuccess()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "B"
            });

            //Act
            var result = await _transportCategories.Object.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>());

            //Assert 
            Assert.NotNull(result);
            Assert.Equal("B", result.CategoryName);
        }

        [Fact]
        public async Task AddTransport_IfUserNotFound_ReturnsNotFound()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A"
            });

            _mapper.Setup(repo => repo.Map<Transport>(It.IsAny<AddTransportRequestModel>())).Returns(new Transport()
            {
                CarCategory = new TransportCategory()
                {
                    CategoryName = "A"
                }

            });

            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            //Act
            Func<Task> act = () => _service.AddTransport(It.IsAny<AddTransportRequestModel>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task AddTransport_IfUserFound_ReturnsSuccess()
        {
            //Arrange
            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Sasha"
            });

            //Act
            var result = await _userManager.Object.FindByIdAsync(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Sasha", result.FirstName);
        }

        [Fact]
        public async Task AddTransport_PersonalDataNotfound_ReturnsNotFound()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A"
            });

            _mapper.Setup(repo => repo.Map<Transport>(It.IsAny<AddTransportRequestModel>())).Returns(new Transport()
            {
                CarCategory = new TransportCategory()
                {
                    CategoryName = "A"
                }

            });

            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Sasha"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>())).ReturnsAsync((PersonalData)null);

            //Act
            Func<Task> act = () => _service.AddTransport(It.IsAny<AddTransportRequestModel>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task AddTransport_WithExistingPersonalData_ReturnsSuccess()
        {
            //Arrange
            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>())).ReturnsAsync(new PersonalData()
            {
                ServiceNumber = "123"
            });

            //Act
            var result = await _personalData.Object.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>());

            //Assert 
            Assert.NotNull(result);
            Assert.Equal("123", result.ServiceNumber);
        }

        [Fact]
        public async Task AddTransport_PersonalDataDidntUpdate_ReturnsBadRequest()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A"
            });

            _mapper.Setup(repo => repo.Map<Transport>(It.IsAny<AddTransportRequestModel>())).Returns(new Transport()
            {
                CarCategory = new TransportCategory()
                {
                    CategoryName = "A"
                }

            });
            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Sasha"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>())).ReturnsAsync(new PersonalData()
            {
                ServiceNumber = "123",
                UserCars = new List<string>()
            });

            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);

            _personalData.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<PersonalData, bool>>>(), It.IsAny<UpdateDefinition<PersonalData>>())).ReturnsAsync(mockResult.Object);

            //Act
            Func<Task> act = () => _service.AddTransport(It.IsAny<AddTransportRequestModel>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task AddTransport_ResultSuccess()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A"
            });

            _mapper.Setup(repo => repo.Map<Transport>(It.IsAny<AddTransportRequestModel>())).Returns(new Transport()
            {
                CarCategory = new TransportCategory()
                {
                    CategoryName = "A"
                }

            });
            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                FirstName = "Sasha"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>())).ReturnsAsync(new PersonalData()
            {
                ServiceNumber = "123",
                UserCars = new List<string>()
            });


            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);

            _personalData.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<PersonalData, bool>>>(), It.IsAny<UpdateDefinition<PersonalData>>())).ReturnsAsync(mockResult.Object);

            //Act
            var result = await _service.AddTransport(It.IsAny<AddTransportRequestModel>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetTransportById_ReturnsSuccess()
        {
            //Arrange
            var transport = new Transport()
            {
                Id = transportSuccess.Id,
                UserId = transportSuccess.UserId,
            };

            _transports.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<Transport, bool>>>())).ReturnsAsync(transport);
            _mapper.Setup(s => s.Map<TransportResponseApiModel>(transport)).Returns(new TransportResponseApiModel()
            {
                Id = transport.Id,
                UserId = transport.UserId
            });

            //Act
            var result = await _service.GetTransportById(transportSuccess.Id, transportSuccess.UserId);

            //Assert
            Assert.IsType<TransportResponseApiModel>(result);
            Assert.Equal(result.Id, transportSuccess.Id);
            Assert.Equal(result.UserId, transportSuccess.UserId);
        }

        [Fact]
        public async Task GetTransportById_WithUnexistingTransport_ReturnsNotFound()
        {
            //Arrange
            _transports.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<Transport, bool>>>()))
                .ReturnsAsync((Transport)null);

            //Act
            Func<Task> act = () => _service.GetTransportById(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetAllTransport_WithUnexistingTransport_ReturnsNotFound()
        {
            //Arrange
            _transports.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<Transport, bool>>>())).ReturnsAsync(new List<Transport>());

            //Act
            Func<Task> act = () => _service.GetAllTransports(It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task GetAllTransport_WithExistingTransport_ReturnsSuccess()
        {
            //Arrange
            _transports.Setup(repo => repo.GetAllByFilterAsync(It.IsAny<Expression<Func<Transport, bool>>>())).ReturnsAsync(new List<Transport>()
            {
                new Transport()
                {
                    ProducedBy="BMW"
                }
            });
            _mapper.Setup(repo => repo.Map<List<TransportResponseApiModel>>(It.IsAny<List<Transport>>())).Returns(new List<TransportResponseApiModel>());

            //Act
            var result = (await _service.GetAllTransports(It.IsAny<string>())).ToList();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<TransportResponseApiModel>>(result);
        }

        [Fact]
        public async Task UpdateTransport_IfEverythingOk_ReturnsSuccess()
        {
            //Arrange

            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A",

            });

            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);

            var update = Builders<Transport>.Update
                .Set(c => c.Color, "Black")
                .Set(c => c.InsuaranceNumber, new Insuarance() { SerialNumber = "1111" })
                .Set(c => c.CarCategory, new TransportCategory() { CategoryName = "B" });

            _transports.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<Transport, bool>>>(), It.IsAny<UpdateDefinition<Transport>>())).ReturnsAsync(mockResult.Object);

            //Act
            var result = await _service.UpdateTransport(Model(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateTransport_IfTransportDidntUpdate_ReturnsBadRequest()
        {
            //Arrange
            _transportCategories.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<TransportCategory, bool>>>())).ReturnsAsync(new TransportCategory()
            {
                CategoryName = "A"
            });

            var mockResult = new Mock<UpdateResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);
    
            _transports.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<Transport, bool>>>(), It.IsAny<UpdateDefinition<Transport>>())).ReturnsAsync(mockResult.Object);

            //Act
            Func<Task> act = () => _service.UpdateTransport(Model(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task DeleteTransport_IfTransportDidntDelete_ReturnsBadRequest()
        {
            //Arrange
            var mockResult = new Mock<DeleteResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(false);

            _transports.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<Transport, bool>>>())).ReturnsAsync(mockResult.Object);

            //Act
            Func<Task> act = () => _service.DeleteTransport(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            await Assert.ThrowsAsync<RestException>(act);
        }

        [Fact]
        public async Task DeleteTransport_IfEverythingOk_ReturnsSuccess()
        {
            //Arrange
            var mockResult = new Mock<DeleteResult>();
            mockResult.Setup(c => c.IsAcknowledged).Returns(true);

            _transports.Setup(repo => repo.DeleteAsync(It.IsAny<Expression<Func<Transport, bool>>>())).ReturnsAsync(mockResult.Object);
            _userManager.Setup(repo => repo.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User()
            {
                Id = "123"
            });

            _personalData.Setup(repo => repo.GetByFilterAsync(It.IsAny<Expression<Func<PersonalData, bool>>>())).ReturnsAsync(new PersonalData() { UserCars = new List<string>() });

            _personalData.Setup(repo => repo.UpdateAsync(It.IsAny<Expression<Func<PersonalData, bool>>>(), It.IsAny<UpdateDefinition<PersonalData>>()));

            //Act
            var result = await _service.DeleteTransport(It.IsAny<string>(), It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(result);
            Assert.True(result.Success);
        }
    }
}

