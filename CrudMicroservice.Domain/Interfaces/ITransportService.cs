using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface ITransportService
    {
        Task<ResponseApiModel<HttpStatusCode>> AddTransport(AddTransportRequestModel transportModel, string userId);
        Task<ResponseApiModel<HttpStatusCode>> UpdateTransport(UpdateTransportRequestModel transportModel, string userId);
        Task<IEnumerable<TransportResponseApiModel>> GetAllTransports(string userId);
        Task<TransportResponseApiModel> GetTransportById(string transportId, string userId);
        Task<ResponseApiModel<HttpStatusCode>> DeleteTransport(string transportId, string userId);
        Task<TransportResponseApiModel> GetTransportByCarPlate(string carPlate);
    }
}
