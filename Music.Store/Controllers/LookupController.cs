using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Music.Store.Models;
using TestWebApi.Data;

namespace Music.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LookupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Artist")]
        public async Task<IActionResult> GetArtist()
        {
            var list = await _context.Artists
                .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return Ok(list);
        }

        [HttpGet("Album")]
        public async Task<IActionResult> GetAlbum()
        {
            var list = await _context.Albums
                .OrderBy(x => x.Title)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Title
                }).ToListAsync();
            return Ok(list);
        }

        [HttpGet("Genre")]
        public async Task<IActionResult> GetGenre()
        {
            var list = await _context.Genres
                .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            return Ok(list);
        }

        [HttpGet("MediaType")]
        public async Task<IActionResult> GetMediaType()
        {
            var list = await _context.MediaTypes
                .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            return Ok(list);
        }
    }
}
