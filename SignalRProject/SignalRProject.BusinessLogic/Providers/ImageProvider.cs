using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SignalRProject.BusinessLogic.Providers.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SignalRProject.BusinessLogic.Providers
{
    public class ImageProvider : IImageProvider
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageProvider(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ResizeAndSave(IFormFile file, string path, string name, int? maxWidth = null, int? maxHeight = null)
        {

            if (!maxHeight.HasValue)
            {
                maxHeight = Constants.DefaultImageSizes.MaxHeightOriginalImage;
            }

            if (!maxWidth.HasValue)
            {
                maxWidth = Constants.DefaultImageSizes.MaxWidthOriginalImage;
            }

            int width = maxWidth ?? default;
            int height = maxHeight ?? default;

            var directory = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string pathForSave = Path.Combine(directory, name);
            string savedFilePath = Path.Combine(path, name);

            Image image = Image.FromStream(file.OpenReadStream(), true, true);

            Bitmap newImage = new Bitmap(image, width, height);

            newImage.Save(pathForSave,ImageFormat.Png);


            return savedFilePath;
        }

    }
}
