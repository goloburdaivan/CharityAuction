using Microsoft.AspNetCore.Mvc;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesContoller : BaseController
    {
        public CategoriesContoller(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [HttpPost("create")]
        public IActionResult Index([FromBody] Category category)
        {
            databaseContext.Categories.Add(category);
            databaseContext.SaveChanges();

            return Ok(category);
        }
    }
}
