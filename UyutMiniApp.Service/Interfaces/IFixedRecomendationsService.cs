using UyutMiniApp.Service.DTOs.FixedRecomendations;
using UyutMiniApp.Service.DTOs.IFixedRecomendations;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IFixedRecomendationsService
    {
        Task AddAsync(CreateFixedRecomendationDto dto);
        Task UpdateAsync(Guid id, CreateFixedRecomendationDto dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<IEnumerable<ViewFixedRecomendationDto>>> GetAllAsync();
    }
}
