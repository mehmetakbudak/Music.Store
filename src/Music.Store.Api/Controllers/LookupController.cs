using Microsoft.AspNetCore.Mvc;
using Music.Store.Service.Services;
using System.Threading.Tasks;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("Artist")]
        public async Task<IActionResult> GetArtist()
        {
            var list = await _lookupService.GetArtist();
            return Ok(list);
        }

        [HttpGet("Album")]
        public async Task<IActionResult> GetAlbum()
        {
            var list = await _lookupService.GetAlbum();
            return Ok(list);
        }

        [HttpGet("Genre")]
        public async Task<IActionResult> GetGenre()
        {
            var list = await _lookupService.GetGenre();
            return Ok(list);
        }

        [HttpGet("MediaType")]
        public async Task<IActionResult> GetMediaType()
        {
            var list = await _lookupService.GetMediaType();
            return Ok(list);
        }
    }
}
