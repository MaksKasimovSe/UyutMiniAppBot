using UyutMiniApp.Service.DTOs.Ingredients;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IIngredientService
    {
        Task CreateAsync(CreateIngredientDto dto);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateIngredientDto dto);
        Task<IEnumerable<ViewIngredientDto>> GetAllAsync();
        Task<ViewIngredientDto> GetAsync(Guid id);
    }
}
