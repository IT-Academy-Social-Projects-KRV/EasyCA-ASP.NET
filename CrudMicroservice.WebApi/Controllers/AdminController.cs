using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAllEuroProtocols")]
        public async Task<IActionResult> GetAllEuroProtocols()
        {
            var response = await _adminService.GetAllEuroProtocols();
            return Ok(response);
        }

        [HttpGet("GetAllInspectors")]
        public async Task<IActionResult> GetAllInspectors()
        {
            var response = await _adminService.GetAllInspectors();
            return Ok(response);
        }

        [HttpPost("AddInspectors")]
        public async Task<IActionResult> AddInspectors(InspectorRequestApiModel data)
        {
            var response = await _adminService.AddInspector(data);
            return Ok(response);
        }
    }
}
