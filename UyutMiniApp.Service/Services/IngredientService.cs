using Mapster;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Ingredients;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class IngredientService(IGenericRepository<Ingredient> genericRepository) : IIngredientService
    {
        public async Task CreateAsync(CreateIngredientDto dto)
        {
            var existIngredient = await genericRepository.GetAsync(i => i.Name == dto.Name);
            if (existIngredient is not null)
                throw new HttpStatusCodeException(400, "Ingredient with such name already exist");

            await genericRepository.CreateAsync(dto.Adapt<Ingredient>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var isIngredient = await genericRepository.DeleteAsync(i => i.Id == id);
            if (!isIngredient)
                throw new HttpStatusCodeException(404, "Ingredient not found");
            await genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewIngredientDto>> GetAllAsync()
        {
            var ingredients = genericRepository.GetAll(false);
            var dtoIngredients = (await ingredients.ToListAsync()).Adapt<IEnumerable<ViewIngredientDto>>();

            return dtoIngredients;
        }

        public async Task<ViewIngredientDto> GetAsync(Guid id)
        {
            var ingredient = await genericRepository.GetAsync(i => i.Id == id);
            if (ingredient is null)
                throw new HttpStatusCodeException(404, "Ingredient not found");

            return ingredient.Adapt<ViewIngredientDto>();
        }

        public async Task UpdateAsync(Guid id, UpdateIngredientDto dto)
        {
            var ingredient = await genericRepository.GetAsync(i => i.Id == id);
            if (ingredient is null)
                throw new HttpStatusCodeException(404, "Ingredient not found");

            genericRepository.Update(dto.Adapt(ingredient));
            await genericRepository.SaveChangesAsync();
        }
    }
}
