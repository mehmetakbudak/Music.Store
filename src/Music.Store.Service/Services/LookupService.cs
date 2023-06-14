using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface ILookupService
    {
        Task<List<LookupModel>> GetArtist();
        Task<List<LookupModel>> GetAlbum();
        Task<List<LookupModel>> GetGenre();
        Task<List<LookupModel>> GetMediaType();
    }

    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LookupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LookupModel>> GetArtist()
        {
            var list = await _unitOfWork.Repository<Artist>()
                .GetAll()
                .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return list;
        }

        public async Task<List<LookupModel>> GetAlbum()
        {
            var list = await _unitOfWork.Repository<Album>()
                .GetAll()
                .OrderBy(x => x.Title)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Title
                }).ToListAsync();

            return list;
        }

        public async Task<List<LookupModel>> GetGenre()
        {
            var list = await _unitOfWork.Repository<Genre>()
               .GetAll()
               .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return list;
        }

        public async Task<List<LookupModel>> GetMediaType()
        {
            var list = await _unitOfWork.Repository<MediaType>()
                .GetAll()
                .OrderBy(x => x.Name)
                .Select(x => new LookupModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return list;
        }
    }
}
