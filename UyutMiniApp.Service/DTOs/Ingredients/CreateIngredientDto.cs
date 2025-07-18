using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UyutMiniApp.Service.DTOs.Ingredients;

public class CreateIngredientDto
{
    public string Name { get; set; }
    [BindNever]
    public string ImageUrl { get; set; }
    public IFormFile Image { get; set; }
    public decimal Price { get; set; }
    public decimal Gramm { get; set; }
    public decimal GrammLimit { get; set; }
    public int Quantity { get; set; }
    public int QuantityLimit { get; set; }

    public Guid CategoryId { get; set; }
}