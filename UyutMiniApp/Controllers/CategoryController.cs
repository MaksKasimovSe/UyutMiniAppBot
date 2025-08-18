using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Category;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync([FromForm] CreateCategoryDto dto)
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

            await categoryService.AddAsync(dto);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllCategories([FromQuery] CategoryFor categoryFor) =>
            Ok(await categoryService.GetAllAsync(categoryFor));
        
        [HttpGet("stock"), AllowAnonymous]
        public async Task<IActionResult> GetAllStockCategories() =>
            Ok(await categoryService.GetAllStockAsync());

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync(Guid id) =>
            await categoryService.DeleteAsync(id);
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync(Guid id, [FromForm] UpdateCategoryDto dto)
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
            await categoryService.EditAsync(id, dto);
        }
    }
}
