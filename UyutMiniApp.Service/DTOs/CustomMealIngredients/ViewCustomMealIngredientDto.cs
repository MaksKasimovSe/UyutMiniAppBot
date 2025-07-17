using UyutMiniApp.Service.DTOs.Ingredients;

namespace UyutMiniApp.Service.DTOs.CustomMealIngredients;

public class ViewCustomMealIngredientDto
{
    public Guid Id { get; set; }
    public Guid CustomMealId { get; set; }
    public ViewIngredientDto Ingredient { get; set; }
    public int Quantity { get; set; }
}