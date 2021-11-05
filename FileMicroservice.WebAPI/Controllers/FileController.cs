using System.Threading.Tasks;
using FileMicroservice.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileMicroservice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("DownloadFiles")]
        public async Task<IActionResult> DownloadFiles([FromQuery] string[] ids)
        {
            var response = await _fileService.DownloadFiles(ids);
            return Ok(response);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] IFormCollection formData )
        {
            var response = await _fileService.UploadFiles(formData.Files);
            return Ok(response);
        }

        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _fileService.DeleteFile(id);
            return Ok(response);
        }
    }
}
