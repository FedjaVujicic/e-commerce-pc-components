using ComponentShopAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Services.Image
{
    public interface IImageService
    {
        public Task<string> Upload(IFormFile imageFile, ProductType imageType);
        public FileContentResult Download(string imageName, ProductType imageType);
        public void Delete(string imageName, ProductType imageType);
    }
}
