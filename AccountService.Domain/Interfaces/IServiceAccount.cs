using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        Task RegisterUser(RegisterApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        Task<PersonalDataApiModel> GetPersonalData(string userId);
    }
}
