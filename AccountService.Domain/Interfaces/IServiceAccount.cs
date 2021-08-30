using System.Net;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        Task RegisterUser(RegisterApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        public Task<ResponseApiModel<HttpStatusCode>> UpdateUserData(PersonalDataApiModel data, string userId);
        Task<PersonalDataApiModel> GetPersonalData(string userId);
    }
}
