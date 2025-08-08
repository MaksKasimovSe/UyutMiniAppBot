using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class OrderService(IGenericRepository<Order> genericRepository, 
        IGenericRepository<SavedAddress> addressRepository,
        IGenericRepository<User> userRepository,
        IGenericRepository<MenuItem> menuItemRepository,
        IGenericRepository<Courier> courierRepository) : IOrderService
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


            if (dto.Type == OrderType.Delivery) 
            {
                if (dto.DeliveryInfo is null)
                    throw new HttpStatusCodeException(400,"Adress should be given");
                if (!dto.DeliveryInfo.Address.Contains("경기도 평택시 포승읍"))
                {
                    throw new HttpStatusCodeException(400, "We don't deliver to that address");
                }
            }


            var newOrder = dto.Adapt<Order>();

            newOrder.Status = OrderStatus.Pending;
            var lastOrder = await genericRepository.GetAll(false).OrderByDescending(o => o.CreatedAt).FirstOrDefaultAsync();
            if (lastOrder is null)
                newOrder.OrderNumber = 1;
            else if (lastOrder.OrderNumber == 100)
                newOrder.OrderNumber = 1;
            else
                newOrder.OrderNumber = ++lastOrder.OrderNumber;
            if (newOrder.PaymentMethod == PaymentMethod.Transfer)
                totalPrice = totalPrice - newOrder.OrderNumber;

            var orders = genericRepository.GetAll(false,o => o.UserId == HttpContextHelper.UserId);
            if (orders.Count() == 0)
                totalPrice -= (totalPrice / 100 * 10);

            newOrder.TotalAmount = totalPrice;

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

            var resOrder = await genericRepository.GetAsync(o => o.Id == newOrder.Id, includes:["User", "Courier", "DeliveryInfo", "Items", "Items.MenuItem"], isTracking: false);
           
            return newOrder.Adapt<ViewOrderDto>();
        }

        public async Task<ViewOrderDto> GetAsync(Guid id)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id,includes: ["User", "Courier", "DeliveryInfo", "Items", "Items.MenuItem"], isTracking:false);

            if (existOrder is null)
                throw new HttpStatusCodeException(404, "order not found");

            return existOrder.Adapt<ViewOrderDto>();
        }

        public async Task<string> UpdateOrderReceipt(Guid id, string url)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Order not found");
            existOrder.OrderUrl = url;
            genericRepository.Update(existOrder);
            await genericRepository.SaveChangesAsync();
            
            return url;
        }

        public async Task ChangeStatus(Guid id, OrderStatus status)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id, ["Items.MenuItem","User","DeliveryInfo"]);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Order not found");
            existOrder.Status = status;
            genericRepository.Update(existOrder);

            await genericRepository.SaveChangesAsync();
            if (status == OrderStatus.Paid)
            {
                if (existOrder.Type == OrderType.Delivery)
                {
                    var availableCouriers = courierRepository.GetAll(
                        false, c => c.IsAvailable == true && c.IsWorking == true);
                    if (availableCouriers.Count() == 0)
                        throw new HttpStatusCodeException(400, "No active couriers");

                    string botToken = "8259246379:AAH4rLnUXnriLV31BNLahU8O7LkNxI4x8Ro";
                    string messageText =
                        $"Новый заказ на имя: {existOrder.User.Name}\n\nНомер заказа: {existOrder.OrderNumber}\nАддресс: {existOrder.DeliveryInfo.Address}\nНомер телефона: {existOrder.User.PhoneNumber}\n\nПозиции:\n";
                    string url = $"https://api.telegram.org/bot{botToken}/sendMessage";
                    foreach (var meals in existOrder.Items)
                    {
                        messageText += $"{meals.MenuItem.Name} {meals.MenuItem.Price}₩\n";
                    }
                    messageText += $"\n\n Коментарий: {existOrder.DeliveryInfo.Comment}";
                    foreach (var c in availableCouriers)
                    {
                        var payload = new
                        {
                            chat_id = c.TelegramUserId,
                            text = messageText,
                            reply_markup = new
                            {
                                inline_keyboard = new List<object>
                            {
                                new[]
                                {
                                    new {
                                        text = "✅ Принять",
                                        callback_data = $"accepted:{existOrder.Id}"
                                    }
                                },
                                new[]
                                {
                                    new {
                                        text = "❌ Отказаться",
                                        callback_data = $"rejected:{existOrder.Id}"
                                    }
                                }
                            }
                            }
                        };

                        using var client = new HttpClient();
                        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(url, content);
                        string responseText = await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        public async Task ChangeProcess(Guid id, OrderProcess orderProcess)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Order not found");

            existOrder.OrderProcess = orderProcess;
            genericRepository.Update(existOrder);
            await genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewOrderDto>> GetTodaysOrders()
        {
            var orders = genericRepository.GetAll(false, o => o.CreatedAt.Date == DateTime.UtcNow.Date, includes: ["Items", "Items.MenuItem","User"]);

            var dtoOrders = await orders.Where(o => o.User.Id == HttpContextHelper.UserId).ToListAsync();
            
            return dtoOrders.Adapt<List<ViewOrderDto>>();
        }

        public async Task<IEnumerable<ViewOrderDto>> GetPendingOrders()
        {
            var orders = genericRepository.GetAll(false, o => o.CourierId == null, includes: ["Items", "Items.MenuItem", "User", "DeliveryInfo"]);

            var dtoOrders = await orders.ToListAsync();

            return dtoOrders.Adapt<List<ViewOrderDto>>();
        }
    }
}
