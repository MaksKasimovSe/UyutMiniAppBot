using Microsoft.AspNetCore.Mvc;
using UyutMiniApp.Service.DTOs.Users;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(CreateUserDto dto)
        {
            await userService.AddAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto dto) =>
            Ok(await userService.GenerateToken(dto.TelegramUserId, dto.PhoneNumber));

        [HttpGet("{telegramUserId}")]
        public async Task<IActionResult> GetAsync(long telegramUserId) 
            => Ok(await userService.GetAsync(telegramUserId));
    }
}
