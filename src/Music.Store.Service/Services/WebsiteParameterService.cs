using Microsoft.EntityFrameworkCore;
using Music.Store.Data.Repositories;
using Music.Store.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Store.Service.Services
{
    public interface IWebsiteParameterService
    {
        Task<T> GetParametersByType<T>(string code) where T : class, new();
    }

    public class WebsiteParameterService : IWebsiteParameterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebsiteParameterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetParametersByType<T>(string code) where T : class, new()
        {
            T model = new T();

            var parentParameter = await _unitOfWork.Repository<WebsiteParameter>()
                .Get(p => p.Code == code && p.ParentId == null);

            if (parentParameter != null)
            {
                var parameters = await _unitOfWork.Repository<WebsiteParameter>()
                    .GetAll(x => x.ParentId == parentParameter.Id).ToListAsync();

                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    var parameter = parameters.FirstOrDefault(x => x.Code == property.Name);

                    if (parameter != null)
                    {
                        property.SetValue(model, parameter.Value);
                    }
                }
            }
            return model;
        }
    }
}
