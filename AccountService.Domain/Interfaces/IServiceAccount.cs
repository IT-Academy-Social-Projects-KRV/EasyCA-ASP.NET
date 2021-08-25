using System.Threading.Tasks;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        public Task RegisterUser(RegisterApiModel user);
        public Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest);
        public Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token);
        public bool RevokeToken(string token);
    }
}
