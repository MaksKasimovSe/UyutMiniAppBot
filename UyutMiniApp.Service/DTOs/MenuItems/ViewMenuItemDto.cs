using UyutMiniApp.Service.DTOs.SetItems;

namespace UyutMiniApp.Service.DTOs.MenuItems;
public class ViewMenuItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsSet { get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<ViewSetItemDto> SetItems { get; set; }
}