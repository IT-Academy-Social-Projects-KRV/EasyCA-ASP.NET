using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using ProtocolService.Data.Entities;
using ProtocolService.Data.Interfaces;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;
using ProtocolService.Domain.Errors;
using ProtocolService.Domain.Interfaces;

namespace ProtocolService.Domain.Services
{
    public class ServiceProtocol : IServiceProtocol
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EuroProtocol> _euroProtocols;

        public ServiceProtocol(IMapper mapper, IGenericRepository<EuroProtocol> euroProtocol)
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

        public async Task<IEnumerable<EuroProtocolResponseModel>> FindAllProtocolWithEmail(string email)
        {
            var euroProtocols = await _euroProtocols.GetAllByFilterAsync(x => x.SideA.Email == email || x.SideB.Email == email);

            if (euroProtocols == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "EuroProtocolNotFound");
            }

            return _mapper.Map<IEnumerable<EuroProtocol>, IEnumerable<EuroProtocolResponseModel>>(euroProtocols);
        }
    }
}
