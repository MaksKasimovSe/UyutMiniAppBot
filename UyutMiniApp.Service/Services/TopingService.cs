using Mapster;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Topings;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class TopingService(IGenericRepository<Toping> genericRepository, IGenericRepository<MenuItem> menuItemRepository) : ITopingService
    {
        public async Task CreateAsync(CreateTopingDto dto)
        {
            var existMenuItem = await menuItemRepository.GetAsync(m => m.Id == dto.MenuItemId);
            if (existMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            await genericRepository.CreateAsync(dto.Adapt<Toping>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existTopic = await genericRepository.GetAsync(t => t.Id == id);
            if (existTopic is null)
                throw new HttpStatusCodeException(404, "Toping not found");

            await genericRepository.DeleteAsync(t => t.Id == id);
            await genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewTopingDto>> GetAllAsync()
        {
            var topings = await genericRepository.GetAll(false).ToListAsync();

            return topings.Adapt<IEnumerable<ViewTopingDto>>();
        }

        public async Task UpdateAsync(Guid id, CreateTopingDto dto)
        {
            var existMenuItem = await menuItemRepository.GetAsync(m => m.Id == dto.MenuItemId);
            if (existMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            var existTopic = await genericRepository.GetAsync(t => t.Id == id);
            if (existTopic is null)
                throw new HttpStatusCodeException(404, "Toping not found");

            genericRepository.Update(dto.Adapt(existTopic));
            await genericRepository.SaveChangesAsync();
        }
    }
}
