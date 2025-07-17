using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController,Route("[controller]")]
    public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]string search = "") =>
            Ok(await menuItemService.GetAllAsync(search));

        [HttpPost]
        public async Task AddAsync(CreateMenuItemDto dto) =>
            await menuItemService.CreateAsync(dto);

    }
}
