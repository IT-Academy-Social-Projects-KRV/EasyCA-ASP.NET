using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.Properties;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace CrudMicroservice.Domain.Services
{
    public class TransportService : ITransportService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Transport> _transports;
        private readonly IGenericRepository<TransportCategory> _transportsCategories;
        private readonly IGenericRepository<PersonalData> _personalData;

        public TransportService(IMapper mapper, UserManager<User> userManager, IGenericRepository<Transport> transports, IGenericRepository<TransportCategory> transportsCategories,
            IGenericRepository<PersonalData> personalData)
        {
            _mapper = mapper;
            _userManager = userManager;
            _transports = transports;
            _transportsCategories = transportsCategories;
            _personalData = personalData;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> AddTransport(AddTransportRequestModel transportModel, string userId)
        {
            var carCategory = await _transportsCategories.GetByFilterAsync(x => x.CategoryName == transportModel.CategoryName);

            if (carCategory == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportCategoryNotFound"));
            }

            var transport = _mapper.Map<Transport>(transportModel);
            transport.CarCategory = carCategory;
            transport.UserId = userId;

            await _transports.CreateAsync(transport);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var personalData = await _personalData.GetByFilterAsync(x => x.Id == user.PersonalDataId);

            if (personalData == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("PersonalDataNotFound"));
            }

            personalData.UserCars.Add(transport.Id);

            var update = Builders<PersonalData>.Update
                .Set(c => c.UserCars, personalData.UserCars);

            var result = await _personalData.UpdateAsync(x => x.Id == personalData.Id, update);

            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("PersonalDataWrongUpdated"));//resources
            }

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportAddingSucceeded"));
        }

        public async Task<IEnumerable<TransportResponseApiModel>> GetAllTransports(string userId)
        {
            var transports = await _transports.GetAllByFilterAsync(c => c.UserId == userId);

            if (transports == null || !transports.Any())
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return _mapper.Map<IEnumerable<TransportResponseApiModel>>(transports);
        }

        public async Task<TransportResponseApiModel> GetTransportById(string transportId, string userId)
        {
            var transport = await _transports.GetByFilterAsync(x => x.UserId == userId && x.Id == transportId);

            if (transport == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return _mapper.Map<TransportResponseApiModel>(transport);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId)
        {
            var carCategory = await _transportsCategories.GetByFilterAsync(x => x.CategoryName == transportModel.CategoryName);

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

            var result = await _transports.UpdateAsync(c => c.Id == transportModel.Id, update);

            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("PersonalDataWrongUpdated"));//resources
            }

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportUpdatingSucceeded"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId)
        {
            var result =await _transports.DeleteAsync(x => x.UserId == userId && x.Id == transportId);

            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportDeleteFailed"));
            }

            var user = await _userManager.FindByIdAsync(userId);
            var personalData = await _personalData.GetByFilterAsync(x => x.Id == user.PersonalDataId);

            personalData.UserCars.Remove(transportId);

            var update = Builders<PersonalData>.Update
                 .Set(c => c.UserCars, personalData.UserCars);

            await _personalData.UpdateAsync(x => x.Id == personalData.Id, update);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("TransportDeleteSucceeded"));
        }

        public async Task<TransportResponseApiModel> GetTransportByCarPlate(string carPlate)
        {
            var transport = await _transports.GetByFilterAsync(x => x.CarPlate == carPlate.ToUpper());

            if (transport == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return _mapper.Map<TransportResponseApiModel>(transport);
        }
    }
}
