using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Interfaces;
using UyutMiniApp.Signalr;

namespace UyutMiniApp.Controllers
{
    
    [ApiController, Route("[controller]")]
    public class OrderController(IOrderService orderService, IHubContext<ChatHub> hubContext, IHubContext<OrderCheckHub> orderCheckHub) : ControllerBase
    {
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync(CreateOrderDto createOrderDto) =>
            Ok(await orderService.CreateAsync(createOrderDto));

        [HttpGet("{id}"), Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await orderService.GetAsync(id));

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromForm] string user, [FromForm] string message)
        {
            await hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok(new { Status = "Message sent" });
        }
    }
}
