using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public async Task DeleteCourierAsync(Guid id)
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
            var couriers = genericRepository.GetAll(false);

            var dtoCouriers = (await couriers.ToListAsync()).Adapt<List<ViewCourierDto>>();

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
                throw new HttpStatusCodeException(404, "Delivery not found");
            if (existOrder.CourierId is not null)
                throw new HttpStatusCodeException(404, "Order is taken by another courier");

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
    }
}
