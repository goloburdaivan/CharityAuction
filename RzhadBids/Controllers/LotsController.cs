using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RzhadBids.Auth;
using RzhadBids.DTO;
using RzhadBids.Filtering;
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

        [FromServices]
        public ThumbnailGenerator ThumbnailGenerator { get; set; }

        [FromServices]
        public IPhotoUploadService PhotoUploadService { get; set; }

        private readonly IConfiguration configuration;

        public LotsController(DatabaseContext databaseContext,
            IHubContext<AuctionHub> hubContext,
            IConfiguration configuration) : base(databaseContext)
        {
            this.hubContext = hubContext;
            this.configuration = configuration;
        }

        [HttpGet("/lots")]
        public IActionResult Index(int? page, bool? active, FiltersDTO filters)
        {
            int pageNumber = page ?? 1;
            IQueryable<Lot> lots = databaseContext.Lots
                .Include(l => l.LotPhotos)
                .Include(l => l.Bids)
                .Include(u => u.User);

            if (active.HasValue && active.Value)
            {
                lots = lots.Where(lot => lot.DateEnd > DateTime.UtcNow);
            }

            var strategies = new List<IFilterStrategy>
            {
                FilterStrategyFactory.GetFilterStrategy(FilterType.Category),
                FilterStrategyFactory.GetFilterStrategy(FilterType.Date),
                FilterStrategyFactory.GetFilterStrategy(FilterType.Price),
                FilterStrategyFactory.GetFilterStrategy(FilterType.Activity)
            };

            foreach (var strategy in strategies)
            {
                lots = strategy.ApplyFilter(lots, filters);
            }

            ViewBag.Categories = databaseContext.Categories.ToList();
            ViewBag.Filters = filters;

            return View(lots.ToPagedList());
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

            if (lot == null)
            {
                return BadRequest(new { Error = "Lot not found" });
            }

            if (user == null)
            {
                return BadRequest(new { Error = "Not logged in" });
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

            ApplicationUser? lastBidUser =
                databaseContext.Bids.Any() ?
                databaseContext.Bids.Include(bid => bid.User)
                .OrderBy(bid => bid.Id)
                .Last().User
                : null;

            if (lastBidUser != null && lastBidUser.Id == currentUser.Id)
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

        [HttpGet("/lot/{id:int}/edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser? user = await UserManager.GetUserAsync(User);
            var lot = await databaseContext.Lots
                .Include(lot => lot.User)
                .Include(lot => lot.Category)
                .Include(lot => lot.LotPhotos)
                .FirstOrDefaultAsync(lot => lot.Id == id);

            var categories = await databaseContext.Categories.ToListAsync();

            if (lot == null)
            {
                return BadRequest(new { Message = "Lot not found" });
            }

            if (user.Id != lot.User.Id)
            {
                return Forbid();
            }

            var model = new LotEditViewModel
            {
                Categories = categories,
                Lot = lot
            };

            return View(model);
        }

        [HttpPost("/lot/{id:int}/edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, LotDTO editedLot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Invalid lot model" });
            }

            var lot = await databaseContext.Lots
                .Include(lot => lot.User)
                .FirstOrDefaultAsync(lot => lot.Id == id);

            var user = await UserManager.GetUserAsync(User);

            if (lot == null)
            {
                return BadRequest(new { Error = "Lot not found" });
            }

            if (user.Id != lot.User.Id)
            {
                return Forbid();
            }

            lot.StartingPrice = editedLot.StartingPrice;
            lot.Title = editedLot.Title;
            lot.CategoryId = editedLot.CategoryId;
            lot.Description = editedLot.Description;

            try
            {
                foreach (var photo in editedLot.Photos)
                {
                    var stream = ThumbnailGenerator.GenerateThumbnail(photo);
                    await PhotoUploadService.UploadBlobAsync(photo.FileName, stream);
                    string? baseUrl = configuration["AzureBaseUrl"];
                    lot.LotPhotos.Add(new LotPhoto { LotId = lot.Id, Url = baseUrl + photo.FileName });
                    stream.Close();
                }

                ViewBag.Message = "Файл успішно завантажено.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Виникла помилка при завантаженні файлу: " + ex.Message;
            }

            databaseContext.Update(lot);
            await databaseContext.SaveChangesAsync();

            return Redirect($"/lot/{id}/edit");
        }
    }
}
