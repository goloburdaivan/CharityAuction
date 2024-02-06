using Microsoft.AspNetCore.Mvc;

namespace RzhadBids.Controllers
{

    [Route("/photos")]
    public class PhotosController : Controller
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return Ok();
        }

        /*[HttpPost("/photos")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Уникальное имя файла
                    string fileName = Path.GetFileName(Guid.NewGuid().ToString() + "_" + file.FileName);

                    // Путь к папке, в которую сохраняются изображения
                    string uploadPath = Server.MapPath("~/Uploads");

                    // Если папка не существует, создаем ее
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Полный путь к файлу на сервере
                    string filePath = Path.Combine(uploadPath, fileName);

                    // Сохраняем файл на сервере
                    file.SaveAs(filePath);

                    ViewBag.Message = "Файл успешно загружен.";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Произошла ошибка при загрузке файла: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Error = "Файл не выбран.";
            }

            return View();
        }*/
    }
}
