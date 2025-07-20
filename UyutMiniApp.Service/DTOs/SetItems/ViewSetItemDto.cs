using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

namespace UyutMiniApp.Service.DTOs.SetItems;
public class ViewSetItemDto
{
    public Guid Id { get; set; }
    public Guid MenuItemId { get; set; }
    public Guid IncludedItemId { get; set; }
    public ViewMenuItemDto IncludedItem { get; set; }
    public bool IsReplaceable { get; set; }

    public ICollection<ViewSetItemReplacementOptionDto> ReplacementOptions { get; set; }
}