using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain;
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
    public interface IAlbumService
    {
        PaginationModel<Album> GetByFilter(AlbumFilterModel model);
        Task<AlbumModel> GetById(int id);
        Task<ServiceResult> Post(AlbumModel model, string rootPath);
        Task<ServiceResult> Put(AlbumModel model, string rootPath);
        Task<ServiceResult> Delete(int id, string rootPath);
    }

    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginationModel<Album> GetByFilter(AlbumFilterModel model)
        {
            var data = _unitOfWork.Repository<Album>()
                .GetAll()
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

            return list;
        }

        public async Task<AlbumModel> GetById(int id)
        {
            AlbumModel model = null;

            var album = await _unitOfWork.Repository<Album>().Find(id);

            if (album == null)
            {
                return model;
            }

            model = new AlbumModel
            {
                ArtistId = album.ArtistId,
                Id = album.Id,
                Title = album.Title,
                ImageUrl = !string.IsNullOrEmpty(album.ImageUrl) ? $"{Global.ApiUrl}{album.ImageUrl}" : null
            };

            return model;
        }

        public async Task<ServiceResult> Post(AlbumModel model, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            string fileName = null;

            if (model.Image != null)
            {
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(rootPath, "images\\albums", fileName);

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

            await _unitOfWork.Repository<Album>().Add(album);
            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Put(AlbumModel model, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            string fileName = null;

            var album = await _unitOfWork.Repository<Album>().Find(model.Id);

            if (album == null)
            {
                result.Message = "Album is not found!";
                result.HttpStatusCode = HttpStatusCode.NotFound;
                return result;
            }

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(album.ImageUrl))
                {
                    var currentPath = $"{rootPath}{album.ImageUrl}";
                    File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.Image.FileName}");
                var path = Path.Combine(rootPath, "images\\albums", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                return result;
            }

            album.ArtistId = model.ArtistId;
            album.Title = model.Title;
            album.ImageUrl = fileName != null ? $"/images/albums/{fileName}" : album.ImageUrl;

            _unitOfWork.Repository<Album>().Update(album);
            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Delete(int id, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var album = await _unitOfWork.Repository<Album>().Find(id);

            if (album == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Message = "Album is not found!";
                return result;
            }

            if (!string.IsNullOrEmpty(album.ImageUrl))
            {
                var currentPath = $"{rootPath}{album.ImageUrl}";
                if (File.Exists(currentPath))
                {
                    File.Delete(currentPath);
                }
            }
            _unitOfWork.Repository<Album>().Remove(album);

            await _unitOfWork.Save();

            return result;
        }
    }
}
