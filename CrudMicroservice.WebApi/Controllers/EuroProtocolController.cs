﻿using Microsoft.AspNetCore.Mvc;
using CrudMicroservice.Domain.Interfaces;
using System.Threading.Tasks;
using CrudMicroservice.Domain.ApiModel.RequestApiModels;
using Microsoft.AspNetCore.Authorization;

namespace CrudMicroservice.WebApi
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EuroProtocolController : ControllerBase
    {
        private readonly IEuroProtocolService _euroProtocolService;

        public EuroProtocolController(IEuroProtocolService euroProtocolService)
        {
            _euroProtocolService = euroProtocolService;
        }

        [HttpPost("CreateEuroProtocol")]
        public async Task<IActionResult> RegisterEuroProtocol (EuroProtocolRequestModel euroProtocolRequest)
        {
            var response = await _euroProtocolService.RegistrationEuroProtocol(euroProtocolRequest);
            return Ok(response);
        }

        [HttpPost("RegisterSideBEuroProtocol")]
        public async Task<IActionResult> RegisterSideBEuroProtocol(SideRequestModel SideRequest)
        {
            var response = await _euroProtocolService.RegisterSideBEuroProtocol(SideRequest);
            return Ok(response);
        }

        [HttpGet("FindAllEuroProtocolsByEmail")]
        public async Task<IActionResult> FindAllEuroProtocolsByEmail(string email)
        {
            var response = await _euroProtocolService.FindAllEuroProtocolsByEmail(email);
            return Ok(response);
        }

        [HttpPut("UpdateEuroProtocol")]
        public async Task<IActionResult> UpdateEuroProtocol(EuroProtocolRequestModel data)
        {
            var response = await _euroProtocolService.UpdateEuroProtocol(data);
            return Ok(response);
        }

        [HttpGet("GetAllCircumstances")]
        public async Task<IActionResult> GetAllCircumstances()
        {
            var response = await _euroProtocolService.GetAllCircumstances();
            return Ok(response);
        }
    }
}
