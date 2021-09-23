using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthMicroservice.Data.Entities;
using AuthMicroservice.Domain.ApiModel.ResponseApiModels;
using AuthMicroservice.Domain.Errors;
using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.Properties;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroservice.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public string CreateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"));

            var role = user.Roles.FirstOrDefault();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id",user.Id.ToString()),
                    new Claim("Role",role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("TokenExpires")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "http://localhost:5000",
                IssuedAt = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken.Token == token);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserRefreshTokenNotFound"));
            }

            var refreshToken = user.RefreshToken;

            if (!refreshToken.IsActive)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserRefreshTokenNotActive"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newRefreshToken = CreateRefreshToken();
            refreshToken.Revoked = DateTime.UtcNow;
            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            var JWTToken = CreateJwtToken(user);

            return new AuthenticateResponseApiModel(user.Email, JWTToken, newRefreshToken.Token, roles.FirstOrDefault());
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<double>("RefreshTokenExpires")),
                    Created = DateTime.UtcNow
                };
            }
        }

        public bool RevokeToken(string token)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken.Token == token);

            if (user == null)
            {
                return false;
            }

            var refreshToken = user.RefreshToken;

            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.Revoked = DateTime.UtcNow;
            _userManager.UpdateAsync(user);

            return true;
        }
    }
}
