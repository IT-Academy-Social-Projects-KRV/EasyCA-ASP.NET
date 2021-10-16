using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface ICarAccidentService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationCarAccidentProtocol(CarAccidentRequestApiModel data);
        Task<IEnumerable<CarAccidentResponseApiModel>> FindAllCarAccidentProtocolsByInvolvedEmail(string email);
        Task<ResponseApiModel<HttpStatusCode>> UpdateCarAccidentProtocol(CarAccidentRequestApiModel data);
    }
}
