using AutoMapper;
using ProtocolService.Data;
using ProtocolService.Data.Entities;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;
using ProtocolService.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;

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
            var euroProtocol = _mapper.Map<EuroProtocolRequestModel, EuroProtocol> (data, new EuroProtocol());
            await _context.EuroProtocols.InsertOneAsync(euroProtocol);
            
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Creating EuroProtocol is success!");
        }
    }
}
