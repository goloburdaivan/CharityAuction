using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Models;
using RzhadBids.Services;
using RzhadBids.ViewModels;

namespace RzhadBids.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [HttpGet("/profile")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/profile/{id}")]
        public IActionResult Profile(string id)
        {

            var user = databaseContext.Users
                .Where(user => user.Id == id).FirstOrDefault();
            var lots = databaseContext.Lots
                .Include(lot => lot.User)
                .Include(lot => lot.Bids)
                .Where(lot => lot.User.Id == id);

            ProfileViewModel model = new()
            {
                User = user,
                Lots = lots.ToList()
                
            };

			if (lots == null)
			{
				return BadRequest(new { Message = "Lot not found" });
			}

			foreach (var u in databaseContext.Users)
            {
                //await Console.Out.WriteLineAsync(u.Id);
            }

            return View(model);
        }
    }
}
