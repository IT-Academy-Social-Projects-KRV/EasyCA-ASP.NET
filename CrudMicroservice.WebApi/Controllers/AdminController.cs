﻿using CrudMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudMicroservice.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpPut("DeleteInspectorRole")]
        public async Task<IActionResult> DeleteInspector(string email)
        {
            var response = await _adminService.DeleteInspector(email);
            return Ok(response);
        }
    }
}
