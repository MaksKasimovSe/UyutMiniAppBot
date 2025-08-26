using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.CustomMeals;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CustomMealController(ICustomMealService customMealService) : ControllerBase
    {
        /// <summary>
        /// Create custom meal
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = "User, Admin")]
        public async Task AddAsync(CreateCustomMealDto dto) =>
            await customMealService.AddAsync(dto);

        /// <summary>
        /// Get custom meal by id
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await customMealService.GetAsync(id));
    }
}
