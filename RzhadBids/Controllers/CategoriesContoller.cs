using Microsoft.AspNetCore.Mvc;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesContoller : DbController
    {

        public CategoriesContoller(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [HttpPost("create")]
        public IActionResult Index([FromBody] Category category)
        {
            this.databaseContext.Categories.Add(category);
            this.databaseContext.SaveChanges();

            return Ok(category);
        }
    }
}
