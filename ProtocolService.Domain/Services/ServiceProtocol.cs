﻿using AutoMapper;
using ProtocolService.Data;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;
using ProtocolService.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProtocolService.Domain.Errors;
using System.Collections.Generic;

namespace ProtocolService.Domain.Services
{
    public class ServiceProtocol : IServiceProtocol
    {
        private readonly ProtocolDbContext _context;
        private readonly IMapper _mapper;

        public ServiceProtocol(ProtocolDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data)
        {
            var euroProtocol = _mapper.Map<EuroProtocol> (data);
            await _context.EuroProtocols.InsertOneAsync(euroProtocol);            
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating EuroProtocol is success!");
        }      

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestModel data)
        {
            var filter = Builders<EuroProtocol>.Filter.Eq(c => c.SideB.Email, data.Email);
            var side = _mapper.Map<Side>(data);
            var update = Builders<EuroProtocol>.Update
                .Set(c => c.IsClosed, true)
                .Set(c => c.SideB, side);
            await _context.EuroProtocols.UpdateOneAsync(filter, update);
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating EuroProtocol is success!");
        }

        public async Task<List<EuroProtocolResponseModel>> FindProtocolWithEmail(string email)
        {
            var filter = Builders<EuroProtocol>.Filter.Eq(c => c.SideA.Email, email);
            var euroProtocols = await _context.EuroProtocols.Find(filter).ToListAsync();

            if (euroProtocols == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "EuroProtocolNotFound");
            }

            return _mapper.Map<List<EuroProtocolResponseModel>>(euroProtocols);
        }
    }
}
