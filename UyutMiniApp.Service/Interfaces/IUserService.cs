using UyutMiniApp.Service.DTOs.Users;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(CreateUserDto dto);
        Task DeleteAsync(Guid id);
        Task<ViewUserDto> GetAsync(long telegramUserId);
        Task<string> GenerateToken(long telegramUserId);
    }
}
