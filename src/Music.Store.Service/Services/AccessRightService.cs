using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Domain.Models.AccessRight;
using Music.Store.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IAccessRightService
    {
        Task<List<AccessRightGetModel>> Get(int? parentId = null, List<AccessRightGetModel> children = null);
        Task<AccessRightModel> GetById(int id);
        Task<ServiceResult> Post(AccessRightModel model);
        Task<ServiceResult> Put(AccessRightModel model);
        Task<ServiceResult> Delete(int id);
    }

    public class AccessRightService : IAccessRightService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccessRightService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccessRightGetModel>> Get(int? parentId = null, List<AccessRightGetModel> children = null)
        {
            var list = new List<AccessRightGetModel>();

            var data = await _unitOfWork.Repository<AccessRight>()
                .GetAll(x => x.ParentId == parentId)
                .Include(x => x.AccessRightEndpoints)
                .Select(x => new AccessRightGetModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    IsActive = x.IsActive,
                    AccessRightEndpoints = x.AccessRightEndpoints.Select(a =>
                    new AccessRightEndpointModel
                    {
                        Id = a.Id,
                        Endpoint = a.Endpoint,
                        Method = a.Method,
                        RouteLevel = a.RouteLevel
                    }).ToList(),
                }).ToListAsync();

            list.AddRange(data);

            foreach (var item in list)
            {
                var items = await Get(item.Id, data);
                if (items != null && items.Count > 0)
                {
                    item.Children = items;
                }
            }
            return list;
        }

        public async Task<AccessRightModel> GetById(int id)
        {
            var entity = await _unitOfWork.Repository<AccessRight>()
                .GetAll(x => x.Id == id)
                .Include(x => x.AccessRightEndpoints)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new KeyNotFoundException("Access right not found.");
            }

            var model = new AccessRightModel
            {
                Id = entity.Id,
                ParentId = entity.ParentId,
                Name = entity.Name,
                IsActive = entity.IsActive,
                AccessRightEndpoints = entity.AccessRightEndpoints.Select(x =>
                new AccessRightEndpointModel
                {
                    Id = x.Id,
                    Endpoint = x.Endpoint,
                    Method = x.Method,
                    RouteLevel = x.RouteLevel
                }).ToList()
            };
            return model;
        }

        public async Task<ServiceResult> Post(AccessRightModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var accessRight = new AccessRight
            {
                IsActive = model.IsActive,
                Name = model.Name,
                ParentId = model.ParentId
            };

            await _unitOfWork.Repository<AccessRight>().Add(accessRight);

            if (model.AccessRightEndpoints != null && model.AccessRightEndpoints.Count > 0)
            {
                foreach (var accessRightEndpoint in model.AccessRightEndpoints)
                {
                    var isExist = await _unitOfWork.Repository<AccessRightEndpoint>()
                       .Any(x => x.Endpoint == accessRightEndpoint.Endpoint &&
                                 x.Method == accessRightEndpoint.Method &&
                                 x.RouteLevel == accessRightEndpoint.RouteLevel);

                    if (isExist)
                    {
                        throw new FoundException("Access right has already been registered.");
                    }
                    await _unitOfWork.Repository<AccessRightEndpoint>().Add(new AccessRightEndpoint
                    {
                        AccessRightId = accessRight.Id,
                        Endpoint = accessRightEndpoint.Endpoint,
                        Method = accessRightEndpoint.Method,
                        RouteLevel = accessRightEndpoint.RouteLevel
                    });
                }
            }
            await _unitOfWork.Save();

            return result;
        }

        public async Task<ServiceResult> Put(AccessRightModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var entity = await _unitOfWork.Repository<AccessRight>()
               .GetAll(x => x.Id == model.Id)
               .Include(x => x.AccessRightEndpoints)
               .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException("Access right not found.");
            }

            entity.ParentId = model.ParentId;
            entity.IsActive = model.IsActive;
            entity.Name = model.Name;

            if (entity.AccessRightEndpoints != null && entity.AccessRightEndpoints.Count > 0)
            {
                _unitOfWork.Repository<AccessRightEndpoint>().RemoveAll(entity.AccessRightEndpoints);
            }
            if (model.AccessRightEndpoints != null && model.AccessRightEndpoints.Count > 0)
            {
                foreach (var accessRightEndpoint in model.AccessRightEndpoints)
                {
                    await _unitOfWork.Repository<AccessRightEndpoint>().Add(new AccessRightEndpoint
                    {
                        AccessRightId = entity.Id,
                        Endpoint = accessRightEndpoint.Endpoint,
                        Method = accessRightEndpoint.Method,
                        RouteLevel = accessRightEndpoint.RouteLevel
                    });
                }
            }
            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var entity = await _unitOfWork.Repository<AccessRight>()
                .GetAll(x => x.Id == id)
                .Include(x => x.AccessRightEndpoints)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException("Access right not found.");
            }

            _unitOfWork.Repository<AccessRightEndpoint>().RemoveAll(entity.AccessRightEndpoints);
            _unitOfWork.Repository<AccessRight>().Remove(entity);

            await _unitOfWork.Save();

            return result;
        }
    }
}
