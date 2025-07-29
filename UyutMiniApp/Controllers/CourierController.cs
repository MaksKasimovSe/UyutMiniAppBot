using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CourierController(ICourierService courierService, IHubContext<OrderProcessHub> hubContext) : ControllerBase
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

        [HttpPatch("start-day")]
        public async Task StartWorkingDay() =>
            await courierService.StartWorkingDay();

        [HttpPatch("end-day")]
        public async Task EndWorkingDay() =>
            await courierService.EndWorkingDay();

        [HttpPatch("start-delivery/{orderId}")]
        public async Task StartDelivery([FromRoute] Guid orderId)
        {
            await courierService.StartDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivering));
        }
        [HttpPatch("finish-delivery/{orderId}")]
        public async Task EndDelivery([FromRoute] Guid orderId)
        {
            await courierService.FinishDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivered));
        }

        [HttpPatch("accept-order/{orderId}")]
        public async Task AcceptOrder([FromRoute] Guid orderId) =>
            await courierService.AcceptOrder(orderId);
    }
}
