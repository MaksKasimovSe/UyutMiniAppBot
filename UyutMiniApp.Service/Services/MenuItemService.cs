using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Helpers;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class MenuItemService(IGenericRepository<MenuItem> genericRepository) : IMenuItemService
    {
        public async Task CreateAsync(CreateMenuItemDto dto)
        {
            var menuItem = dto.Adapt<MenuItem>();
            await genericRepository.CreateAsync(dto.Adapt<MenuItem>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existMenuItem = await genericRepository.GetAsync(c => c.Id == id);
            var isDeleted = await genericRepository.DeleteAsync(c => c.Id == id);
            if (!isDeleted)
                throw new HttpStatusCodeException(404, "Category not found");
            FileHelper.Remove(existMenuItem.ImageUrl);

            await genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewMenuItemDto>> GetAllAsync(string search)
        {
            var menuItems = genericRepository.GetAll(false, m => m.Name.Contains(search));

            var dtoMenuItems = (await menuItems.ToListAsync()).Adapt<List<ViewMenuItemDto>>();

            return dtoMenuItems;
        }

        public async Task UpdateAsync(Guid id, UpdateMenuItemDto dto)
        {
            var existMenuItem = await genericRepository.GetAsync(mi => mi.Id == id);
            if (existMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            var alreadyExistMenuItem = await genericRepository.GetAsync(mi => mi.Name == dto.Name && mi.Id != id);
            if (alreadyExistMenuItem is not null)
                throw new HttpStatusCodeException(404, "Menu item with given name already exist");
            FileHelper.Remove(existMenuItem.ImageUrl);
            var menuItems = genericRepository.Update(dto.Adapt(existMenuItem));
            await genericRepository.SaveChangesAsync();
        }
        public async Task<ViewMenuItemDto> GetSetVersionAsync(Guid id)
        {
            var menuItems = await genericRepository.GetAll(includes: ["SetItems", "Topings", "SetItems.IncludedItem", "SetItems.ReplacementOptions", "SetItems.ReplacementOptions.ReplacementMenuItem"], expression: m => m.IsSet).ToListAsync();

            var menuItem = menuItems.FirstOrDefault(m => m.SetItems.FirstOrDefault(si => si.IncludedItemId == id && si.IsMain) is not null);

            if (menuItem is null)
                throw new HttpStatusCodeException(404, "No set found for given item");

            return menuItem.Adapt<ViewMenuItemDto>();
        }
    }
}
