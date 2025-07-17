using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Couriers;

namespace UyutMiniApp.Service.Interfaces
{
    public interface ICourierService
    {
        public Task CreateAsync(CreateCourierDto dto);
        public Task DeleteCourierAsync(Guid id);
        public Task UpdateAsync(Guid id, UpdateCourierDto dto);
        public Task<IEnumerable<ViewCourierDto>> GetAllAsync();
        public Task<ViewCourierDto> GetByIdAsync(long telegramUserId);
        Task<string> GenerateToken(long telegramUserId, string phoneNumber);
    }
}
