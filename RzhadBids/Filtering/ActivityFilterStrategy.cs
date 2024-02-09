using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public class ActivityFilterStrategy : IFilterStrategy
    {
        public IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter)
        {
            if (filter.Actual != null)
            {
                query = query.Where(lot => lot.DateEnd > DateTime.UtcNow);
            }

            if (filter.Unactual != null)
            {
                query = query.Where(lot => lot.DateEnd < DateTime.UtcNow);
            }

            if (filter.MaxBids != null)
            {
                query = query.OrderByDescending(lot => lot.Bids.Count);
            }

            return query;
        }
    }
}
