using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.IFixedRecomendations;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class FixedRecommendationController(IFixedRecomendationsService fixedRecomendationsService) : ControllerBase
    {
        /// <summary>
        /// Add new Fixed recomendation (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateFixedRecomendationDto dto) =>
            await fixedRecomendationsService.AddAsync(dto);

        /// <summary>
        /// Update Fixed recomendation (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync([FromRoute] Guid id, CreateFixedRecomendationDto dto) =>
            await fixedRecomendationsService.UpdateAsync(id, dto);

        /// <summary>
        /// Delete Fixed recomendation (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync([FromRoute] Guid id) =>
            await fixedRecomendationsService.DeleteAsync(id);

        /// <summary>
        /// Get fixced recomendations
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetTaskAsync() =>
            Ok(await fixedRecomendationsService.GetAllAsync());
    }
}
