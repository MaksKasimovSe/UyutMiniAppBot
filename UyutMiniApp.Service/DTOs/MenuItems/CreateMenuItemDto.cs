using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using UyutMiniApp.Service.DTOs.SetItems;

namespace UyutMiniApp.Service.DTOs.MenuItems;
public class CreateMenuItemDto
{
    public string Name { get; set; }
    [IgnoreDataMember]
    public string ImageUrl { get; set; }
    public IFormFile Image { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsSet { get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<CreateSetItemDto> SetItems { get; set; }
}