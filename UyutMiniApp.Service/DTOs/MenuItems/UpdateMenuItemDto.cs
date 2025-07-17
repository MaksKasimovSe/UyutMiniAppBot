using System.Text.Json.Serialization;
using UyutMiniApp.Service.DTOs.SetItems;

namespace UyutMiniApp.Service.DTOs.MenuItems;
public class UpdateMenuItemDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsSet { get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<UpdateSetItemDto> SetItems { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}
