using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.CustomMeals;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomMealController(ICustomMealService customMealService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateCustomMealDto dto) =>
            await customMealService.AddAsync(dto);

        [HttpGet("{id}"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await customMealService.GetAsync(id));
    }
}
