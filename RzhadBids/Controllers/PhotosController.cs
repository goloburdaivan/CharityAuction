using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RzhadBids.DTO;
using RzhadBids.Models;
using RzhadBids.Services;
using RzhadBids.ViewModels;

namespace RzhadBids.Controllers
{

    [Route("/photos")]
    [Authorize]
    public class PhotosController : BaseController
    {
        readonly IPhotoUploadService photoStorageService;
        readonly IConfiguration configuration;

        [FromServices]
        public ThumbnailGenerator ThumbnailGenerator { get; set; }
        public PhotosController(IPhotoUploadService photoStorageService,
            IConfiguration configuration, DatabaseContext databaseContext) : base(databaseContext)
        {
            this.photoStorageService = photoStorageService;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            var categoriesModel = new LotCreateViewModel
            {
                Categories = databaseContext.Categories
            };

            return View(categoriesModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(LotDTO formData)
        {
            if (formData.Photos.IsNullOrEmpty())
            {
                ViewBag.Error = "Файл не выбран.";
                return View();
            }

            var currentUser = await UserManager.GetUserAsync(User);

            var chat = new Chat
            {
                Title = $"Чат лоту - {formData.Title}"
            };

            await databaseContext.Chats.AddAsync(chat);
            await databaseContext.SaveChangesAsync();

            var lot = new Lot
            {
                Title = formData.Title,
                CategoryId = formData.CategoryId,
                DateStart = DateTime.Now,
                DateEnd = DateTime.UtcNow.AddDays(1),
                StartingPrice = formData.StartingPrice,
                Description = formData.Description,
                User = currentUser,
                ChatId = chat.Id
            };

            await databaseContext.Lots.AddAsync(lot);

            try
            {
                foreach (var photo in formData.Photos)
                {
                    var stream = ThumbnailGenerator.GenerateThumbnail(photo);
                    await photoStorageService.UploadBlobAsync(photo.FileName, stream);
                    string? baseUrl = configuration["AzureBaseUrl"];
                    lot.LotPhotos.Add(new LotPhoto { LotId = lot.Id, Url = baseUrl + photo.FileName });
                    stream.Close();
                }

                ViewBag.Message = "Файл успешно загружен.";
                await databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Произошла ошибка при загрузке файла: " + ex.Message;
            }

            var categoriesModel = new LotCreateViewModel
            {
                Categories = databaseContext.Categories
            };

            return View(categoriesModel);
        }
    }
}
