using RzhadBids.DTO;
using RzhadBids.Models;

namespace RzhadBids.Filtering
{
    public interface IFilterStrategy
    {
        IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, FiltersDTO filter);
    }
}
