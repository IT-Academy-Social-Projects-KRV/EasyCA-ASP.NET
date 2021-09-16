using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using ProtocolService.Domain.Interfaces;
using System.Threading.Tasks;
using ProtocolService.Domain.ApiModel.RequestApiModels;

namespace ProtocolService.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class EuroProtocolController : ControllerBase
    {
        private readonly IServiceProtocol _serviceProtocol;
        private readonly IConfiguration _configuration;

        public EuroProtocolController(IServiceProtocol serviceProtocol, IConfiguration configuration)
        {
            _serviceProtocol = serviceProtocol;
            _configuration = configuration;
        }

        [HttpPost("CreateEuroProtocol")]
        public async Task<IActionResult> RegisterEuroProtocol (EuroProtocolRequestModel euroProtocolRequest)
        {
            var response = await _serviceProtocol.RegistrationEuroProtocol(euroProtocolRequest);
            return Ok(response);
        }

        [HttpPost("RegisterSideBEuroProtocol")]
        public async Task<IActionResult> RegisterSideBEuroProtocol(SideRequestModel SideRequest)
        {
            var response = await _serviceProtocol.RegisterSideBEuroProtocol(SideRequest);
            return Ok(response);
        }

        [HttpGet("SearchEuroProtocolByEmail")]
        public async Task<IActionResult> FindProtocolWithEmail(string email)
        {
            var response = await _serviceProtocol.FindProtocolWithEmail(email);
            return Ok(response);
        }
    }
}
