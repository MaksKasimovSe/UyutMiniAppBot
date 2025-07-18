using UyutMiniApp.Service.DTOs.CustomMeals;

namespace UyutMiniApp.Service.Interfaces
{
    public interface ICustomMealService
    {
        public Task AddAsync(CreateCustomMealDto dto);
        public Task<ViewCustomMealDto> GetAsync(Guid id);
        public Task DeleteAsync(Guid id);
    }
}
