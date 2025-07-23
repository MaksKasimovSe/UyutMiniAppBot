using System.Text.Json.Serialization;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.DeliveryInfos;
using UyutMiniApp.Service.DTOs.OrderItems;
using UyutMiniApp.Service.DTOs.SetReplacementSelections;

namespace UyutMiniApp.Service.DTOs.Orders;
public class CreateOrderDto
{
    public Guid UserId { get; set; }
    public OrderType Type { get; set; }
    [JsonIgnore]
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Guid? CourierId { get; set; }
    public ICollection<CreateOrderItemDto> Items { get; set; }
    public ICollection<CreateSetReplacementSelectionDto> SetReplacements { get; set; }
    public CreateDeliveryInfoDto DeliveryInfo { get; set; }
}
