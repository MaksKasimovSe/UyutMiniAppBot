using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Category;

namespace UyutMiniApp.Service.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CreateCategoryDto dto);
        Task EditAsync(Guid id, UpdateCategoryDto dto);
        Task DeleteAsync(Guid id);
        Task<List<ViewCategoryDto>> GetAllAsync(CategoryFor categoryFor);
        Task<List<ViewCategoryDto>> GetAllStockAsync();
    }
}
