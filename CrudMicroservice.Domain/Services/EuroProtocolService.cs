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
using Microsoft.AspNetCore.Identity;

namespace CrudMicroservice.Domain.Services
{
    public class EuroProtocolService : IEuroProtocolService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EuroProtocol> _euroProtocols;
        private readonly IGenericRepository<Circumstance> _circumstances;
        private readonly IGenericRepository<Transport> _transports;
        private readonly IGenericRepository<PersonalData> _personalDatas;
        private readonly UserManager<User> _userManager;

        public EuroProtocolService(IMapper mapper, IGenericRepository<EuroProtocol> euroProtocol, IGenericRepository<Circumstance> circumstances,
            IGenericRepository<Transport> transports, IGenericRepository<PersonalData> personalDatas, UserManager<User> userManager)
        {
            _mapper = mapper;
            _euroProtocols = euroProtocol;
            _circumstances = circumstances;
            _transports = transports;
            _personalDatas = personalDatas;
            _userManager = userManager;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data)
        {
            var euroProtocol = _mapper.Map<EuroProtocol>(data);

            await _euroProtocols.CreateAsync(euroProtocol);

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("SuccessfulCreatingOfEuroProtocol"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestModel data)
        {
            var side = _mapper.Map<Side>(data);
            var update = Builders<EuroProtocol>.Update
                .Set(c => c.IsClosed, true)
                .Set(c => c.SideB, side);

            var result = await _euroProtocols.UpdateAsync(c => c.SerialNumber == data.ProtocolSerial, update);
            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("SideBIsNotRegistered"));
            }
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("SideBRegistrationSuccess"));
        }

        public async Task<IEnumerable<EuroProtocolSimpleResponseModel>> FindAllEuroProtocolsByEmail(string email)
        {
            var euroProtocols = await _euroProtocols.GetAllByFilterAsync(x => x.SideA.Email == email || x.SideB.Email == email);

            if (euroProtocols == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("EuroProtocolNotFound"));
            }

            var mappedProtocols = _mapper.Map<IEnumerable<EuroProtocol>, IEnumerable<EuroProtocolSimpleResponseModel>>(euroProtocols);

            return mappedProtocols;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateEuroProtocol(EuroProtocolRequestModel data)
        {
            if (data.IsClosed)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("EuroProtocolIsClosed"));
            }

            var mapped = _mapper.Map<EuroProtocol>(data);

            var update = Builders<EuroProtocol>.Update
                .Set(c => c.Address, mapped.Address)
                .Set(c => c.SideA, mapped.SideA)
                .Set(c => c.SideB, mapped.SideB)
                .Set(c => c.Witnesses, mapped.Witnesses)
                .Set(c => c.RegistrationDateTime, data.RegistrationDateTime)
                .Set(c => c.IsClosed, data.IsClosed);

            var result = await _euroProtocols.UpdateAsync(x => x.SerialNumber == data.SerialNumber, update);
            if (!result.IsAcknowledged)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("ProtocolIsNotUpdated!"));
            }

            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EuroProtocolUpdated"));
        }

        public async Task<IEnumerable<CircumstanceResponseModel>> GetAllCircumstances()
        {
            var circumstances = await _circumstances.GetAllAsync();

            if (circumstances == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "CircumstanceslNotFound");
            }

            return _mapper.Map<IEnumerable<Circumstance>, IEnumerable<CircumstanceResponseModel>>(circumstances);
        }

        public async Task<EuroProtocolFullResponseModel> GetEuroProtocolBySerialNumber(string serialNumber)
        {
            var euroProtocol = await _euroProtocols.GetByFilterAsync(x => x.SerialNumber == serialNumber);

            if (euroProtocol == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("EuroProtocolNotFound"));
            }

            var mappedEuroProtocolSimpleResponseModel = _mapper.Map<EuroProtocolSimpleResponseModel>(euroProtocol);

            var userSideA = await _userManager.FindByEmailAsync(euroProtocol.SideA.Email);
            var userSideB = await _userManager.FindByEmailAsync(euroProtocol.SideB.Email);

            if (userSideA == null || userSideB == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var mappedUserResponseModelA = _mapper.Map<UserResponseModel>(userSideA);
            var mappedUserResponseModelB = _mapper.Map<UserResponseModel>(userSideB);

            var personalDataUserA = await _personalDatas.GetByFilterAsync(p => p.Id == userSideA.PersonalDataId);
            var personalDataUserB = await _personalDatas.GetByFilterAsync(p => p.Id == userSideB.PersonalDataId);

            mappedUserResponseModelA.PersonalData = _mapper.Map<PersonalDataResponseModel>(personalDataUserA);
            mappedUserResponseModelB.PersonalData = _mapper.Map<PersonalDataResponseModel>(personalDataUserB);

            var transportSideA = await _transports.GetByFilterAsync(t => t.Id == euroProtocol.SideA.TransportId);
            var transportSideB = await _transports.GetByFilterAsync(t => t.Id == euroProtocol.SideB.TransportId);

            if (transportSideA == null || transportSideB == null)
            {
                throw new RestException(HttpStatusCode.NotFound, Resources.ResourceManager.GetString("TransportNotFound"));
            }

            var mappedTransportResponseModelA = _mapper.Map<TransportResponseApiModel>(transportSideA);
            var mappedTransportResponseModelB = _mapper.Map<TransportResponseApiModel>(transportSideB);

            var response = new EuroProtocolFullResponseModel
            {
                EuroProtocolSimple = mappedEuroProtocolSimpleResponseModel,
                UserDataSideA = mappedUserResponseModelA,
                UserDataSideB = mappedUserResponseModelB,
                TransportSideA = mappedTransportResponseModelA,
                TransportSideB = mappedTransportResponseModelB,
                Witnesses = euroProtocol.Witnesses
            };

            return response;
        }
    }
}
