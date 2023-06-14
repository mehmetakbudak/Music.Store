using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IWebHostEnvironment _environment;

        public ArtistController(
            IWebHostEnvironment environment,
            IArtistService artistService)
        {
            _environment = environment;
            _artistService = artistService;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] ArtistFilterModel model)
        {
            var list = _artistService.GetByFilter(model);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _artistService.GetById(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ArtistModel model)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _artistService.Post(model, rootPath);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ArtistModel model)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _artistService.Put(model, rootPath);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _artistService.Delete(id, rootPath);
            return StatusCode(result.StatusCode, result);
        }
    }
}
