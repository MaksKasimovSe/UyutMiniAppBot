using UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

namespace UyutMiniApp.Service.DTOs.SetItems;
public class CreateSetItemDto
{
    public Guid IncludedItemId { get; set; }
    public bool IsReplaceable { get; set; }
    public ICollection<CreateSetItemReplacementOptionDto> ReplacementOptions { get; set; }
}