using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Category;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace UyutMiniApp.Service.Services
{
    public class CategoryService(IGenericRepository<Category> repository) : ICategoryService
    {
        public async Task AddAsync(CreateCategoryDto dto)
        {
            var existCategory = await repository.GetAsync(c => c.Name == dto.Name, includes: ["MenuItems", "Ingredients"]);
            if (existCategory is not null)
                throw new HttpStatusCodeException(400, "Category with given name already exist");

            await repository.CreateAsync(dto.Adapt<Category>());
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existCategory = await repository.GetAsync(c => c.Id == id);

            if (existCategory is not null)
                throw new HttpStatusCodeException(404,"Category does not exist");

            await repository.DeleteAsync(c => c.Id == id);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(Guid id, UpdateCategoryDto dto)
        {
            var existCategory = await repository.GetAsync(c => c.Id == id);

            if (existCategory is not null)
                throw new HttpStatusCodeException(404, "Category does not exist");

            repository.Update(dto.Adapt(existCategory));
            await repository.SaveChangesAsync();
        }

        public async Task<List<ViewCategoryDto>> GetAllAsync()
        {
            var categories = repository.GetAll(false, includes: [ "MenuItems", "Ingredients"]);

            var viewCategories = (await categories.ToListAsync()).Adapt<List<ViewCategoryDto>>();

            return viewCategories;
        }
    }
}
