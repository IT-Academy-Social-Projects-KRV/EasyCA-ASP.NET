using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface ICarAccidentService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationCarAccidentProtocol(CarAccidentRequestModel data);
        Task<IEnumerable<EuroProtocolResponseModel>> FindAllCarAccidentProtocolsBySerial(string serial);
        Task<ResponseApiModel<HttpStatusCode>> UpdateCarAccidentProtocol(CarAccidentRequestModel data);
    }
}
