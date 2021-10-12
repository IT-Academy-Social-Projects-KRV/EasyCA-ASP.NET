using System.Net;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;

namespace CrudMicroservice.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestApiModel data, string userId);
        Task<ResponseApiModel<HttpStatusCode>> UpdatePersonalData(UserRequestApiModel data, string userId);
        Task<PersonalDataResponseApiModel> GetPersonalData(string userId);
        Task<UserResponseApiModel> GetUserById(string userId);
        Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string password, string oldPassword, string userId);
        Task<UserResponseApiModel> GetUserByEmail(string email);
    }
}
