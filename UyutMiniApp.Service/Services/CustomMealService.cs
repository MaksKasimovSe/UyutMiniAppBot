using Mapster;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.CustomMeals;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class CustomMealService(IGenericRepository<CustomMeal> genericRepository) : ICustomMealService
    {
        public async Task AddAsync(CreateCustomMealDto dto)
        {
            await genericRepository.CreateAsync(dto.Adapt<CustomMeal>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await genericRepository.DeleteAsync(c => c.Id == id);
            await genericRepository.SaveChangesAsync();
        }

        public async Task<ViewCustomMealDto> GetAsync(Guid id)
        {
            var customMeal = await genericRepository.GetAsync(c => c.Id == id);

            if (customMeal is null)
                throw new HttpStatusCodeException(404,"Custom meal dto");

            return customMeal.Adapt<ViewCustomMealDto>();
        }
    }
}
