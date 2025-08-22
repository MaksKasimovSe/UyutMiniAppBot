using UyutMiniApp.Service.DTOs.Topings;

namespace UyutMiniApp.Service.Interfaces
{
    public interface ITopingService
    {
        Task CreateAsync(CreateTopingDto dto);
        Task UpdateAsync(Guid id, CreateTopingDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ViewTopingDto>> GetAllAsync(Guid menuItemId);
    }
}
