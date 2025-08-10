using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;
using System.Net;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;
using static System.Net.Mime.MediaTypeNames;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CourierController(ICourierService courierService, IHubContext<OrderProcessHub> hubContext, IHubContext<CourierHub> courierHub) : ControllerBase
    {
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateCourierDto dto) =>
            await courierService.CreateAsync(dto);

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto) =>
            Ok(await courierService.GenerateToken(dto.TelegramUserId));

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync(Guid id, UpdateCourierDto dto) =>
            await courierService.UpdateAsync(id, dto);
        
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id) =>
            await courierService.DeleteAsync(id);

        [HttpGet("{telegramUserId}"), Authorize(Roles = "Courier, Admin")]
        public async Task<IActionResult> GetAsync(long telegramUserId) =>
            Ok(await courierService.GetByIdAsync(telegramUserId));

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await courierService.GetAllAsync());

        [HttpPatch("start-day"), Authorize(Roles = "Courier")]
        public async Task StartWorkingDay() =>
            await courierService.StartWorkingDay();

        [HttpPatch("end-day"), Authorize(Roles = "Courier")]
        public async Task EndWorkingDay() =>
            await courierService.EndWorkingDay();

        [HttpPatch("start-delivery/{orderId}"), Authorize(Roles = "Courier")]
        public async Task StartDelivery([FromRoute] Guid orderId)
        {
            await courierService.StartDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivering));
        }
        [HttpPatch("finish-delivery/{orderId}"), Authorize(Roles = "Courier")]
        public async Task EndDelivery([FromRoute] Guid orderId)
        {
            await courierService.FinishDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivered));
        }

        [HttpPatch("accept-order/{orderId}"), Authorize(Roles = "Courier")]
        public async Task AcceptOrder([FromRoute] Guid orderId)
        {
            var courier = await courierService.GetByIdAsync(long.Parse(HttpContextHelper.TelegramId));
            var text = $"Курьер {courier.Name} - {courier.PhoneNumber} принял заказ и направляестся в кафе";

            await courierService.AcceptOrder(orderId);
            await courierHub.Clients.All.SendAsync("ReceiveMessage", orderId, text);
        }

        [HttpPatch("reject-order/{orderId}"), Authorize(Roles = "Courier")]
        public async Task RejectOrder([FromRoute] Guid orderId)
        {
            var courier = await courierService.GetByIdAsync(long.Parse(HttpContextHelper.TelegramId));
            var text = $"Курьер {courier.Name} - {courier.PhoneNumber} отменил заказ";
            try
            {
                await courierService.RejectOrder(orderId);
            }
            catch (HttpStatusCodeException)
            {
                text += "\nНет свободных курьеров для передачи заказа";
            }
            finally
            {
                await courierHub.Clients.All.SendAsync("ReceiveMessage", orderId, text);
            }
        }
    }
}
