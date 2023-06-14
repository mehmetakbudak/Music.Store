using Music.Store.Domain.Models;
using System.Linq;

namespace Music.Store.Infrastructure.Helpers
{
    public class PaginationHelper<T>
    {
        public static PaginationModel<T> Paginate(IQueryable<T> data, FilterModel model)
        {
            var list = new PaginationModel<T>
            {
                Count = data.Count(),
                Page = model.Page,
                PageSize = model.PageSize,
                List = data.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToList()
            };
            return list;
        }
    }
}
