using Mapster;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
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

            if (existCategory is null)
                throw new HttpStatusCodeException(404, "Category does not exist");

            if (existCategory.ImageUrl is not null)
                FileHelper.Remove(existCategory.ImageUrl);
            await repository.DeleteAsync(c => c.Id == id);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(Guid id, UpdateCategoryDto dto)
        {
            var existCategory = await repository.GetAsync(c => c.Id == id);

            if (existCategory is null)
                throw new HttpStatusCodeException(404, "Category does not exist");

            if (existCategory.ImageUrl is not null)
                FileHelper.Remove(existCategory.ImageUrl);
            repository.Update(dto.Adapt(existCategory));
            await repository.SaveChangesAsync();
        }

        public async Task<List<ViewCategoryDto>> GetAllAsync(CategoryFor categoryFor)
        {
            var categories = repository.GetAll(false, c => c.CategoryFor == categoryFor)
                                        .Include(c => c.MenuItems)
                                            .ThenInclude(mi => mi.SetItems)
                                                .ThenInclude(si => si.ReplacementOptions)
                                                    .ThenInclude(ro => ro.ReplacementMenuItem)
                                        .Include(c => c.MenuItems)
                                            .ThenInclude(mi => mi.SetItems)
                                                .ThenInclude(si => si.IncludedItem)
                                        .Include(c => c.MenuItems)
                                            .ThenInclude(mi => mi.Topings)
                                        .Include(c => c.Ingredients).OrderBy(c => c.CreatedAt);

            var viewCategories = (await categories.ToListAsync()).Adapt<List<ViewCategoryDto>>();

            return viewCategories;
        }

        public async Task<List<ViewCategoryDto>> GetAllStockAsync()
        {
            var categories = repository.GetAll(false);

            var viewCategories = (await categories.ToListAsync()).Adapt<List<ViewCategoryDto>>();

            return viewCategories;
        }
    }
}
