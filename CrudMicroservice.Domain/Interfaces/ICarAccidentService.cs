using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface ICarAccidentService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegistrationCarAccidentProtocol(CarAccidentRequestApiModel data, string inspectorId);
        Task<IEnumerable<CarAccidentResponseApiModel>> FindAllCarAccidentProtocolsByInvolvedId(string inspectorId);
        Task<ResponseApiModel<HttpStatusCode>> UpdateCarAccidentProtocol(CarAccidentRequestApiModel data);
        Task<IEnumerable<CarAccidentResponseApiModel>> FindAllPersonsCAProtocolsForInspector(string personDriverId);
    }
}
