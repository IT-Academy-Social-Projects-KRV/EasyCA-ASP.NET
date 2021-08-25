using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.ApiModel.ResponseApiModels;

namespace AccountService.Domain.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(User user);
        Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token);
        RefreshToken CreateRefreshToken();
        bool RevokeToken(string token);
    }
}

