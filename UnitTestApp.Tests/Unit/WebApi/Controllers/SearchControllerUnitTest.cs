using CrudMicroservice.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQConfig.Models.Responses;
using SearchMicroservice.Domain.Interfaces;
using SearchMicroservice.WebAPI.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
    public class SearchControllerUnitTest
    {
        private readonly Mock<ISearchService> _searchService = new Mock<ISearchService>();
        private readonly SearchController searchController;

        public SearchControllerUnitTest()
        {
            searchController = new SearchController(_searchService.Object);
        }

        [Fact]
        public async Task Search_ResultSuccess()
        {
            //Arrange
            _searchService.Setup(repo => repo.Search(It.IsAny<string>())).ReturnsAsync(new TransportResponseModelRabbitMQ());

            //Act
            var result = await searchController.Search(It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as TransportResponseModelRabbitMQ;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<TransportResponseModelRabbitMQ>(resultValue);
        }

        [Fact]
        public async Task Search_ResultFailed()
        {
            //Arrange
            _searchService.Setup(repo => repo.Search(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => searchController.Search(It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
