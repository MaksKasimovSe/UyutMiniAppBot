using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;
using System.IO;

namespace UyutMiniApp.Controllers
{
    [ApiController,Route("[controller]")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]string search = "") =>
            Ok(await menuItemService.GetAllAsync(search));

        [HttpPost]
        public async Task AddAsync([FromForm] CreateMenuItemDto dto)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);
            
            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);
            
            FileStream fileStream = System.IO.File.OpenWrite(filePath);
            
            await dto.Image.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();
            dto.ImageUrl = $"/images/{fileName}";
            await menuItemService.CreateAsync(dto);
        }
    }
}
