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
    public class ArtistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ArtistController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost("GetByFilter")]
        public IActionResult GetByFilter([FromBody] ArtistFilterModel model)
        {
            var data = _context.Artists
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Text))
            {
                data = data.Where(x => x.Name.ToLower().Contains(model.Text.ToLower()) ||
                                       x.Bio.ToLower().Contains(model.Text.ToLower()));
            }
            if (model.IsPopular != null)
            {
                data = data.Where(x => x.IsPopular == model.IsPopular);
            }
            data.ForEachAsync(x =>
            {
                x.ImageUrl = !string.IsNullOrEmpty(x.ImageUrl) ? $"{Global.ApiUrl}{x.ImageUrl}" : null;
            });
            var list = PaginationHelper<Artist>.Paginate(data, model);
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ArtistModel model = null;
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
            if (artist != null)
            {
                model = new ArtistModel
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Bio = artist.Bio,
                    IsPopular = (artist.IsPopular.HasValue && artist.IsPopular.Value == true) ? true : false,
                    ImageUrl = !string.IsNullOrEmpty(artist.ImageUrl) ? $"{Global.ApiUrl}{artist.ImageUrl}" : null
                };
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ArtistModel model)
        {
            string fileName = null;

            if (model.Image != null)
            {
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(_environment.WebRootPath, "images\\artists", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            var artist = new Artist()
            {
                Name = model.Name,
                IsPopular = model.IsPopular,
                Bio = model.Bio,
                ImageUrl = fileName != null ? $"/images/artists/{fileName}" : null
            };

            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ArtistModel model)
        {
            string fileName = null;

            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (artist == null)
            {
                return NotFound("Artist is not found!");
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(artist.ImageUrl))
                {
                    var currentPath = $"{_environment.WebRootPath}{artist.ImageUrl}";
                    System.IO.File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(_environment.WebRootPath, "images\\artists", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            artist.Name = model.Name;
            artist.IsPopular = model.IsPopular;
            artist.Bio = model.Bio;
            artist.ImageUrl = fileName != null ? $"/images/artists/{fileName}" : artist.ImageUrl;

            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);

            if (artist == null)
            {
                return NotFound("Artist is not found!");
            }

            if (!string.IsNullOrEmpty(artist.ImageUrl))
            {
                var currentPath = $"{_environment.WebRootPath}{artist.ImageUrl}";
                if (System.IO.File.Exists(currentPath))
                {
                    System.IO.File.Delete(currentPath);
                }
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
