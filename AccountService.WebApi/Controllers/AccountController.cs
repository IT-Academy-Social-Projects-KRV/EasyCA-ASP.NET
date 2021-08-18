using AccountService.Domain.Interfaces;
using AccountService.Domain.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IServiceAccount _serviceAccount;
        public AccountController(IServiceAccount serviceAccount)
        {
            _serviceAccount = serviceAccount;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterRequest)
        {
            await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            await _serviceAccount.LoginUser(userLoginRequest);
            return Ok();
        }
    }
}
