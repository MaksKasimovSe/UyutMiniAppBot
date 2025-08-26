using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

public class ViewSetItemReplacementOptionDto
{
    public Guid Id { get; set; }
    public Guid SetItemId { get; set; }
    public int MarkUp { get; set; }
    public Guid ReplacementMenuItemId { get; set; }
    public ViewMenuItemDto ReplacementMenuItem { get; set; }
}