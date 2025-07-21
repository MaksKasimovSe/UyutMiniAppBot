using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class OrderService(IGenericRepository<Order> genericRepository, 
        IGenericRepository<SavedAddress> addressRepository,
        IGenericRepository<User> userRepository,
        IGenericRepository<MenuItem> menuItemRepository) : IOrderService
    {
        public async Task<ViewOrderDto> CreateAsync(CreateOrderDto dto)
        {
            decimal totalPrice = 0;
            foreach (var i in dto.Items)
            {
                var menuItem = await menuItemRepository.GetAsync(mi => mi.Id == i.MenuItemId);
                if (menuItem is null)
                    throw new HttpStatusCodeException(404, "Menu item not found");

                totalPrice += menuItem.Price * i.Quantity;
            }

            totalPrice += dto.DeliveryInfo is null ? 0 : dto.DeliveryInfo.Fee;
            dto.TotalAmount = totalPrice;

            var newOrder = dto.Adapt<Order>();

            var lastOrder = await genericRepository.GetAll().OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync();
            if (lastOrder is null)
                newOrder.OrderNumber = 1;
            else if (lastOrder.OrderNumber == 100)
                newOrder.OrderNumber = 1;
            else
                newOrder.OrderNumber = lastOrder.OrderNumber++;

            newOrder = await genericRepository.CreateAsync(newOrder);

            if (dto.DeliveryInfo is not null)
            {
                var address = new SavedAddress()
                {
                    Address = dto.DeliveryInfo.Address,
                    Floor = dto.DeliveryInfo.Floor,
                    Entrance = dto.DeliveryInfo.Entrance,
                };

                var savedAddress = await addressRepository.CreateAsync(address);

                var user = await userRepository.GetAsync(u => u.TelegramUserId == long.Parse(HttpContextHelper.TelegramId));
                user.SavedAddressId = savedAddress.Id;
                userRepository.Update(user);
            }
            await genericRepository.SaveChangesAsync();

            var resOrder = await genericRepository.GetAsync(o => o.Id == newOrder.Id, includes:["User", "Courier", "DeliveryInfo", "Items", "Items.MenuItem"]);
           
            return newOrder.Adapt<ViewOrderDto>();
        }

        public async Task<ViewOrderDto> GetAsync(Guid id)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id);

            if (existOrder is null)
                throw new HttpStatusCodeException(404, "order not found");

            return existOrder.Adapt<ViewOrderDto>();
        }
    }
}
