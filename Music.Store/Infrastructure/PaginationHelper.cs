using System.Linq;
using Music.Store.Models;

namespace Music.Store.Infrastructure
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
