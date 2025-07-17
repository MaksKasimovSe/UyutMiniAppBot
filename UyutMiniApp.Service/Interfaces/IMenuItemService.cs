using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.Interfaces
{ 
    public interface IMenuItemService 
    {
        Task CreateAsync(CreateMenuItemDto dto);
        Task UpdateAsync(Guid id, UpdateMenuItemDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ViewMenuItemDto>> GetAllAsync(string search);
    }
}
