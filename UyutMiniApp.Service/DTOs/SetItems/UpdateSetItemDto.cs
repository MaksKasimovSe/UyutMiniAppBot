using System.Text.Json.Serialization;
using UyutMiniApp.Service.DTOs.SetItemReplacementOptions;

namespace UyutMiniApp.Service.DTOs.SetItems;

public class UpdateSetItemDto
{
    public Guid IncludedItemId { get; set; }
    public bool IsReplaceable { get; set; }

    public ICollection<CreateSetItemReplacementOptionDto> ReplacementOptions { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
