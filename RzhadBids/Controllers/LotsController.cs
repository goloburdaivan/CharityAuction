using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Auth;
using RzhadBids.Models;
using RzhadBids.Services;
using RzhadBids.ViewModels;
using X.PagedList;

namespace RzhadBids.Controllers
{
    public class LotsController : BaseController
    {

        const int PageSize = 21;
        private readonly IHubContext<AuctionHub> hubContext;

        public LotsController(DatabaseContext databaseContext,
            IHubContext<AuctionHub> hubContext) : base(databaseContext) {    
            this.hubContext = hubContext;
        }

        [HttpGet("/lots")]
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            var lots = databaseContext.Lots.Include(l => l.LotPhotos)
                .Include(u => u.User)
                .ToPagedList(pageNumber, PageSize);
            return View(lots);
        }

        [HttpGet("/lot/{id:int}")]
        public async Task<IActionResult> Lot(int id)
        {
            var lot = databaseContext.Lots
                .Include(lot => lot.LotPhotos)
                .Include(lot => lot.Bids)
                    .ThenInclude(bid => bid.User)
                .Include(lot => lot.Category)
                .Where(lot => lot.Id == id)
                .FirstOrDefault();

            var user = await UserManager.GetUserAsync(User);
            var messages = await databaseContext.Messages.Where(message => message.ChatId == lot.ChatId)
                .Include(message => message.User)
                .ToListAsync();

            Console.WriteLine(messages.Count);

            if (lot == null)
            {
                return BadRequest(new { Error = "Lot not found" });
            }

            if (user == null)
            {
                return BadRequest(new {Error = "Not logged in"});
            }


            LotViewModel model = new()
            {
                Lot = lot,
                User = user,
                Messages = messages
            };

            return View(model);
        }

        [HttpPost("/lot/{id:int}")]
        public async Task<IActionResult> CreateBid(int id, int Sum)
        {
            ApplicationUser? currentUser = await UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return BadRequest(new { Error = "You need to login before sending smth." });
            }

            ApplicationUser lastBidUser =
                databaseContext.Bids.Include(bid => bid.User)
                .OrderBy(bid => bid.Id)
                .Last().User;

            if (lastBidUser.Id == currentUser.Id)
            {
                return BadRequest(new { Error = "You already made a bid" });
            }

            Bid bid = new()
            {
                TimeStamp = DateTime.Now,
                User = currentUser,
                LotId = id,
                Sum = Sum
            };

            await databaseContext.Bids.AddAsync(bid);
            await databaseContext.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveBidUpdate", bid);

            return Redirect($"/lot/{id}");
        }
    }
}
