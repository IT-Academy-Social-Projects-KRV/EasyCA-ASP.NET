using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProtocolService.Domain.ApiModel.RequestApiModels;
using ProtocolService.Domain.ApiModel.ResponceApiModels;

namespace ProtocolService.Domain.Interfaces
{
    public interface IServiceProtocol
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data);
        Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestModel data);
        Task<IEnumerable<EuroProtocolResponseModel>> GetAllEuroProtocolsByEmail(string email);
    }
}
