using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Baskets;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class BasketController(IBasketService basketService) : ControllerBase
    {
        /// <summary>
        /// Create basket for user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = "User, Admin")]
        public async Task AddAsync(CreateBasketDto dto) =>
            await basketService.CreateAsync(dto);

        /// <summary>
        /// Add menu item to the basket
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("item"), Authorize(Roles = "User, Admin")]
        public async Task AddItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.AddItemAsync(dto);


        /// <summary>
        /// Change the quantity of menu item in basket
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("item"), Authorize(Roles = "User, Admin")]
        public async Task EditItemAsync(CreateMenuItemBasketDto dto) =>
            await basketService.ChangeQuantity(dto);

        /// <summary>
        /// Remove menu item from basket 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpDelete("item/{itemId}"), Authorize(Roles = "User, Admin")]
        public async Task DeleteItemAsync(Guid itemId) =>
            await basketService.RemoveItemAsync(itemId);

        /// <summary>
        /// Receive users basket by id in token
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetBasketAsync() =>
            Ok(await basketService.GetBasket());
    }
}
