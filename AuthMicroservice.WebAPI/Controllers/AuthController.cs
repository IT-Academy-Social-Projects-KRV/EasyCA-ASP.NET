using AuthMicroservice.Domain.Interfaces;
using AuthMicroservice.Domain.ApiModel.RequestApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serviceAccount;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService serviceAccount, IJwtService jwtService)
        {
            _serviceAccount = serviceAccount;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel data)
        {
            var response = await _jwtService.RefreshTokenAsync(data.RefreshToken);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterApiModel userRegisterRequest)
        {
            var response = await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok(response);
        }

        [HttpGet("ConfirmEmail/{token}/{email}")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _serviceAccount.ConfirmEmailAsync(email, token);

            return Ok(result);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordApiModel data)
        {
            var response = await _serviceAccount.ForgotPassword(data);

            return Ok(response);
        }

        [HttpGet("RestorePassword/{password}/{token}/{email}")]
        public async Task<IActionResult> RestorePassword(string password, string token, string email)
        {
            var response = await _serviceAccount.RestorePassword(password, token, email);

            return Ok(response);
        }

        [HttpPost("ResendConfirmation")]
        public async Task<IActionResult> ResendConfirmation(ResendConfirmationApiModel data)
        {
            var response = await _serviceAccount.ResendConfirmation(data);

            return Ok(response);
        }
    }
}
