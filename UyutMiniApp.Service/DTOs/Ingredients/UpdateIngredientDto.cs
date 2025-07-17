using System.Text.Json.Serialization;

namespace UyutMiniApp.Service.DTOs.Ingredients;

public class UpdateIngredientDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}