namespace UyutMiniApp.Service.DTOs.Ingredients;
public class ViewIngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Gramm { get; set; }
    public decimal GrammLimit { get; set; }
    public int Quantity { get; set; }
    public int QuantityLimit { get; set; }
}