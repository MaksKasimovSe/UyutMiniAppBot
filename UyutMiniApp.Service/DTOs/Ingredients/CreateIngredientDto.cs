namespace UyutMiniApp.Service.DTOs.Ingredients;

public class CreateIngredientDto
{
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}