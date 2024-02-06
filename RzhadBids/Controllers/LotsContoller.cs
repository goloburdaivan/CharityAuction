using Microsoft.AspNetCore.Mvc;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    [ApiController]
    [Route("/api/lots")]
    public class LotsContoller : Controller
    {
        DatabaseContext databaseContext;
        public LotsContoller(DatabaseContext databaseContext) { 
            this.databaseContext = databaseContext;
        }

        [HttpPost("create")]
        public IActionResult Index([FromBody] Lot lot)
        {
            this.databaseContext.Lots.Add(lot);
            this.databaseContext.SaveChanges();
            return Ok(lot);
        }
    }
}
