namespace UyutMiniApp.Service.DTOs.OrderItems;
public class ViewOrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public ViewMenuForOrderDto MenuItem { get; set; }
    public Guid? CustomMealId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
}

public class ViewMenuForOrderDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
