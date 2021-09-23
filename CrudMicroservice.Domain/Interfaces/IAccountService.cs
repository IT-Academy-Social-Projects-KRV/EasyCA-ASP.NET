using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestModel data, string userId);
        Task<ResponseApiModel<HttpStatusCode>> UpdatePersonalData(PersonalDataRequestModel data, string userId);
        Task<PersonalDataResponseModel> GetPersonalData(string userId);
        Task<UserResponseModel> GetUserById(string userId);
        Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string password, string oldPassword, string userId);
        Task<UserResponseModel> GetUserByEmail(string email);
    }
}
