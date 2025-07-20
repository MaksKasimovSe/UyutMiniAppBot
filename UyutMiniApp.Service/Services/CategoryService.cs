using Mapster;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Category;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class CategoryService(IGenericRepository<Category> repository) : ICategoryService
    {
        public async Task AddAsync(CreateCategoryDto dto)
        {
            var existCategory = await repository.GetAsync(c => c.Name == dto.Name);
            if (existCategory is not null)
                throw new HttpStatusCodeException(400, "Category with given name already exist");
            var category = dto.Adapt<Category>();

            await repository.CreateAsync(dto.Adapt<Category>());
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existCategory = await repository.GetAsync(c => c.Id == id);

            if (existCategory is not null)
                throw new HttpStatusCodeException(404, "Category does not exist");

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
            var categories = repository.GetAll(false, includes: ["MenuItems", "Ingredients", "MenuItems.SetItems", "MenuItems.SetItems.IncludedItem", "MenuItems.SetItems.ReplacementOptions", "MenuItems.SetItems.ReplacementOptions.ReplacementMenuItem"]);

            var viewCategories = (await categories.ToListAsync()).Adapt<List<ViewCategoryDto>>();
            var role = HttpContextHelper.Role;
            return viewCategories;
        }
    }
}
