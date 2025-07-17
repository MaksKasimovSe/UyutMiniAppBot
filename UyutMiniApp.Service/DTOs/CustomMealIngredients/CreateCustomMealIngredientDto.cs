using UyutMiniApp.Service.DTOs.Ingredients;

namespace UyutMiniApp.Service.DTOs.CustomMealIngredients;
public class CreateCustomMealIngredientDto
{
    public int Quantity { get; set; }
    public CreateIngredientDto Ingredient { get; set; }
}
