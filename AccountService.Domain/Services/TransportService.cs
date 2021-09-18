using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Data.Interfaces;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.Errors;
using AccountService.Domain.Interfaces;
using AccountService.Domain.Properties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace AccountService.Domain.Services
{
    public class TransportService : ITransportService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Transport> _transports;
        private readonly IGenericRepository<TransportCategory> _transportsCategories;


        public TransportService(IMapper mapper, UserManager<User> userManager, IGenericRepository<Transport> transports, IGenericRepository<TransportCategory> transportsCategories)
        {
            _mapper = mapper;
            _userManager = userManager;
            _transports = transports;
            _transportsCategories = transportsCategories;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> AddTransport(AddTransportRequestModel transportModel, string userId)
        {
            var filter = Builders<TransportCategory>.Filter.Where(x => x.CategoryName == transportModel.CategoryName);
            var carCategory = await _transportsCategories.GetByFilterAsync(filter);

            if (carCategory == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportCategoryNotFound"));
            }

            var transport = _mapper.Map<Transport>(transportModel);
            transport.CarCategory = carCategory;
            transport.UserId = userId;

            await _transports.CreateAsync(transport);

            var user = await _userManager.FindByIdAsync(userId);
            user.UserData.UserCars.Add(transport.Id);

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportAddingSucceeded"));
        }

        public async Task<IEnumerable<TransportResponseApiModel>> GetAllTransports(string userId)
        {
            var filter = Builders<Transport>.Filter.Eq(c => c.UserId, userId);
            var transports = await _transports.GetAllAsyncByFilter(filter);

            if (transports == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }
            return _mapper.Map<IEnumerable<TransportResponseApiModel>>(transports);
        }

        public async Task<TransportResponseApiModel> GetTransportById(string transportId, string userId)
        {
            var filter = Builders<Transport>.Filter.Where(x => x.UserId == userId && x.Id == transportId);
            var transport = await _transports.GetByFilterAsync(filter);

            if (transport == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return _mapper.Map<TransportResponseApiModel>(transport);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId)
        {
            var filter = Builders<Transport>.Filter.Eq(c => c.Id, transportModel.Id);
            var categoryFilter = Builders<TransportCategory>.Filter.Where(x => x.CategoryName == transportModel.CategoryName);

            var carCategory = await _transportsCategories.GetByFilterAsync(categoryFilter);

            if (carCategory == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportCategoryNotFound"));
            }

            var update = Builders<Transport>.Update
                   .Set(c => c.UserId, userId)
                   .Set(c => c.ProducedBy, transportModel.ProducedBy)
                   .Set(c => c.Model, transportModel.Model)
                   .Set(c => c.CarCategory, carCategory)
                   .Set(c => c.VINCode, transportModel.VINCode)
                   .Set(c => c.CarPlate, transportModel.CarPlate)
                   .Set(c => c.Color, transportModel.Color)
                   .Set(c => c.YearOfProduction, transportModel.YearOfProduction)
                   .Set(c => c.InsuaranceNumber, transportModel.InsuaranceNumber);

            var result = _transports.UpdateAsync(filter, update);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportUpdatingSucceeded"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId)
        {
            var filter = Builders<Transport>.Filter.Where(x => x.UserId == userId && x.Id == transportId);
            var result = _transports.DeleteAsync(filter);

            if (result == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportDeleteFailed"));
            }

            var user = await _userManager.FindByIdAsync(userId);
            user.UserData.UserCars.Remove(transportId);

            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("TransportDeleteSucceeded"));
        }
    }
}
