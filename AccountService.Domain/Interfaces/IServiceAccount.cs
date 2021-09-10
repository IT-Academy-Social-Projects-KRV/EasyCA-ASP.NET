using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(UserRequestModel data, string userId);
        Task<PersonalDataResponseModel> GetPersonalData(string userId);
        Task<UserResponseModel> GetUserById(string userId);
        Task<ResponseApiModel<HttpStatusCode>> CreatePersonalData(PersonalDataRequestModel data, string userId);
        Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string userId, string token);
        Task<ResponseApiModel<HttpStatusCode>> ChangePassword(string password,string oldPassword, string userId);
    }
}
