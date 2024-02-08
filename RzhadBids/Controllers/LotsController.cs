using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Services;
using X.PagedList;

namespace RzhadBids.Controllers
{
    public class LotsController : DbController
    {

        const int PageSize = 21;
        public LotsController(DatabaseContext databaseContext) : base(databaseContext) {    
        }

        [HttpGet("/lots")]
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            var lots = databaseContext.Lots.Include(l => l.LotPhotos).ToPagedList(pageNumber, PageSize);
            return View(lots);
        }
    }
}
