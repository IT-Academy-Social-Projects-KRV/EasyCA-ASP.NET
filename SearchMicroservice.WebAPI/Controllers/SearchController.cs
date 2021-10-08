using Microsoft.AspNetCore.Mvc;
using SearchMicroservice.Domain.Interfaces;
using System.Threading.Tasks;

namespace SearchMicroservice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet("SearchTransport")]
        public async Task<IActionResult> Search(string search)
        {
            var response = await _searchService.Search(search);
            return Ok(response);
        }
    }
}
