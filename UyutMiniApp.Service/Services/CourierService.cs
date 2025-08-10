using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class CourierService(IGenericRepository<Courier> genericRepository, IGenericRepository<Order> orderRepository, IConfiguration configuration) : ICourierService
    {
        public async Task CreateAsync(CreateCourierDto dto)
        {
            var existCourier = await genericRepository.GetAsync(c => c.PhoneNumber == dto.PhoneNumber);
            if (existCourier is not null)
                throw new HttpStatusCodeException(400, "Couries with this number already exist");

            await genericRepository.CreateAsync(dto.Adapt<Courier>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var res = await genericRepository.DeleteAsync(c => c.Id == id);
            if (!res)
                throw new HttpStatusCodeException(404, "Courier not found");

            await genericRepository.SaveChangesAsync();
        }
        public async Task<string> GenerateToken(long telegramUserId)
        {
            var existCourier =
                await genericRepository.GetAsync(c => c.TelegramUserId == telegramUserId);
            if (existCourier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", existCourier.Id.ToString()),
                    new Claim("TelegramUserId", existCourier.TelegramUserId.ToString()),
                    new Claim(ClaimTypes.Role, "Courier")
                }),
                Expires = DateTime.UtcNow.AddMonths(int.Parse(configuration["JWT:lifetime"])),
                Issuer = configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
        public async Task<IEnumerable<ViewCourierDto>> GetAllAsync()
        {
            IQueryable<Courier> couriers = genericRepository.GetAll(false);


            var dtoCouriers = (await couriers.ToListAsync()).Adapt<List<ViewCourierDto>>();
            foreach (var dto in dtoCouriers)
            {
                orderRepository.GetAll(false, o => o.CourierId == dto.Id);
                dto.NumberOfOrders = dtoCouriers.Count();
            }
            
            return dtoCouriers;
        }

        public async Task<ViewCourierDto> GetByIdAsync(long telegramUserId)
        {
            var courier = await genericRepository.GetAsync(c => c.TelegramUserId == telegramUserId);

            if (courier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            var dtoCourier = courier.Adapt<ViewCourierDto>();
            return dtoCourier;
        }

        public async Task UpdateAsync(Guid id, UpdateCourierDto dto)
        {
            var existCourier = await genericRepository.GetAsync(c => c.Id == id);

            if (existCourier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            var dtoCourier = genericRepository.Update(dto.Adapt(existCourier));
            await genericRepository.SaveChangesAsync();
        }

        public async Task StartWorkingDay()
        {
            var existCourier = await genericRepository.GetAsync(c => c.Id == HttpContextHelper.UserId);;
            if (existCourier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            existCourier.IsWorking = true;
            genericRepository.Update(existCourier);
            await genericRepository.SaveChangesAsync();
        }
        public async Task EndWorkingDay()
        {
            var existCourier = await genericRepository.GetAsync(c => c.Id == HttpContextHelper.UserId); ;
            if (existCourier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            existCourier.IsWorking = false;
            genericRepository.Update(existCourier);
            await genericRepository.SaveChangesAsync();
        }

        public async Task StartDelivery(Guid orderId)
        {
            var existOrder = await orderRepository.GetAsync(o => o.Id == orderId);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Delivery not found");
            if (existOrder.OrderProcess == OrderProcess.Delivering ||
                existOrder.OrderProcess == OrderProcess.Delivered ||
                existOrder.Status == OrderStatus.Failed)
                throw new HttpStatusCodeException(404, "Order is unavailable now");

            existOrder.OrderProcess = OrderProcess.Delivering;
            orderRepository.Update(existOrder);
            await orderRepository.SaveChangesAsync();
        }

        public async Task FinishDelivery(Guid orderId)
        {
            var existOrder = await orderRepository.GetAsync(o => o.Id == orderId);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Delivery not found");
            var existCourier = await genericRepository.GetAsync(o => o.Id == HttpContextHelper.UserId);
            if (existCourier is null)
                throw new HttpStatusCodeException(404, "Courier not found");

            existCourier.IsAvailable = true;

            genericRepository.Update(existCourier);
            await genericRepository.SaveChangesAsync();

            existOrder.OrderProcess = OrderProcess.Delivered;
            orderRepository.Update(existOrder);
            await orderRepository.SaveChangesAsync();
        }

        public async Task AcceptOrder(Guid orderId)
        {
            var existOrder = await orderRepository.GetAsync(o => o.Id == orderId);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Order not found");
            if (existOrder.CourierId is not null)
                throw new HttpStatusCodeException(400, "Order is taken by another courier");

            var existCourier = await genericRepository.GetAsync(o => o.Id == HttpContextHelper.UserId);
            if (existCourier is null)
                throw new HttpStatusCodeException(404,"Courier not found");

            existCourier.IsAvailable = false;

            genericRepository.Update(existCourier);
            await genericRepository.SaveChangesAsync();
            
            existOrder.CourierId = HttpContextHelper.UserId;
            

            orderRepository.Update(existOrder);
            await orderRepository.SaveChangesAsync();
        }
        public async Task RejectOrder(Guid orderId)
        {
            var existOrder = await orderRepository.GetAsync(o => o.Id == orderId, ["User", "Courier", "DeliveryInfo", "Items", "Items.MenuItem"]);
            if (existOrder is null)
                throw new HttpStatusCodeException(404, "Order not found");
            
            if (existOrder.CourierId is not null && existOrder.CourierId == HttpContextHelper.UserId)
            {
                existOrder.CourierId = null;

                var availableCouriers = await genericRepository.GetAll(
                                        false, c => c.IsAvailable == true && c.IsWorking == true).ToListAsync();
                if (availableCouriers.Count == 0)
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
                    var existCourier = await genericRepository.GetAsync(c => c.Id == HttpContextHelper.UserId);
                    if (existCourier is not null)
                    {
                        existCourier.IsAvailable = true;
                        genericRepository.Update(existCourier);
                        await genericRepository.SaveChangesAsync();
                    }
                }
            }
            orderRepository.Update(existOrder);
            await orderRepository.SaveChangesAsync();
        }
    }
}
