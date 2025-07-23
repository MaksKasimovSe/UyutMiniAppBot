using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Orders;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IOrderService
    {
        Task<ViewOrderDto> CreateAsync(CreateOrderDto dto);
        Task<ViewOrderDto> GetAsync(Guid id);
        Task<string> UpdateOrderReceipt(Guid id, string url);
        Task ChangeStatus(Guid id, OrderStatus status);
    }
}
