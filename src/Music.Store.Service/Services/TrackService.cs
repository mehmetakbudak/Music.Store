using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Infrastructure.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface ITrackService
    {
        PaginationModel<Track> GetByFilter(TrackFilterModel model);
        Task<TrackModel> GetById(int id);
        Task<ServiceResult> Put(TrackModel model, string rootPath);
    }

    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginationModel<Track> GetByFilter(TrackFilterModel model)
        {
            var data = _unitOfWork.Repository<Track>()
                .GetAll()
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

            return list;
        }

        public async Task<TrackModel> GetById(int id)
        {
            TrackModel model = null;

            var track = await _unitOfWork.Repository<Track>().Get(x => x.Id == id);

            if (track == null)
            {
                return model;
            }

            model = new TrackModel
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
            return model;
        }

        public async Task<ServiceResult> Put(TrackModel model, string rootPath)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            string fileName = null;

            var track = await _unitOfWork.Repository<Track>().Get(x => x.Id == model.Id);

            if (track == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.Message = "Track is not found!";
                return result;
            }

            if (model.File != null)
            {
                if (!string.IsNullOrEmpty(track.FileUrl))
                {
                    var currentPath = $"{rootPath}{track.FileUrl}";
                    File.Delete(currentPath);
                }
                fileName = ($"{Guid.NewGuid()}_{model.File.FileName}");
                var path = Path.Combine(rootPath, "audios\\tracks", fileName);

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

            _unitOfWork.Repository<Track>().Update(track);
            await _unitOfWork.Save();

            return result;
        }
    }
}
