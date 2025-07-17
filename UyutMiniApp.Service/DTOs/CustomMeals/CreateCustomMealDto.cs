using UyutMiniApp.Service.DTOs.CustomMealIngredients;

namespace UyutMiniApp.Service.DTOs.CustomMeals;

public class CreateCustomMealDto
{
    public string Name { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<CreateCustomMealIngredientDto> Ingredients { get; set; }
}