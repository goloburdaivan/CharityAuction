using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RzhadBids.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet("/profile")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
