using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface ITransportService
    {
        Task<ResponseApiModel<HttpStatusCode>> AddTransport(AddTransportRequestModel transportModel, string userId);
        Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId);
        Task<IEnumerable<TransportResponseApiModel>> GetAllTransports(string userId);
        Task<TransportResponseApiModel> GetTransportById(string transportId, string userId);
        Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId);
    }
}
