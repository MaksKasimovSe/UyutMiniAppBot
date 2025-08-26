using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Topings;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class TopingController(ITopingService topingService) : ControllerBase
    {
        /// <summary>
        /// Add new toping (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task CreateAsync(CreateTopingDto dto) =>
            await topingService.CreateAsync(dto);
        /// <summary>
        /// Get topings
        /// </summary>
        /// <param name="menuItemId"></param>
        /// <returns></returns>
        [HttpGet("{menuItemId}"), AllowAnonymous]
        public async Task<IActionResult> GetAll([FromRoute] Guid menuItemId) =>
            Ok(await topingService.GetAllAsync(menuItemId));

        /// <summary>
        /// Delete toping (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync(Guid id) =>
            await topingService.DeleteAsync(id);
    }
}
