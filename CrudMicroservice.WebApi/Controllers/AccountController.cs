using CrudMicroservice.Domain.Interfaces;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CrudMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut("UpdateData")]
        public async Task<IActionResult> UpdatePersonalData(UserRequestApiModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _accountService.UpdatePersonalData(data, userId);
            return Ok(response);
        }

        [HttpGet("GetPersonalData")]
        public async Task<IActionResult> GetPersonalData()
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _accountService.GetPersonalData(userId);

            return Ok(response);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _accountService.GetUserById(userId);

            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _accountService.GetUserById(id);

            return Ok(response);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _accountService.GetUserByEmail(email);

            return Ok(response);
        }

        [HttpPost("CreatePersonalData")]
        public async Task<IActionResult> CreatePersonalData(PersonalDataRequestApiModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _accountService.CreatePersonalData(data, userId);

            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestApiModel data)
        {
            var userId = User.FindFirst("Id")?.Value;
            var response = await _accountService.ChangePassword(data.Password, data.OldPassword, userId);

            return Ok(response);
        }
    }
}
