using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Services.Image
{
    public interface IImageService
    {
        public Task<string> Upload(IFormFile imageFile);
        public FileContentResult Download(string imageName);
        public void DeleteIfExists(string imageName);
    }
}
