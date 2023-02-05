using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Music.Store.Data.Entity;
using Music.Store.Infrastructure;
using Music.Store.Models;
using TestWebApi.Data;

namespace Music.Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public AlbumController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] AlbumFilterModel model)
        {
            var data = _context.Albums
                .Include(x => x.Artist)
                .Include(x => x.Tracks)
                .ThenInclude(x => x.Genre)
                .Include(x => x.Tracks)
                .ThenInclude(x => x.MediaType)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Title))
            {
                data = data.Where(x => x.Title.ToLower().Contains(model.Title.ToLower()));
            }
            if (model.ArtistIds != null && model.ArtistIds.Count > 0)
            {
                data = data.Where(x => model.ArtistIds.Contains(x.ArtistId));
            }
            data.ForEachAsync(x =>
            {
                x.ImageUrl = !string.IsNullOrEmpty(x.ImageUrl) ? $"{Global.ApiUrl}{x.ImageUrl}" : null;
            });
            var list = PaginationHelper<Album>.Paginate(data, model);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            AlbumModel model = null;
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
            if (album != null)
            {
                model = new AlbumModel
                {
                    ArtistId = album.ArtistId,
                    Id = album.Id,
                    Title = album.Title,
                    ImageUrl = !string.IsNullOrEmpty(album.ImageUrl) ? $"{Global.ApiUrl}{album.ImageUrl}" : null
                };
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AlbumModel model)
        {
            string fileName = null;

            if (model.Image != null)
            {
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(_environment.WebRootPath, "images\\albums", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            var album = new Album()
            {
                ArtistId = model.ArtistId,
                Title = model.Title,
                ImageUrl = fileName != null ? $"/images/albums/{fileName}" : null
            };

            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] AlbumModel model)
        {
            string fileName = null;

            var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (album == null)
            {
                return NotFound("Album is not found!");
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(album.ImageUrl))
                {
                    var currentPath = $"{_environment.WebRootPath}{album.ImageUrl}";
                    System.IO.File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(_environment.WebRootPath, "images\\albums", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            album.ArtistId = model.ArtistId;
            album.Title = model.Title;
            album.ImageUrl = fileName != null ? $"/images/albums/{fileName}" : album.ImageUrl;

            _context.Albums.Update(album);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);

            if (album == null)
            {
                return NotFound("Album is not found!");
            }

            if (!string.IsNullOrEmpty(album.ImageUrl))
            {
                var currentPath = $"{_environment.WebRootPath}{album.ImageUrl}";
                if (System.IO.File.Exists(currentPath))
                {
                    System.IO.File.Delete(currentPath);
                }
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
