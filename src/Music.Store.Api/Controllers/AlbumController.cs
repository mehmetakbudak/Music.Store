using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IWebHostEnvironment _environment;

        public AlbumController(
            IWebHostEnvironment environment,
            IAlbumService albumService)
        {
            _environment = environment;
            _albumService = albumService;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] AlbumFilterModel model)
        {
            var list = _albumService.GetByFilter(model);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await _albumService.GetById(id);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AlbumModel model)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _albumService.Post(model, rootPath);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] AlbumModel model)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _albumService.Put(model, rootPath);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _albumService.Delete(id, rootPath);
            return StatusCode(result.StatusCode, result);
        }
    }
}
