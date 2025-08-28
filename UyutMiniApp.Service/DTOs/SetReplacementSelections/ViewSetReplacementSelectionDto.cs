using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

namespace UyutMiniApp.Service.DTOs.SetReplacementSelections;

public class ViewSetReplacementSelectionDto
{
    public Guid Id { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid OriginalSetItemId { get; set; }
    public Guid ReplacementMenuItemId { get; set; }
    public ViewMenuItemDto ReplacementMenuItem { get; set; }
    public ICollection<ViewSetItemReplacementOptionDto> ReplacementOptions { get; set; }
}
