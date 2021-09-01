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
using MongoDB.Driver;

namespace AccountService.Domain.Services
{
    public class TransportService : ITransportService
    {
        private readonly ApplicationDbContext _context;

        public TransportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> AddTransort(AddTransportRequestModel transportModel, string userId)
        {
            var carCategory = await _context.TransportCategories.Find(x => x.CategoryName == transportModel.CategoryName).FirstOrDefaultAsync();

            if (carCategory == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportCategoryNotFound"));
            }

            Transport transport = new Transport
            {
                UserId = userId,
                ProducedBy = transportModel.ProducedBy,
                Model = transportModel.Model,
                CarCategory = carCategory,
                VINCode = transportModel.VINCode,
                CarPlate = transportModel.CarPlate,
                Color = transportModel.Color,
                YearOfProduction = transportModel.YearOfProduction,
                InsuaranceNumber = transportModel.InsuaranceNumber
            };

            await _context.Transports.InsertOneAsync(transport);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.Created, true, Resources.ResourceManager.GetString("TransportAddingSucceeded"));
        }

        public async Task<IEnumerable<Transport>> GetAllTransports(string userId)
        {
            var filter = Builders<Transport>.Filter.Eq(c => c.UserId, userId);
            var transports = await _context.Transports.Find(filter).ToListAsync();

            if (transports == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return transports;
        }

        public async Task<Transport> GetTransportById(string transportId, string userId)
        {
            var filter = Builders<Transport>.Filter.Where(x => x.UserId == userId && x.Id == transportId);
            var transport = await _context.Transports.Find(filter).SingleOrDefaultAsync();

            if (transport == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportsNotFound"));
            }

            return transport;
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

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("TransportDeleteSucceeded"));
        }
    }
}
