using Mapster;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Baskets;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class BasketService(IGenericRepository<Basket> genericRepository) : IBasketService
    {
        public async Task AddItemAsync(CreateMenuItemBasketDto dto)
        {
            var basket = await genericRepository.GetAsync(b => b.UserId == HttpContextHelper.UserId, includes: ["MenuItemsBaskets"]);
            if (basket == null)
                throw new HttpStatusCodeException(400, "Basket does not exist create it first");

            basket.MenuItemsBaskets.Add(dto.Adapt<MenuItemBasket>());

            await genericRepository.SaveChangesAsync();
        }

        public async Task ChangeQuantity(CreateMenuItemBasketDto dto)
        {
            var basket = await genericRepository.GetAsync(b => b.UserId == HttpContextHelper.UserId, includes: ["MenuItemsBaskets"]);
            if (basket is null)
                throw new HttpStatusCodeException(400, "Basket does not exist create it first");
            var item = basket.MenuItemsBaskets.FirstOrDefault(mb => mb.MenuItemId == dto.MenuItemId);
            if (item is null)
                throw new HttpStatusCodeException(400, "Item is not in basket");
            item.Quantity = dto.Quantity;

            await genericRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateBasketDto dto)
        {
            var alreadyExistBasket = await genericRepository.GetAsync(b => b.UserId == HttpContextHelper.UserId, isTracking: false);
            if (alreadyExistBasket is not null)
                throw new HttpStatusCodeException(400, "Basket already exist");
            var entityBasket = dto.Adapt<Basket>();
            entityBasket.UserId = (Guid)HttpContextHelper.UserId;

            await genericRepository.CreateAsync(entityBasket);
            await genericRepository.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(Guid menuItemId)
        {
            var basket = await genericRepository.GetAsync(b => b.UserId == HttpContextHelper.UserId, includes: ["MenuItemsBaskets"]);
            if (basket is null)
                throw new HttpStatusCodeException(400, "Basket does not exist create it first");
            var item = basket.MenuItemsBaskets.FirstOrDefault(mb => mb.MenuItemId == menuItemId);
            if (item is null)
                throw new HttpStatusCodeException(400, "Item is not in basket");

            basket.MenuItemsBaskets.Remove(item);

            await genericRepository.SaveChangesAsync();
        }

        public async Task<ViewBasketDto> GetBasket()
        {
            var basket = await genericRepository.GetAsync(b => b.UserId == HttpContextHelper.UserId, includes: ["MenuItemsBaskets", "MenuItemsBaskets.MenuItem", "MenuItemsBaskets.Topings", "MenuItemsBaskets.Topings.Toping", "MenuItemsBaskets.ReplacementSelection.ReplacementMenuItem"], isTracking: false);
            if (basket is null)
                throw new HttpStatusCodeException(404, "Basket does not exist create it first");

            return basket.Adapt<ViewBasketDto>();
        }
    }
}
