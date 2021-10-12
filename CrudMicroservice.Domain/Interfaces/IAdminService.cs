using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<EuroProtocolResponseModel>> GetAllEuroProtocols();
        Task<IEnumerable<UserResponseModel>> GetAllInspectors();
        Task<ResponseApiModel<HttpStatusCode>> DeleteInspector(string email);
    }
}
