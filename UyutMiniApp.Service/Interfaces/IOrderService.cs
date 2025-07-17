using UyutMiniApp.Service.DTOs.Orders;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IOrderService
    {
        Task<ViewOrderDto> CreateAsync(CreateOrderDto dto);
        Task<ViewOrderDto> GetAsync(Guid id);
    }
}
