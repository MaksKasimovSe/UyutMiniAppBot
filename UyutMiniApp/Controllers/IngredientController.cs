using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Ingredients;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")] 
    public class IngredientController(IIngredientService ingredientService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateIngredientDto dto) => 
            await ingredientService.CreateAsync(dto);

        [HttpPut("id"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync([FromRoute]Guid id, UpdateIngredientDto dto) =>
            await ingredientService.UpdateAsync(id, dto);

        [HttpGet("id")]
        public async Task<IActionResult> GetAsync([FromRoute]Guid id) =>
            Ok(await ingredientService.GetAsync(id));

        [HttpDelete("id"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync([FromRoute] Guid id) =>
            await ingredientService.DeleteAsync(id);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await ingredientService.GetAllAsync());

    }
}
