using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Couriers;
using UyutMiniApp.Service.DTOs.DeliveryInfos;
using UyutMiniApp.Service.DTOs.OrderItems;
using UyutMiniApp.Service.DTOs.SetReplacementSelections;
using UyutMiniApp.Service.DTOs.Users;

namespace UyutMiniApp.Service.DTOs.Orders;

public class ViewOrderDto
{
    public Guid Id { get; set; }
    public ViewUserDto User { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderType Type { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public ICollection<ViewOrderItemDto> Items { get; set; }
    public ICollection<ViewSetReplacementSelectionDto> SetReplacements { get; set; }

    public ViewCourierDto Courier { get; set; }
    public ViewDeliveryInfoDto DeliveryInfo { get; set; }
}