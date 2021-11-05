using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using CrudMicroservice.Domain.ApiModel.ResponseApiModels;
using CrudMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarAccidentController: ControllerBase
    {
        private readonly ICarAccidentService _carAccidentService;

        public CarAccidentController(ICarAccidentService carAccidentService)
        {
            _carAccidentService = carAccidentService;
        }

        [Authorize(Roles = "inspector")]
        [HttpPost("CreateCA")]
        public async Task<IActionResult> CarAccidentRegistration(CarAccidentRequestApiModel data)
        {
            var inspectorNumber = User.FindFirst("Id")?.Value;
            var response = await _carAccidentService.RegistrationCarAccidentProtocol(data, inspectorNumber);
            return Ok(response);
        }

        [Authorize(Roles = "inspector")]
        [HttpGet("GetAllCAByInspector")]
        public async Task<IActionResult> FindAllCarAccidentProtocolsByInvolvedId()
        {
            var inspectorNumber = User.FindFirst("Id")?.Value;
            var response = await _carAccidentService.FindAllCarAccidentProtocolsByInvolvedId(inspectorNumber);
            return Ok(response);
        }

        [Authorize(Roles = "inspector")]
        [HttpPut("UpdateCA")]
        public async Task<IActionResult> UpdateCarAccidentProtocol(CarAccidentRequestApiModel data)
        {
            var response = await _carAccidentService.UpdateCarAccidentProtocol(data);
            return Ok(response);
        }

        [Authorize(Roles = "inspector")]
        [HttpGet("GetAllPersonsCAByInspector")]
        public async Task<IActionResult> FindAllPersonsCAProtocolsForInspector(string personDriverId)
        {
            var response = await _carAccidentService.FindAllPersonsCAProtocolsForInspector(personDriverId);
            return Ok(response);
        }

        [HttpGet("GetAllCAProtocolsByPerson")]
        public async Task<IActionResult> FindAllCAProtocolsForPerson()
        {
            var userNumber = User.FindFirst("Id")?.Value;
            string driverId = await _carAccidentService.GetUsersDriverLicense(userNumber);
            var response = await _carAccidentService.FindAllCAProtocolsForPerson(driverId);
            return Ok(response);
        }
    }
}
