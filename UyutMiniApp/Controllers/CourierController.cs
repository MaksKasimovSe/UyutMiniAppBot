using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class CourierController(ICourierService courierService) : ControllerBase
    {
        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task AddAsync(CreateCourierDto dto) =>
            await courierService.CreateAsync(dto);

        [HttpPost("login")]
        public async Task LoginAsync(LoginUserDto dto) =>
            Ok(await courierService.GenerateToken(dto.TelegramUserId, dto.PhoneNumber));

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task UpdateAsync(Guid id, UpdateCourierDto dto) =>
            await courierService.UpdateAsync(id, dto);

        [HttpGet("{telegramUserId}")]
        public async Task<IActionResult> GetAsync(long telegramUserId) =>
            Ok(await courierService.GetByIdAsync(telegramUserId));

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await courierService.GetAllAsync());
    }
}
