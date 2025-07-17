using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.DTOs.SetItems;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class MenuItemService(IGenericRepository<MenuItem> genericRepository) : IMenuItemService
    {
        public async Task CreateAsync(CreateMenuItemDto dto)
        {
            if (dto.IsSet)
                if (dto.SetItems is null || dto.SetItems.Count <= 1)
                    throw new HttpStatusCodeException(400, "Set items should have at least 2 elements");

            await genericRepository.CreateAsync(dto.Adapt<MenuItem>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var isDeleted = await genericRepository.DeleteAsync(c => c.Id == id);
            if (!isDeleted)
                throw new HttpStatusCodeException(404,"Category not found");

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

            var alreadyExistMenuItem = await genericRepository.GetAsync(mi => mi.Name == dto.Name);
            if (alreadyExistMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item with given name already exist");

            var menuItems = genericRepository.Update(dto.Adapt(existMenuItem));
            await genericRepository.SaveChangesAsync();
        }
    }
}
