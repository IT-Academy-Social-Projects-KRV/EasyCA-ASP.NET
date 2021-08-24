using AccountService.Data.Entities;
using AspNetCore.Identity.MongoDbCore.Models;
using System.Text.Json.Serialization;

namespace AccountService.Domain.ResponseModels
{
    public class AuthenticateResponse
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }

        //[JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Email = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
