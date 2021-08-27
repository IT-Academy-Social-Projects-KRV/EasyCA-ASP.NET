using AccountService.Domain.Interfaces;
using AccountService.Domain.ApiModel.RequestApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceAccount _serviceAccount;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        public AccountController(IServiceAccount serviceAccount, IConfiguration configuration, IJwtService jwtService)
        {
            _serviceAccount = serviceAccount;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _jwtService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<double>("RefreshTokenExpires")),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterApiModel userRegisterRequest)
        {
            await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok();
        }

        [HttpPut("UpdateData")]
        public async Task<IActionResult> UpdateUserData(PersonalDataApiModel data)
        {
            var userId = User.FindFirst("Id").Value;
            var response = await _serviceAccount.UpdateUserData(data, userId);
            return Ok(response);
        }
    }
}
