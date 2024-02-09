using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public class CategoryFilterStrategy : IFilterStrategy
    {
        public IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter)
        {
            if (filter.FilterCategories != null && filter.FilterCategories.Any())
            {
                query = query.Where(l => filter.FilterCategories.Contains(l.CategoryId.ToString()));
            }
            return query;
        }
    }
}
