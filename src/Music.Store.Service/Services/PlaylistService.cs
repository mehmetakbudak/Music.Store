using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Infrastructure.Helpers;
using System.Linq;

namespace Music.Store.Service.Services
{
    public interface IPlaylistService
    {
        PaginationModel<Playlist> GetByFilter(PlaylistFilterModel model);
        PaginationModel<Track> GetPlaylistTracks(PlaylistTrackFilterModel model);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaylistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PaginationModel<Playlist> GetByFilter(PlaylistFilterModel model)
        {
            var data = _unitOfWork.Repository<Playlist>().GetAll();

            if (!string.IsNullOrEmpty(model.Name))
            {
                data = data.Where(x => x.Name.ToLower().Contains(model.Name.ToLower()));
            }

            var list = PaginationHelper<Playlist>.Paginate(data, model);

            return list;
        }

        public PaginationModel<Track> GetPlaylistTracks(PlaylistTrackFilterModel model)
        {
            var data = _unitOfWork.Repository<PlaylistTrack>()
                .GetAll()
                .Include(x => x.Track)
                .ThenInclude(x => x.Genre)
                .Where(x => x.PlaylistId == model.PlaylistId)
                .Select(x => x.Track)
                .AsQueryable();

            var list = PaginationHelper<Track>.Paginate(data, model);

            return list;
        }
    }
}
