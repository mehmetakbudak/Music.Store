using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using Music.Store.Domain.Models;
using Music.Store.Domain.Models.UserAccessRight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IUserAccessRightService
    {
        Task<List<int>> GetByUserId(int userId);
        Task<bool> CheckAccessRight(int userId, string[] pathes, string method);
        Task<ServiceResult> CreateOrUpdate(UserAccessRightModel model);
    }

    public class UserAccessRightService : IUserAccessRightService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAccessRightService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<int>> GetByUserId(int userId)
        {
            var list = await _unitOfWork.Repository<UserAccessRight>()
                .GetAll(x => x.UserId == userId)
                .Select(x => x.AccessRightId).ToListAsync();
            return list;
        }

        public async Task<bool> CheckAccessRight(int userId, string[] pathes, string method)
        {
            string endpoint = string.Empty;
            int routeLevel = 2;
            if (pathes.Length > 3)
            {
                var level3 = pathes[3].ToString();
                if (!Int32.TryParse(level3.ToString(), out int level3Value))
                {
                    endpoint = level3;
                    routeLevel = 3;
                }
            }
            else
            {
                endpoint = pathes[2];
            }
            var check = await _unitOfWork.Repository<UserAccessRight>()
                .Any(x => x.UserId == userId && x.AccessRight.AccessRightEndpoints
                .Any(a => a.Endpoint.ToLower() == endpoint.ToLower() && a.Method.ToLower() == method.ToLower() && a.RouteLevel == routeLevel));
            return check;
        }

        public async Task<ServiceResult> CreateOrUpdate(UserAccessRightModel model)
        {
            var result = new ServiceResult { HttpStatusCode = HttpStatusCode.OK };

            var list = await _unitOfWork.Repository<UserAccessRight>()
                .GetAll(x => x.UserId == model.UserId).ToListAsync();

            var addingList = model.AccessRightIds
                           .Where(x => !list.Select(x => x.AccessRightId).Contains(x)).ToList();

            if (addingList != null && addingList != null)
            {
                foreach (var a in addingList)
                {
                    await _unitOfWork.Repository<UserAccessRight>().Add(new UserAccessRight
                    {
                        AccessRightId = a,
                        UserId = model.UserId
                    });
                }
            }

            var deletingList = list.Where(x => !model.AccessRightIds.Contains(x.AccessRightId)).ToList();

            if (deletingList != null && deletingList.Any())
            {
                _unitOfWork.Repository<UserAccessRight>().RemoveAll(deletingList);
            }

            return result;
        }
    }
}
