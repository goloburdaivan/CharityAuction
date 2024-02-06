using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    [ApiController]
    [Route("/api/lots")]
    public class LotsContoller : DbController
    {
        public LotsContoller(DatabaseContext databaseContext) : base(databaseContext) {    
        }

        [HttpPost("create")]
        public IActionResult Index([FromBody] Lot lot)
        {
            this.databaseContext.Lots.Add(lot);
            this.databaseContext.SaveChanges();
            return Ok(lot);
        }

        [HttpGet]
        public IActionResult List() {
            return Ok(databaseContext.Lots);
        }
    }
}
