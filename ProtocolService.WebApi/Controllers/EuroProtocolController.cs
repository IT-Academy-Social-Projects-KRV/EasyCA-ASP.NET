using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using ProtocolService.Domain.Interfaces;
using System.Threading.Tasks;
using ProtocolService.Domain.ApiModel.RequestApiModels;

namespace ProtocolService.WwebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class EuroProtocolController : ControllerBase
    {
        private readonly IProtocolService _serviceProtocol;
        private readonly IConfiguration _configuration;

        public EuroProtocolController(IProtocolService serviceProtocol, IConfiguration configuration)
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
    }
}
