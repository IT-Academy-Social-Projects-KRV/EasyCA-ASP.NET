using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using AccountService.Domain.ModelDTO.EntitiesDTO;

namespace AccountService.Domain.Interfaces
{
    public interface ITransportService
    {
        Task<ResponseApiModel<HttpStatusCode>> AddTransort(AddTransportRequestModel transportModel, string userId);
        Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId);
        Task<IEnumerable<TransportDTO>> GetAllTransports(string userId);
        Task<TransportDTO> GetTransportById(string transportId, string userId);
        Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId);
    }
}
