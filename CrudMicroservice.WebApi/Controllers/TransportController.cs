using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudMicroservice.WebApi.Controllers
{
    [Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpGet("GetAllTransports")]
        public async Task<IActionResult> GetAllTransports()
        {
            var userId = User.FindFirst("id")?.Value;
            var response = await _transportService.GetAllTransports(userId);
            return Ok(response);
        }

        [HttpGet("GetTransport")]
        public async Task<IActionResult> GetTransportById(string transportId)
        {
            var response = await _transportService.GetTransportById(transportId);
            return Ok(response);
        }

        [HttpPost("AddTransport")]
        public async Task<IActionResult> AddTransport(AddTransportRequestModel model)
        {
            var userId = User.FindFirst("id")?.Value;
            var response = await _transportService.AddTransport(model, userId);
            return Ok(response);
        }

        [HttpPut("UpdateTransport")]
        public async Task<IActionResult> UpdateTransort(UpdateTransportRequestModel model)
        {
            var userId = User.FindFirst("id")?.Value;
            var response = await _transportService.UpdateTransport(model, userId);
            return Ok(response);
        }

        [HttpDelete("DeleteTransport")]
        public async Task<IActionResult> DeleteTransport(string transportId)
        {
            var userId = User.FindFirst("id")?.Value;
            var response = await _transportService.DeleteTransport(transportId, userId);
            return Ok(response);
        }

        [HttpGet("GetTransportByCarPlate")]
        public async Task<IActionResult> GetTransportByCarPlate(string carPlate)
        {
            var response = await _transportService.GetTransportByCarPlate(carPlate);
            return Ok(response);
        }
    }
}
