using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    
    [ApiController, Route("[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost, Authorize]
        public async Task<IActionResult> CreateAsync(CreateOrderDto createOrderDto) =>
            Ok(await orderService.CreateAsync(createOrderDto));

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetAsync(Guid id) =>
            Ok(await orderService.GetAsync(id));
    }
}
