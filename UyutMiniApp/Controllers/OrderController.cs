using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class OrderController(IOrderService orderService, IHubContext<ChatHub> hubContext, IHubContext<OrderCheckHub> orderCheckHub, IHubContext<OrderProcessHub> orderProcessHub) : ControllerBase
    {
        /// <summary>
        /// Create order enums values: 
        /// </summary>
        /// <param name="createOrderDto">PaymentMethod should be (KakaoPay: 0, NaverPay: 1, Transfer: 2, Cash: 3), OrderType should be (In Kafe: 0, Delivery: 1)</param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateAsync(CreateOrderDto createOrderDto)
        {
            var res = await orderService.CreateAsync(createOrderDto);
            await orderCheckHub.Clients.All.SendAsync("ReceiveMessage", $"{HttpContextHelper.TelegramId}:{res.Id}:{HttpContextHelper.UserId}", JsonConvert.SerializeObject(res));

            return Ok(res);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await orderService.GetAsync(id));

        /// <summary>
        /// Change order status (Admins only)
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("send"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendMessage(SendOrderMessageDto message)
        {
            await orderService.ChangeStatus(message.Id, message.Status);
            if (message.Status == OrderStatus.Paid)
            {
                var orderProcess = OrderProcess.Cooking;
                await orderService.ChangeProcess(message.Id, orderProcess);
            }
            var order = await orderService.GetAsync(message.Id);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", $"{order.User.TelegramUserId}:{message.Id.ToString()}:{order.User.Id}", Enum.GetName(message.Status));
            await orderProcessHub.Clients.All.SendAsync("ReceiveMessage", $"{order.User.TelegramUserId}:{message.Id.ToString()}:{order.User.Id}", Enum.GetName(OrderProcess.Cooking));
            return Ok(new { Status = "Message sent" });
        }

        /// <summary>
        /// Upload order receipt (Admins only)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receiptImage"></param>
        /// <returns></returns>
        [HttpPost("receipt"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadReceipt([FromQuery] Guid id, IFormFile receiptImage)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.ReceiptsPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            FileStream fileStream = System.IO.File.OpenWrite(filePath);

            await receiptImage.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();
            var url = $"/receipts/{fileName}";

            await orderService.UpdateOrderReceipt(id, url);

            return Ok(url);
        }

        /// <summary>
        /// Change process of delivery (Admins only)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("process"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrderMessage(SendProcessMessageDto dto)
        {
            await orderService.ChangeProcess(dto.Id, dto.OrderProcess);

            var order = await orderService.GetAsync(dto.Id);
            await orderProcessHub.Clients.All.SendAsync("ReceiveMessage", $"{order.User.TelegramUserId}:{dto.Id}:{order.User.Id}", Enum.GetName(dto.OrderProcess));
            return Ok(new { Status = "Message sent" });
        }

        /// <summary>
        /// Get todays orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("today"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetTodaysOrders() =>
             Ok(await orderService.GetTodaysOrders());
    }
}
