using UyutMiniApp.Service.DTOs.SetReplacementSelections;

namespace UyutMiniApp.Service.DTOs.OrderItems;
public class ViewOrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public ViewMenuForOrderDto MenuItem { get; set; }
    public Guid? CustomMealId { get; set; }
    public decimal Price { get; set; }
    public string OrderUrl { get; set; }
    public int Quantity { get; set; } = 1;
    public ICollection<ViewSetReplacementSelectionDto> SetReplacements { get; set; }
}

public class ViewMenuForOrderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsSet { get; set; }
}
