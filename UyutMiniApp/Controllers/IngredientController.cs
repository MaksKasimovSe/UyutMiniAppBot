using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Ingredients;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class IngredientController(IIngredientService ingredientService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync([FromForm] CreateIngredientDto dto)
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

            await ingredientService.CreateAsync(dto);
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync([FromRoute] Guid id, [FromForm] UpdateIngredientDto dto)
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
            await ingredientService.UpdateAsync(id, dto);
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id) =>
            Ok(await ingredientService.GetAsync(id));

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync([FromRoute] Guid id) =>
            await ingredientService.DeleteAsync(id);

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await ingredientService.GetAllAsync());

    }
}
