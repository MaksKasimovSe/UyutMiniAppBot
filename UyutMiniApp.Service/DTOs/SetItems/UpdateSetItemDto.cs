using System.Text.Json.Serialization;

namespace UyutMiniApp.Service.DTOs.SetItems;

public class UpdateSetItemDto
{
    public Guid IncludedItemId { get; set; }
    public bool IsReplaceable { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
