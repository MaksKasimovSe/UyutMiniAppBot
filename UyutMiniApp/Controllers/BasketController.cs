using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Basket;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class BasketController(IBasketService basketService) : ControllerBase
    {
        [HttpPost]
        public async Task AddAsync(CreateBasketDto dto) => 
            await basketService.CreateAsync(dto);

        [HttpPost("item")]
        public async Task AddItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.AddItemAsync(dto);

        [HttpPut("item")]
        public async Task EditItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.ChangeQuantity(dto);

        [HttpDelete("item/{itemId}")]
        public async Task DeleteItemAsync(Guid itemId) =>
            await basketService.RemoveItemAsync(itemId);

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync() =>
            Ok(await basketService.GetBasket());
    }
}
