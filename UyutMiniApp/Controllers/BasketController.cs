using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Basket;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class BasketController(IBasketService basketService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "User, Admin")]
        public async Task AddAsync(CreateBasketDto dto) =>
            await basketService.CreateAsync(dto);

        [HttpPost("item"), Authorize(Roles = "User, Admin")]
        public async Task AddItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.AddItemAsync(dto);

        [HttpPut("item"), Authorize(Roles = "User, Admin")]
        public async Task EditItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.ChangeQuantity(dto);

        [HttpDelete("item/{itemId}"), Authorize(Roles = "User, Admin")]
        public async Task DeleteItemAsync(Guid itemId) =>
            await basketService.RemoveItemAsync(itemId);

        [HttpGet, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetBasketAsync() =>
            Ok(await basketService.GetBasket());
    }
}
