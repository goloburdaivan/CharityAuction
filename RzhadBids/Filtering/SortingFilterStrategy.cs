using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public class SortingFilterStrategy : IFilterStrategy
    {
        public IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter)
        {
            if (filter.SortDesc != null)
            {
                query = query.OrderByDescending(l => l.StartingPrice);
            }

            if (filter.SortAsc != null)
            {
                query = query.OrderBy(l => l.StartingPrice);
            }

            return query;
        }
    }
}
