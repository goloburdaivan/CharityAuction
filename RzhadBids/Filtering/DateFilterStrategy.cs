using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public class DateFilterStrategy : IFilterStrategy
    {
        public IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter)
        {
            if (filter.StartDate.HasValue) {
                query = query.Where(lot => lot.DateStart >= filter.StartDate);
            }

            if (filter.EndDate != null && filter.EndDate.HasValue)
            {
                query = query.Where(lot => lot.DateEnd >= filter.EndDate);
            }

            return query;
        }
    }
}
