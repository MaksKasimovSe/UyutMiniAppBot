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
        Task ChangeProcess(Guid id, OrderProcess orderProcess);
        Task<IEnumerable<ViewOrderDto>> GetTodaysOrders();
        Task SetPaymentMethod(Guid orderId, PaymentMethod paymentMethod);
    }
}
