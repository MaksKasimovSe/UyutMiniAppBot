using UyutMiniApp.Service.DTOs.CustomMeals;

namespace UyutMiniApp.Service.DTOs.OrderItems;
public class CreateOrderItemDto
{
    public Guid MenuItemId { get; set; }
    public CreateCustomMealDto CustomMeal { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}