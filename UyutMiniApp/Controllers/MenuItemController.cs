using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string search = "") =>
            Ok(await menuItemService.GetAllAsync(search));

        [HttpPost, Authorize(Roles = "Admin")]
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
        [HttpPut("id")]
        public async Task UpdateAsync(Guid id, [FromForm] UpdateMenuItemDto dto)
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

            await menuItemService.UpdateAsync(id, dto);
        }
    }
}
