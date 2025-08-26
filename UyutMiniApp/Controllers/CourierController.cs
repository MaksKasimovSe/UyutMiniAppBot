using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CourierController(ICourierService courierService, IHubContext<OrderProcessHub> hubContext, IHubContext<CourierHub> courierHub) : ControllerBase
    {
        /// <summary>
        /// Register new courier (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateCourierDto dto) =>
            await courierService.CreateAsync(dto);

        /// <summary>
        /// Login as courier
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto) =>
            Ok(await courierService.GenerateToken(dto.TelegramUserId));

        /// <summary>
        /// Change courier profile info (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync(Guid id, UpdateCourierDto dto) =>
            await courierService.UpdateAsync(id, dto);

        /// <summary>
        /// Delete courier account (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task DeleteAsync(Guid id) =>
            await courierService.DeleteAsync(id);

        /// <summary>
        /// Get courier info by telegram id 
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="telegramUserId"></param>
        /// <returns></returns>
        [HttpGet("{telegramUserId}"), Authorize(Roles = "Courier, Admin")]
        public async Task<IActionResult> GetAsync(long telegramUserId) =>
            Ok(await courierService.GetByIdAsync(telegramUserId));

        /// <summary>
        /// Get all couriers (Admins only)
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await courierService.GetAllAsync());

        /// <summary>
        /// Start working day for couriers
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <returns></returns>
        [HttpPatch("start-day"), Authorize(Roles = "Courier")]
        public async Task StartWorkingDay() =>
            await courierService.StartWorkingDay();

        /// <summary>
        /// End working day for couriers
        /// </summary>
        /// <returns></returns>
        [HttpPatch("end-day"), Authorize(Roles = "Courier")]
        public async Task EndWorkingDay() =>
            await courierService.EndWorkingDay();

        /// <summary>
        /// Start order delivery
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPatch("start-delivery/{orderId}"), Authorize(Roles = "Courier")]
        public async Task StartDelivery([FromRoute] Guid orderId)
        {
            await courierService.StartDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivering));
        }
        /// <summary>
        /// Finish order delivery
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPatch("finish-delivery/{orderId}"), Authorize(Roles = "Courier")]
        public async Task EndDelivery([FromRoute] Guid orderId)
        {
            await courierService.FinishDelivery(orderId);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", orderId, Enum.GetName(OrderProcess.Delivered));
        }

        /// <summary>
        /// Accept order for delivering
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPatch("accept-order/{orderId}"), Authorize(Roles = "Courier")]
        public async Task AcceptOrder([FromRoute] Guid orderId)
        {
            var courier = await courierService.GetByIdAsync(long.Parse(HttpContextHelper.TelegramId));
            var text = $"Курьер {courier.Name} - {courier.PhoneNumber} принял заказ и направляестся в кафе";

            await courierService.AcceptOrder(orderId);
            await courierHub.Clients.All.SendAsync("ReceiveMessage", orderId, text);
        }

        /// <summary>
        /// Cancel order delivery
        /// </summary>
        /// <remarks>
        /// Auth required
        /// </remarks>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
