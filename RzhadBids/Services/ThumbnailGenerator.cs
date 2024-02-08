using System.Drawing;
using System.Drawing.Imaging;

namespace RzhadBids.Services
{
    public class ThumbnailGenerator
    {

        const int ThumbnailWidth = 330;
        const int ThumbnailHeight = 270;

        public Stream GenerateThumbnail(IFormFile file)
        {
            if (file == null) 
                throw new ArgumentNullException("No file selected");
            if (file.Length == 0)
                throw new ArgumentException("Bad file format");

            var bigImage = Image.FromStream(file.OpenReadStream(), true, true);
            var thumbnail = bigImage.GetThumbnailImage(ThumbnailWidth, ThumbnailHeight, () => false, IntPtr.Zero);
            var outputStream = new MemoryStream();
            thumbnail.Save(outputStream, ImageFormat.Png);
            outputStream.Position = 0;
            return outputStream;
        }
    }
}
