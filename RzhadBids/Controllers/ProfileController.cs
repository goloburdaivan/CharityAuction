using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> MyProfile()
        {
            var user = await UserManager.GetUserAsync(User);
            return Redirect($"/profile/{user.Id}");
        }
        [HttpGet("/profile/{id}")]
        public IActionResult Profile(string id)
        {

            var user = databaseContext.Users
                .Include(user => user.Bids)
                .Where(user => user.Id == id).FirstOrDefault();
            var lots = databaseContext.Lots
                .Include(lot => lot.User)
                .Include(lot => lot.Bids)
                .Where(lot => lot.User.Id == id);

            var winnedLots = databaseContext.Lots
            .Include(lot => lot.Bids)
            .Include(lot => lot.User)
            .Where(lot => lot.User.Id == id && lot.DateEnd < DateTime.Now && lot.Bids.Any())
            .GroupBy(lot => lot.Id);

            ProfileViewModel model = new()
            {
                User = user,
                Lots = lots.ToList(),
                WinnedLots = GetWinnedLots(id),
            };

			if (lots == null)
			{
				return BadRequest(new { Message = "Lot not found" });
			}

            return View(model);
        }

        private List<Lot> GetWinnedLots(string userId)
        {
            var wonLots = new List<Lot>();
            var lots = databaseContext.Lots.Where(l => l.DateEnd < DateTime.Now)
                        .Include(l => l.Bids)
                        .ThenInclude(b => b.User);

            foreach (var lot in lots)
            {
                var userBid = lot.Bids
                    .Where(b => b.User.Id == userId)
                    .OrderByDescending(b => b.Sum)
                    .FirstOrDefault();

                if (userBid != null && userBid.Sum == lot.Bids.Max(b => b.Sum))
                {
                    wonLots.Add(lot);
                }
            }

            return wonLots;
        }
    }
}
