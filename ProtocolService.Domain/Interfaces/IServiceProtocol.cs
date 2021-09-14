using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;
using System.Net;
using System.Threading.Tasks;

namespace ProtocolService.Domain.Interfaces
{
    public interface IServiceProtocol
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data);
    }
}
