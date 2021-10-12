using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IEuroProtocolService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationEuroProtocol(EuroProtocolRequestApiModel data);
        Task<ResponseApiModel<HttpStatusCode>> RegisterSideBEuroProtocol(SideRequestApiModel data);
        Task<IEnumerable<EuroProtocolSimpleResponseApiModel>> FindAllEuroProtocolsByEmail(string email);
        Task<ResponseApiModel<HttpStatusCode>> UpdateEuroProtocol(EuroProtocolRequestApiModel data);
        Task<IEnumerable<CircumstanceResponseApiModel>> GetAllCircumstances();
        Task<EuroProtocolFullResponseApiModel> GetEuroProtocolBySerialNumber(string serialNumber);
    }
}
