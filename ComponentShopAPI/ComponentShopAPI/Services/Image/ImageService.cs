using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        private string CreatePath(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            return Path.Combine(imagePath, imageName);
        }

        public async Task<string> Upload(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = CreatePath(imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public FileContentResult Download(string imageName)
        {
            if (imageName == "")
            {
                return null;
            }

            var imagePath = CreatePath(imageName);
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string contentType = "image/" + Path.GetExtension(imageName).Replace(".", "");

            return new FileContentResult(imageBytes, contentType);
        }

        public void DeleteIfExists(string imageName)
        {
            var imagePath = CreatePath(imageName);
            if (!File.Exists(imagePath))
            {
                return;
            }
            File.Delete(imagePath);
        }
    }
}
