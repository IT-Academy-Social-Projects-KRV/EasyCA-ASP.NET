using System.Threading.Tasks;
using AuthMicroservice.Data.Entities;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;

namespace AuthMicroservice.Domain.Interfaces
{
    public interface IJwtService
    {
        Task<string> CreateJwtToken(User user);
        Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token);
        RefreshToken CreateRefreshToken();
        bool RevokeToken(string token);
    }
}

