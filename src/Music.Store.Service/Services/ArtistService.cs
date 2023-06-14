using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Infrastructure;
using Music.Store.Infrastructure.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IArtistService
    {
        PaginationModel<Artist> GetByFilter(ArtistFilterModel model);
        Task<ArtistModel> GetById(int id);
        Task<ServiceResult> Post(ArtistModel model, string rootPath);
        Task<ServiceResult> Put(ArtistModel model, string rootPath);
        Task<ServiceResult> Delete(int id, string rootPath);
    }

    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginationModel<Artist> GetByFilter(ArtistFilterModel model)
        {
            var data = _unitOfWork.Repository<Artist>()
                .GetAll()
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

            return list;
        }

        public async Task<ArtistModel> GetById(int id)
        {
            ArtistModel model = null;

            var artist = await _unitOfWork.Repository<Artist>().Find(id);

            if (artist == null)
            {
                return model;
            }

            model = new ArtistModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Bio = artist.Bio,
                IsPopular = artist.IsPopular,
                ImageUrl = !string.IsNullOrEmpty(artist.ImageUrl) ? $"{Global.ApiUrl}{artist.ImageUrl}" : null
            };

            return model;
        }

        public async Task<ServiceResult> Post(ArtistModel model, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            string fileName = null;

            if (model.Image != null)
            {
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(rootPath, "images\\artists", fileName);

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

            await _unitOfWork.Repository<Artist>().Add(artist);
            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Put(ArtistModel model, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            string fileName = null;

            var artist = await _unitOfWork.Repository<Artist>().Find(model.Id);

            if (artist == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Message = "Artist is not found!";
                return result;
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(artist.ImageUrl))
                {
                    var currentPath = $"{rootPath}{artist.ImageUrl}";
                    File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(rootPath, "images\\artists", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            artist.Name = model.Name;
            artist.IsPopular = model.IsPopular;
            artist.Bio = model.Bio;
            artist.ImageUrl = fileName != null ? $"/images/artists/{fileName}" : artist.ImageUrl;

            _unitOfWork.Repository<Artist>().Update(artist);
            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Delete(int id, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var artist = await _unitOfWork.Repository<Artist>().Find(id);

            if (artist == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Message = "Artist is not found!";
                return result;
            }

            if (!string.IsNullOrEmpty(artist.ImageUrl))
            {
                var currentPath = $"{rootPath}{artist.ImageUrl}";
                if (File.Exists(currentPath))
                {
                    File.Delete(currentPath);
                }
            }
            _unitOfWork.Repository<Artist>().Remove(artist);

            await _unitOfWork.Save();

            return result;
        }
    }
}
