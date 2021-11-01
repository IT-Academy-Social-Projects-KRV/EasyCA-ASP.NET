using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="inspector")]
    [ApiController]
    public class CarAccidentController: ControllerBase
    {
        private readonly ICarAccidentService _carAccidentService;

        public CarAccidentController(ICarAccidentService carAccidentService)
        {
            _carAccidentService = carAccidentService;
        }

        [HttpPost]
        public async Task<IActionResult> CarAccidentRegistration(CarAccidentRequestApiModel data)
        {
            var inspectorNumber = User.FindFirst("Id")?.Value;
            var response = await _carAccidentService.RegistrationCarAccidentProtocol(data, inspectorNumber);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> FindAllCarAccidentProtocolsByInvolvedId()
        {
            var inspectorNumber = User.FindFirst("Id")?.Value;
            var response = await _carAccidentService.FindAllCarAccidentProtocolsByInvolvedId(inspectorNumber);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarAccidentProtocol(CarAccidentRequestApiModel data)
        {
            var response = await _carAccidentService.UpdateCarAccidentProtocol(data);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> FindAllPersonsCAProtocolsForInspector(string personDriverId)
        {
            var response = await _carAccidentService.FindAllPersonsCAProtocolsForInspector(personDriverId);
            return Ok(response);
        }
    }
}
