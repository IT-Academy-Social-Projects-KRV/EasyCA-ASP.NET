using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.Errors;

namespace CrudMicroservice.Domain.Services
{
    public class EuroProtocolService : IEuroProtocolService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EuroProtocol> _euroProtocols;

        public EuroProtocolService(IMapper mapper, IGenericRepository<EuroProtocol> euroProtocol)
        {
            _mapper = mapper;
            _euroProtocols = euroProtocol;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data)
        {
            var euroProtocol = _mapper.Map<EuroProtocol>(data);

            await _euroProtocols.CreateAsync(euroProtocol);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating EuroProtocol is success!");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestModel data)
        {
            var side = _mapper.Map<Side>(data);
            var update = Builders<EuroProtocol>.Update
                .Set(c => c.IsClosed, true)
                .Set(c => c.SideB, side);

            await _euroProtocols.UpdateAsync(c => c.SideB.Email == data.Email, update);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating EuroProtocol is success!");
        }

        public async Task<IEnumerable<EuroProtocolResponseModel>> FindAllEuroProtocolsByEmail(string email)
        {
            var euroProtocols = await _euroProtocols.GetAllByFilterAsync(x => x.SideA.Email == email || x.SideB.Email == email);

            if (euroProtocols == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "EuroProtocolNotFound");
            }

            var mappedProtocols = _mapper.Map<IEnumerable<EuroProtocol>, IEnumerable<EuroProtocolResponseModel>>(euroProtocols);

            return mappedProtocols;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateEuroProtocol(EuroProtocolRequestModel data)
        {
            if(data.IsClosed)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Euro Protocol is closed, you can`t change any information!");
            }

            var mapped = _mapper.Map<EuroProtocolRequestModel, EuroProtocol>(data);

            var update = Builders<EuroProtocol>.Update
                .Set(c => c.Address, mapped.Address)
                .Set(c => c.SideA, mapped.SideA)
                .Set(c => c.SideB, mapped.SideB)
                .Set(c => c.Witnesses, mapped.Witnesses);

            await _euroProtocols.UpdateAsync(x => x.SerialNumber == data.SerialNumber, update);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Update EuroProtocol is success!");
        }
    }
}
