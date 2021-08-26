using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AccountService.Data.Entities;
using AccountService.Domain.Interfaces;
using AccountService.Domain.ApiModel.RequestApiModels;
using AccountService.Domain.ApiModel.ResponseApiModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AccountService.Domain.Services
{
    public class ServiceAccount : IServiceAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public ServiceAccount(UserManager<User> userManager,SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task RegisterUser(RegisterApiModel userRequest)
        {
            User user = new User()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                UserName = userRequest.Email,
            };

            var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (result.Succeeded)
            {
                if (user.UserData.ServiceId == null)
                {
                    await _userManager.AddToRoleAsync(user, "participant");
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "inspector");
                }
            }
            else
            {
                throw new ArgumentException("Result error");
            }         
        }
        public async Task<AuthenticateResponseApiModel> LoginUser(LoginApiModel userRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password,false,false);
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            
            if (user == null)
            {
                return null;
            }
            
            var token = CreateJwtToken(user);
            var refreshtoken = CreateRefreshToken();
            user.RefreshToken = refreshtoken;
            await _userManager.UpdateAsync(user);
            await _signInManager.SignInAsync(user, false);
            return new AuthenticateResponseApiModel(user.Email, token, refreshtoken.Token);
        }        
        private string CreateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("TokenExpires")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshToken.Token == token);
            var refreshToken = user.RefreshToken;
            
            if (!refreshToken.IsActive)
            {
                return null;
            }
            
            var newRefreshToken = CreateRefreshToken();
            refreshToken.Revoked = DateTime.UtcNow;
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            var JWTToken = CreateJwtToken(user);
            return new AuthenticateResponseApiModel(user.Email, JWTToken, newRefreshToken.Token);            
        }        
        private RefreshToken CreateRefreshToken()
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
            // return false if no user found with token
            if (user == null) return false;
            var refreshToken = user.RefreshToken;
            // return false if token is not active
            if (!refreshToken.IsActive) return false;
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _userManager.UpdateAsync(user);
            return true;            
        }
    }
}
