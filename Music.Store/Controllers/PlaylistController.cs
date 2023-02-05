using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Music.Store.Data.Entity;
using Music.Store.Infrastructure;
using Music.Store.Models;
using TestWebApi.Data;

namespace Music.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaylistController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] PlaylistFilterModel model)
        {
            var data = _context.Playlists.AsQueryable();

            if (!string.IsNullOrEmpty(model.Name))
            {
                data = data.Where(x => x.Name.ToLower().Contains(model.Name.ToLower()));
            }

            var list = PaginationHelper<Playlist>.Paginate(data, model);
            return Ok(list);
        }

        [HttpPost("GetPlaylistTracks")]
        public IActionResult GetPlaylistTracks([FromBody] PlaylistTrackFilterModel model)
        {
            var data = _context.PlaylistTracks
                .Include(x => x.Track)
                .ThenInclude(x => x.Genre)
                .Where(x => x.PlaylistId == model.PlaylistId)
                .Select(x => x.Track)                             
                .AsQueryable();

            var list = PaginationHelper<Track>.Paginate(data, model);
            return Ok(list);
        }
    }
}
