using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.IFixedRecomendations;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class FixedRecommendationController(IFixedRecomendationsService fixedRecomendationsService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateFixedRecomendationDto dto) =>
            await fixedRecomendationsService.AddAsync(dto);

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync([FromRoute] Guid id, CreateFixedRecomendationDto dto) =>
            await fixedRecomendationsService.UpdateAsync(id, dto);

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync([FromRoute] Guid id) =>
            await fixedRecomendationsService.DeleteAsync(id);

        [HttpGet]
        public async Task GetTaskAsync() =>
            await fixedRecomendationsService.GetAllAsync();
    }
}
