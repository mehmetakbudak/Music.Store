using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Music.Store.Data.Entity;
using Music.Store.Infrastructure;
using Music.Store.Models;
using TestWebApi.Data;
using Microsoft.AspNetCore.Hosting;

namespace Music.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TrackController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] TrackFilterModel model)
        {
            var data = _context.Tracks
               .Include(x => x.Album)
               .ThenInclude(x => x.Artist)
               .Include(x => x.Genre)
               .Include(x => x.MediaType)
               .OrderByDescending(x => x.Id)
               .AsQueryable();

            if (!string.IsNullOrEmpty(model.Name))
            {
                data = data.Where(x => x.Name.ToLower().Contains(model.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Composer))
            {
                data = data.Where(x => x.Composer.ToLower().Contains(model.Composer.ToLower()));
            }
            if (model.AlbumIds != null && model.AlbumIds.Count > 0)
            {
                data = data.Where(x => model.AlbumIds.Contains(x.AlbumId));
            }
            if (model.ArtistIds != null && model.ArtistIds.Count > 0)
            {
                data = data.Where(x => model.ArtistIds.Contains(x.Album.ArtistId));
            }
            if (model.GenreIds != null && model.GenreIds.Count > 0)
            {
                data = data.Where(x => model.GenreIds.Contains(x.GenreId));
            }
            if (model.MediaTypeIds != null && model.MediaTypeIds.Count > 0)
            {
                data = data.Where(x => model.MediaTypeIds.Contains(x.MediaTypeId));
            }
            var list = PaginationHelper<Track>.Paginate(data, model);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var track = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == id);
            if (track == null)
            {
                return NotFound("Track is not found.");
            }
            var model = new TrackModel
            {
                AlbumId = track.AlbumId,
                Composer = track.Composer,
                FileUrl = track.FileUrl,
                GenreId = track.GenreId,
                Id = id,
                Lyrics = track.Lyrics,
                MediaTypeId = track.MediaTypeId,
                Name = track.Name,
                UnitPrice = track.UnitPrice,
                WebUrl = track.WebUrl
            };
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] TrackModel model)
        {
            string fileName = null;

            var track = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (track == null)
            {
                return NotFound("Track is not found!");
            }

            if (model.File != null)
            {
                if (!string.IsNullOrEmpty(track.FileUrl))
                {
                    var currentPath = $"{_environment.WebRootPath}{track.FileUrl}";
                    System.IO.File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.File.FileName}");
                var path = Path.Combine(_environment.WebRootPath, "audios\\tracks", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
            }

            track.AlbumId = model.AlbumId;
            track.Name = model.Name;
            track.FileUrl = fileName != null ? $"/audios/tracks/{fileName}" : track.FileUrl;
            track.Lyrics = model.Lyrics;
            track.Composer = model.Composer;
            track.UnitPrice = model.UnitPrice;
            track.GenreId = model.GenreId;
            track.MediaTypeId = model.MediaTypeId;
            track.WebUrl = model.WebUrl;
            track.Bytes = model.File != null ? model.File.Length : 0;
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
