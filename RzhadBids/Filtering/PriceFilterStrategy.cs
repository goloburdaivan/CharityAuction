using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public class PriceFilterStrategy : IFilterStrategy
    {
        public IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter)
        {
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(l => l.StartingPrice >= filter.MinPrice);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(l => l.StartingPrice <= filter.MaxPrice);
            }

            return query;
        }
    }
}
