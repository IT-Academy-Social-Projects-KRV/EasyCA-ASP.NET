using MassTransit;
using RabbitMQConfig.Models.Requests;
using RabbitMQConfig.Models.Responses;
using SearchMicroservice.Domain.Errors;
using SearchMicroservice.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace SearchMicroservice.Domain.Services
{
    public class SearchService : ISearchService
    {
        private readonly IClientFactory _clientFactory;

        public SearchService(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TransportResponseModelRabbitMQ> Search(string search)
        {
            var client = _clientFactory.CreateRequestClient<TransportRequestModelRabbitMQ>();
            var response = await client.GetResponse<TransportResponseModelRabbitMQ>(new TransportRequestModelRabbitMQ()
            {
                Filter = search
            });
            
            if(response.Message.Id==null)
            {
                throw new RestException(HttpStatusCode.BadRequest,"Wrong Data or transport not found");
            }

            return response.Message;
        }
    }
}
