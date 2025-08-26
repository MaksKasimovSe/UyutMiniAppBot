using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.DeliveryInfos;
using UyutMiniApp.Service.DTOs.OrderItems;
using UyutMiniApp.Service.DTOs.SetReplacementSelections;

namespace UyutMiniApp.Service.DTOs.Orders;
public class CreateOrderDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public OrderType Type { get; set; }
    [JsonIgnore]
    public decimal TotalAmount { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    [Required]
    public ICollection<CreateOrderItemDto> Items { get; set; }
    public ICollection<CreateOrderItemTopingDto> SelectedTopings { get; set; }
    public CreateDeliveryInfoDto DeliveryInfo { get; set; }
}
