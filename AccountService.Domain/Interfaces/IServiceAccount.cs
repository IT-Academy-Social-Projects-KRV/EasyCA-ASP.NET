using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.RequestModels;
using AccountService.Domain.ResponseModels;

namespace AccountService.Domain.Interfaces
{
    public interface IServiceAccount
    {
        public Task RegisterUser(UserRegisterRequest user);
        public Task<AuthenticateResponse> LoginUser(UserLoginRequest userRequest);
        public Task<AuthenticateResponse> RefreshTokenAsync(string token);
        public bool RevokeToken(string token);

    }
}
