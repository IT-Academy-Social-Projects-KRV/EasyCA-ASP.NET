using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IEuroProtocolService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestModel data);
        Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestModel data);
        Task<IEnumerable<EuroProtocolSimpleResponseModel>> FindAllEuroProtocolsByEmail(string email);
        Task<ResponseApiModel<HttpStatusCode>> UpdateEuroProtocol(EuroProtocolRequestModel data);
        Task<IEnumerable<CircumstanceResponseModel>> GetAllCircumstances();
        Task<EuroProtocolFullResponseModel> GetEuroProtocolBySerialNumber(string serialNumber);
    }
}
