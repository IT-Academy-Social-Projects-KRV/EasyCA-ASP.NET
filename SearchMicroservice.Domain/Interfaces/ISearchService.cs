using RabbitMQConfig.Models.Responses;
using System.Threading.Tasks;

namespace SearchMicroservice.Domain.Interfaces
{
    public interface ISearchService
    {
        Task<TransportResponseModelRabbitMQ> Search(string search);
    }
}
