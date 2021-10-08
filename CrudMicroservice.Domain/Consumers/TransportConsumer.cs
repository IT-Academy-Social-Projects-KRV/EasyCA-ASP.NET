using AutoMapper;
using CrudMicroservice.Data.Entities;
using CrudMicroservice.Data.Interfaces;
using CrudMicroservice.Domain.Errors;
using MassTransit;
using RabbitMQConfig.Models.Requests;
using RabbitMQConfig.Models.Responses;
using System.Net;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Consumers
{
    public class TransportConsumer : IConsumer<TransportRequestModelRabbitMQ>
    {
        private readonly IGenericRepository<Transport> _transportRepository;
        private readonly IMapper _mapper;
        public TransportConsumer(IGenericRepository<Transport> transportRepository, IMapper mapper)
        {
            _transportRepository = transportRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TransportRequestModelRabbitMQ> context)
        {
            var filter = context.Message.Filter; 

            var transport = await _transportRepository.GetByFilterAsync(x => x.VINCode == filter);
            if (transport == null) { throw new RestException(HttpStatusCode.BadRequest, "You entered wrong data");  }

            //if (filter.Length.Equals(17)) 
            //{
            //    transport = await _transportRepository.GetByFilterAsync(x => x.VINCode == filter);

            //}
            //if(filter.Length.Equals(8))
            //{ 
            //    transport = await _transportRepository.GetByFilterAsync(x => x.CarPlate == filter);
            //}
            //else 
            //{
            //    throw new RestException(HttpStatusCode.BadRequest, "You entered wrong data");
            //}

            var responseTransport = _mapper.Map<TransportResponseModelRabbitMQ>(transport);
            if (responseTransport == null) { throw new RestException(HttpStatusCode.BadRequest, "wrong data"); }
            await context.RespondAsync(responseTransport);
        }
    }
}
