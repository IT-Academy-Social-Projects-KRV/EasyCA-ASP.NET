using System.Threading.Tasks;
using AuthMicroservice.Data.Entities;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;

namespace AuthMicroservice.Domain.Interfaces
{
    public interface IJwtService
    {
        string CreateJwtToken(User user);
        Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token);
        RefreshToken CreateRefreshToken();
        bool RevokeToken(string token);
    }
}

