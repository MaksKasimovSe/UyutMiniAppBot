using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Topings;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class TopingController(ITopingService topingService) : ControllerBase
    {
        [HttpPost]
        public async Task CreateAsync(CreateTopingDto dto) =>
            await topingService.CreateAsync(dto);

        [HttpGet("{menuItemId}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid menuItemId) =>
            Ok(await topingService.GetAllAsync(menuItemId));

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id) =>
            await topingService.DeleteAsync(id);
    }
}
