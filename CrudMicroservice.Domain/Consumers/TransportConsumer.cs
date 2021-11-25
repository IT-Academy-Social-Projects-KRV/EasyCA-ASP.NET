using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.Errors;
using CrudMicroservice.Domain.Properties;
using MassTransit;
using RabbitMQConfig.Models.Requests;
using RabbitMQConfig.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Consumers
{
    public class TransportConsumer : IConsumer<TransportRequestModelRabbitMQ>
    {
        private readonly IGenericRepository<Transport> _transportRepository;
        private readonly IGenericRepository<EuroProtocol> _euroProtocolRepository;
        private readonly IGenericRepository<CarAccident> _carAccidentRepository;
        private readonly IMapper _mapper;

        public TransportConsumer(IGenericRepository<Transport> transportRepository, IMapper mapper, IGenericRepository<EuroProtocol> euroProtocolRepository, IGenericRepository<CarAccident> carAccidentRepository)
        {
            _transportRepository = transportRepository;
            _euroProtocolRepository = euroProtocolRepository;
            _carAccidentRepository = carAccidentRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TransportRequestModelRabbitMQ> context)
        {
            var filter = context.Message.Filter;

            Transport transport = new Transport();

            Regex regVin = new Regex(@"^(([a-h,A-H,j-n,J-N,p-z,P-Z,0-9]{9})([a-h,A-H,j-n,J-N,p,P,r-t,R-T,v-z,V-Z,0-9])([a-h,A-H,j-n,J-N,p-z,P-Z,0-9])(\d{6}))$");
            Regex regCarPlate = new Regex(@"^[АВСРІ]{1}[АIВСЕНКМТРХОЄ]{1}\d{4}[А-Я]{2}$");

            TransportResponseModelRabbitMQ responseTransport = new TransportResponseModelRabbitMQ();

            if (regVin.IsMatch(filter))
            {
                transport = await _transportRepository.GetByFilterAsync(x => x.VINCode == filter);
            }
            else if (regCarPlate.IsMatch(filter))
            {
                transport = await _transportRepository.GetByFilterAsync(x => x.CarPlate == filter);
            }

            if (transport.Id != null)
            {
                var epList = await _euroProtocolRepository.GetAllByFilterAsync(x => x.SideA.TransportId == transport.Id || x.SideB.TransportId == transport.Id);
                var caList = await _carAccidentRepository.GetAllByFilterAsync(x => x.SideOfAccident.TransportId == transport.Id);

                responseTransport = _mapper.Map<TransportResponseModelRabbitMQ>(transport);
                responseTransport.CarAccidentsRegistered = epList.Count + caList.Count;
            }

            await context.RespondAsync(responseTransport);
        }
    }
}
