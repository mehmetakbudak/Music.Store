using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IWebHostEnvironment _environment;

        public TrackController(
            IWebHostEnvironment environment,
            ITrackService trackService)
        {
            _environment = environment;
            _trackService = trackService;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] TrackFilterModel model)
        {
            var list = _trackService.GetByFilter(model);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var model = await _trackService.GetById(id);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] TrackModel model)
        {
            var rootPath = _environment.WebRootPath;
            var result = await _trackService.Put(model, rootPath);
            return StatusCode(result.StatusCode, result);
        }
    }
}
