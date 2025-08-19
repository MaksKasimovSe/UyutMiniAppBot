using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync([FromQuery] string search = "") =>
            Ok(await menuItemService.GetAllAsync(search));

        /// <summary>
        /// Create new menu item (Admins only)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update menu item (Admins only)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Delete menu item (Admins only)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync(Guid id) =>
            await menuItemService.DeleteAsync(id);
    }
}
