using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace UyutMiniApp.Service.DTOs.Ingredients;

public class UpdateIngredientDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public IFormFile Image { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Gramm { get; set; }
    public decimal GrammLimit { get; set; }
    public int Quantity { get; set; }
    public int QuantityLimit { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }
}