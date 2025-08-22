using Mapster;
using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.FixedRecomendations;
using UyutMiniApp.Service.DTOs.IFixedRecomendations;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class FixedRecommendationService(IGenericRepository<FixedRecomendation> genericRepository, IGenericRepository<MenuItem> menuItemGeneric) : IFixedRecomendationsService
    {
        public async Task AddAsync(CreateFixedRecomendationDto dto)
        {
            var existMenuItem = await menuItemGeneric.GetAsync(c => c.Id == dto.MenuItemId);
            if (existMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            await genericRepository.CreateAsync(dto.Adapt<FixedRecomendation>());
            await genericRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var existFixedRecomendation = await genericRepository.GetAsync(fr => fr.Id == id);
            if (existFixedRecomendation is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            await genericRepository.DeleteAsync(fr => fr.Id == id);
            await genericRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ViewFixedRecomendationDto>> GetAllAsync()
        {
            var fixedRecomnedations = await genericRepository.GetAll(false, includes: ["MenuItem"]).ToListAsync();

            return fixedRecomnedations.Adapt<List<ViewFixedRecomendationDto>>();
        }

        public async Task UpdateAsync(Guid id, CreateFixedRecomendationDto dto)
        {
            var existMenuItem = await menuItemGeneric.GetAsync(c => c.Id == dto.MenuItemId);
            if (existMenuItem is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            var existFixedRecomendation = await genericRepository.GetAsync(fr => fr.Id == id);
            if (existFixedRecomendation is null)
                throw new HttpStatusCodeException(404, "Menu item not found");

            genericRepository.Update(dto.Adapt(existFixedRecomendation));
            await genericRepository.SaveChangesAsync();
        }
    }
}
