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
using CrudMicroservice.Domain.Properties;

namespace CrudMicroservice.Domain.Services
{
    public class CarAccidentService: ICarAccidentService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CarAccident> _carAccidentProtocols;
        
        public CarAccidentService(IMapper mapper, IGenericRepository<CarAccident> carAccidentProtocols)
        {
            _mapper = mapper;
            _carAccidentProtocols = carAccidentProtocols;
        }
        public async Task<ResponseApiModel<HttpStatusCode>> RegistrationCarAccidentProtocol(CarAccidentRequestApiModel data)
        {
            var carAccidentProtocol = _mapper.Map<CarAccident>(data);
            await _carAccidentProtocols.CreateAsync(carAccidentProtocol);
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("CAProtocolCreatedSuccess"));
        }
        public async Task<IEnumerable<CarAccidentResponseApiModel>> FindAllCarAccidentProtocolsByInvolvedEmail(string email)
        {
            var carAccidentProtocols = await _carAccidentProtocols.GetAllByFilterAsync(x => x.InvolvedEmail == email);
            if (carAccidentProtocols == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("CAprotocolNotFound"));
            }
            var mappedCAProtocols = _mapper.Map<IEnumerable<CarAccident>, IEnumerable<CarAccidentResponseApiModel>>(carAccidentProtocols);
            return mappedCAProtocols;
        }
        public async Task<ResponseApiModel<HttpStatusCode>> UpdateCarAccidentProtocol(CarAccidentRequestApiModel data)
        {
            var mapped = _mapper.Map<CarAccident>(data);
            var update = Builders<CarAccident>.Update
                .Set(x => x.Address, mapped.Address)
                .Set(x => x.Witnesses, mapped.Witnesses)
                .Set(x => x.Evidences, mapped.Evidences)
                .Set(x => x.IsDocumentTakenOff, mapped.IsDocumentTakenOff)
                .Set(x => x.CourtDTG, mapped.CourtDTG);
            var result = await _carAccidentProtocols.UpdateAsync(x => x.SerialNumber == data.SerialNumber, update);
            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("CAProtocolUpdatefailed"));
            }
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("CAProtocolUpdateSuccess"));
        }
    }
}
