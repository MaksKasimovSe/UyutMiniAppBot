using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.DTOs.SetItems;

namespace UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

public class ViewSetItemReplacementOptionDto
{
    public Guid Id { get; set; }
    public Guid SetItemId { get; set; }
    public ViewSetItemDto SetItem { get; set; }
    public Guid ReplacementMenuItemId { get; set; }
    public ViewMenuItemDto ReplacementMenuItem { get; set; }
}