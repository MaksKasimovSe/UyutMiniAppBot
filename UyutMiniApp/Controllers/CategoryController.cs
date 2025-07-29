using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Category;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateCategoryDto dto) =>
            await categoryService.AddAsync(dto);

        [HttpGet]
        public async Task<IActionResult> GetAllCategories() =>
            Ok(await categoryService.GetAllAsync());

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync(Guid id) =>
            await categoryService.DeleteAsync(id);
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync(Guid id, UpdateCategoryDto dto) =>
            await categoryService.EditAsync(id, dto);
    }
}
