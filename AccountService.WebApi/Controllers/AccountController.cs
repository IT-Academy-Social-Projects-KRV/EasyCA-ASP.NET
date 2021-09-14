using AccountService.Domain.Interfaces;
using AccountService.Domain.ApiModel.RequestApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using AccountService.Domain.ApiModel.ResponseApiModels;
using Microsoft.AspNetCore.Authorization;

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
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel data)
        {
            var response = await _jwtService.RefreshTokenAsync(data.RefreshToken);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterApiModel userRegisterRequest)
        {
            var response = await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok(response);
        }

        [HttpPut("UpdateData")]
        public async Task<IActionResult> UpdateUserData(UserRequestModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _serviceAccount.UpdateUserData(data, userId);
            
            return Ok(response);
        }
        
        [HttpGet("GetPersonalData")]
        public async Task<IActionResult> GetPersonalData()
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _serviceAccount.GetPersonalData(userId);

            return Ok(response);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _serviceAccount.GetUserById(userId);
            
            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _serviceAccount.GetUserById(id);
            
            return Ok(response);
        }

        [HttpPost("CreatePersonalData")]
        public async Task<IActionResult> CreatePersonalData(PersonalDataRequestModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _serviceAccount.CreatePersonalData(data, userId);

            return Ok(response);
        }

        [HttpGet("ConfirmEmail/{token}/{email}")]
        public async Task<IActionResult> ConfirmEmail(string email,string token)
        {
            var result = await _serviceAccount.ConfirmEmailAsync(email, token);

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordApiModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var responce = await _serviceAccount.ChangePassword(data.Password, data.OldPassword, userId);

            return Ok(responce);

        }
    }
}
