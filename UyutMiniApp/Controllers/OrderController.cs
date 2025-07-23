using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class OrderController(IOrderService orderService, IHubContext<ChatHub> hubContext, IHubContext<OrderCheckHub> orderCheckHub) : ControllerBase
    {
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync(CreateOrderDto createOrderDto)
        {
            var res = await orderService.CreateAsync(createOrderDto);
            await orderCheckHub.Clients.All.SendAsync("ReceiveMessage", HttpContextHelper.TelegramId, JsonConvert.SerializeObject(res));
            return Ok(res);
        }

        [HttpGet("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await orderService.GetAsync(id));

        [HttpPost("send/{userId}")]
        public async Task<IActionResult> SendMessage([FromRoute] Guid userId, [FromForm] UpdateOrderStatusDto status)
        {
            await orderService.ChangeStatus(status.Id, status.Status);
            await hubContext.Clients.All.SendAsync("ReceiveMessage", userId.ToString(), Enum.GetName(status.Status));
            return Ok(new { Status = "Message sent" });
        }

        [HttpPost("receipt/{id}")]
        public async Task<IActionResult> UploadReceipt(Guid id, [FromForm] IFormFile receiptImage)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

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
    }
}
