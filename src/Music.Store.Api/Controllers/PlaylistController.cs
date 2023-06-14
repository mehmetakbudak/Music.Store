using Microsoft.AspNetCore.Mvc;
using Music.Store.Domain.Models;
using Music.Store.Service.Services;

namespace Music.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(
            IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] PlaylistFilterModel model)
        {
            var list = _playlistService.GetByFilter(model);
            return Ok(list);
        }

        [HttpPost("GetPlaylistTracks")]
        public IActionResult GetPlaylistTracks([FromBody] PlaylistTrackFilterModel model)
        {
            var list = _playlistService.GetPlaylistTracks(model);
            return Ok(list);
        }
    }
}
