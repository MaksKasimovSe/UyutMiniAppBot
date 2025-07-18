using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.CustomMeals;
using UyutMiniApp.Service.DTOs.DeliveryInfos;
using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.DTOs.OrderItems;
public class CreateOrderItemDto
{
    public Guid MenuItem { get; set; }
    public CreateCustomMealDto CustomMeal { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}