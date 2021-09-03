﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccountService.Data;
using AccountService.Data.Entities;
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public TransportService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> AddTransport(AddTransportRequestModel transportModel, string userId)
        {
            var carCategory = await _context.TransportCategories.Find(x => x.CategoryName == transportModel.CategoryName).FirstOrDefaultAsync();

            if (carCategory == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportCategoryNotFound"));
            }

            var transport = _mapper.Map<Transport>(transportModel);
            transport.CarCategory = carCategory;
            transport.UserId = userId;

            await _context.Transports.InsertOneAsync(transport);

            var user = await _userManager.FindByIdAsync(userId);

            user.UserData.UserCars.Add(transport.Id);
            await _userManager.UpdateAsync(user);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportAddingSucceeded"));
        }

        public async Task<IEnumerable<TransportResponseApiModel>> GetAllTransports(string userId)
        {
            var filter = Builders<Transport>.Filter.Eq(c => c.UserId, userId);
            var transports = await _context.Transports.Find(filter).ToListAsync();

            if (transports == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }
            return _mapper.Map<IEnumerable<TransportResponseApiModel>>(transports);
        }

        public async Task<TransportResponseApiModel> GetTransportById(string transportId, string userId)
        {
            var filter = Builders<Transport>.Filter.Where(x => x.UserId == userId && x.Id == transportId);
            var transport = await _context.Transports.Find(filter).SingleOrDefaultAsync();

            if (transport == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return _mapper.Map<TransportResponseApiModel>(transport);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId)
        {
            var filter = Builders<Transport>.Filter.Eq(c => c.Id, transportModel.Id);
            var carCategory = await _context.TransportCategories.Find(x => x.CategoryName == transportModel.CategoryName).FirstOrDefaultAsync();

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

            var result = await _context.Transports.UpdateOneAsync(filter, update);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportUpdatingSucceeded"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId)
        {
            var filter = Builders<Transport>.Filter.Where(x => x.UserId == userId && x.Id == transportId);
            var result = await _context.Transports.DeleteOneAsync(filter);

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
