using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestApp.Tests.Unit.WebApi.Controllers
{
  public class TpansportControllerUnitTest
  {
    private readonly Mock<ITransportService> _transportService = new Mock<ITransportService>();
    private readonly TransportController transportController;

    public TpansportControllerUnitTest()
    {
      transportController = new TransportController(_transportService.Object);

      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
         new Claim("Id", "anuviswan"),

      }, "mock"));

      transportController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
    }

    [Fact]
    public async Task GetAllTransports_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetAllTransports(It.IsAny<string>())).ReturnsAsync(new List<TransportResponseApiModel>() { });

      //Act
      var result = await transportController.GetAllTransports() as OkObjectResult;
      var resultValue = result.Value as List<TransportResponseApiModel>;

      //Assert
      Assert.IsType<OkObjectResult>(result);
      Assert.IsType<List<TransportResponseApiModel>>(resultValue);
    }

    [Fact]
    public async Task GetAllTransports_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetAllTransports(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

      //Act
      Func<Task> act = () => transportController.GetAllTransports();

      //Assert
      var result = await Assert.ThrowsAsync<RestException>(act);
      Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
      Assert.Equal("Failed", result.Message);
    }

    [Fact]
    public async Task GetTransportById_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetTransportById(It.IsAny<string>())).ReturnsAsync(new TransportResponseApiModel { });

      //Act
      var result = await transportController.GetTransportById(It.IsAny<string>()) as OkObjectResult;
      var resultValue = result.Value as TransportResponseApiModel;

      //Assert
      Assert.IsType<OkObjectResult>(result);
      Assert.IsType<TransportResponseApiModel>(resultValue);
    }

    [Fact]
    public async Task GetTransportById_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetTransportById(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

      //Act
      Func<Task> act = () => transportController.GetTransportById(It.IsAny<string>());

      //Assert
      var result = await Assert.ThrowsAsync<RestException>(act);
      Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
      Assert.Equal("Failed", result.Message);
    }

    [Fact]
    public async Task AddTransport_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.AddTransport(It.IsAny<AddTransportRequestApiModel>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

      //Act
      var result = await transportController.AddTransport(It.IsAny<AddTransportRequestApiModel>()) as OkObjectResult;
      var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

      //Assert
      Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
      Assert.Equal("Success", resultValue.Message);
      Assert.True(resultValue.Success);
      Assert.Equal(HttpStatusCode.OK, resultValue.Data);
    }

    [Fact]
    public async Task AddTransport_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.AddTransport(null, It.IsAny<string>())).ThrowsAsync(new NullReferenceException());

      //Act
      Func<Task> act = () => transportController.AddTransport(null);

      //Assert
      await Assert.ThrowsAsync<NullReferenceException>(act);
    }

    [Fact]
    public async Task UpdateTransort_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.UpdateTransport(It.IsAny<UpdateTransportRequestApiModel>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

      //Act
      var result = await transportController.UpdateTransort(It.IsAny<UpdateTransportRequestApiModel>()) as OkObjectResult;
      var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

      //Assert
      Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
      Assert.Equal("Success", resultValue.Message);
      Assert.True(resultValue.Success);
      Assert.Equal(HttpStatusCode.OK, resultValue.Data);
    }

    [Fact]
    public async Task UpdateTransort_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.UpdateTransport(It.IsAny<UpdateTransportRequestApiModel>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

      //Act
      Func<Task> act = () => transportController.UpdateTransort(It.IsAny<UpdateTransportRequestApiModel>());

      //Assert
      var result = await Assert.ThrowsAsync<RestException>(act);
      Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
      Assert.Equal("Failed", result.Message);
    }

    [Fact]
    public async Task DeleteTransport_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.DeleteTransport(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

      //Act
      var result = await transportController.DeleteTransport(It.IsAny<string>()) as OkObjectResult;
      var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

      //Assert
      Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
      Assert.Equal("Success", resultValue.Message);
      Assert.True(resultValue.Success);
      Assert.Equal(HttpStatusCode.OK, resultValue.Data);
    }

    [Fact]
    public async Task DeleteTransport_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.DeleteTransport(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

      //Act
      Func<Task> act = () => transportController.DeleteTransport(It.IsAny<string>());

      //Assert
      var result = await Assert.ThrowsAsync<RestException>(act);
      Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
      Assert.Equal("Failed", result.Message);
    }

    [Fact]
    public async Task GetTransportByCarPlate_ResultSuccess()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetTransportByCarPlate(It.IsAny<string>())).ReturnsAsync(new TransportResponseApiModel { });

      //Act
      var result = await transportController.GetTransportByCarPlate(It.IsAny<string>()) as OkObjectResult;
      var resultValue = result.Value as TransportResponseApiModel;

      //Assert
      Assert.IsType<OkObjectResult>(result);
      Assert.IsType<TransportResponseApiModel>(resultValue);
    }

    [Fact]
    public async Task GetTransportByCarPlate_ResultFailed()
    {
      //Arrange
      _transportService.Setup(repo => repo.GetTransportByCarPlate(It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

      //Act
      Func<Task> act = () => transportController.GetTransportByCarPlate(It.IsAny<string>());

      //Assert
      var result = await Assert.ThrowsAsync<RestException>(act);
      Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
      Assert.Equal("Failed", result.Message);
    }
  }
}
