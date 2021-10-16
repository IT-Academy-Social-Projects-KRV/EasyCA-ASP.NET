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
    public class CarAccidentService: ICarAccidentService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CarAccident> _carAccidentProtocols;
        public CarAccidentService(IMapper mapper, IGenericRepository<CarAccident> carAccidentProtocols)
        {
            _mapper = mapper;
            _carAccidentProtocols = carAccidentProtocols;
        }
        public async Task<ResponseApiModel<HttpStatusCode>> RegistrationCarAccidentProtocol(CarAccidentRequestModel data)
        { 
        
        }
        public async Task<IEnumerable<EuroProtocolResponseModel>> FindAllCarAccidentProtocolsBySerial(string serial)
        { 
        
        }
        public async Task<ResponseApiModel<HttpStatusCode>> UpdateCarAccidentProtocol(CarAccidentRequestModel data)
        { 
        
        }
    }
}
