﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RzhadBids.DTO;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{

    [Route("/photos")]
    public class PhotosController : DbController
    {
        IPhotoUploadService photoStorageService;
        IConfiguration configuration;

        public PhotosController(IPhotoUploadService photoStorageService,
            IConfiguration configuration, DatabaseContext databaseContext) : base(databaseContext)
        {
            this.photoStorageService = photoStorageService;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(LotDTO formData)
        {


            if (formData.Photos.IsNullOrEmpty())
            {
                ViewBag.Error = "Файл не выбран.";
                return View();
            }

            var lot = new Lot
            {
                Title = formData.Title,
                CategoryId = formData.CategoryId,
                DateStart = DateTime.Now,
                DateEnd = DateTime.UtcNow.AddDays(1),
                StartingPrice = formData.StartingPrice,
                Description = formData.Description
            };

            databaseContext.Lots.Add(lot);
            try
            {
                foreach (var photo in formData.Photos)
                {
                    using (var stream = photo.OpenReadStream())
                    {
                        await photoStorageService.UploadBlobAsync(photo.FileName, stream);
                        string? baseUrl = configuration["AzureBaseUrl"];
                        lot.LotPhotos.Add(new LotPhoto { Lot = lot, Url = baseUrl + photo.FileName });
                    }
                }

                ViewBag.Message = "Файл успешно загружен.";
                await databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Произошла ошибка при загрузке файла: " + ex.Message;
            }

            return View();
        }
    }
}
