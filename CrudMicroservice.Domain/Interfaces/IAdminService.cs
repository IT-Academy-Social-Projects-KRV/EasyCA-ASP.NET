using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<EuroProtocolResponseApiModel>> GetAllEuroProtocols();
        Task<IEnumerable<UserResponseApiModel>> GetAllInspectors();
        Task<ResponseApiModel<HttpStatusCode>> AddInspector(InspectorRequestApiModel inspectorRequest);
        Task<ResponseApiModel<HttpStatusCode>> DeleteInspector(DeleteInspectorRequestApiModel data);
    }
}
