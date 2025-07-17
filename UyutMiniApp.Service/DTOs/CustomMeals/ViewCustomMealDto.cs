using UyutMiniApp.Service.DTOs.CustomMealIngredients;

namespace UyutMiniApp.Service.DTOs.CustomMeals;

public class ViewCustomMealDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal TotalPrice { get; set; }
    public ICollection<ViewCustomMealIngredientDto> Ingredients { get; set; }
}