using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class CustomMealIngredient : Auditable
    {
        public Guid CustomMealId { get; set; }
        public CustomMeal CustomMeal { get; set; }

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int Quantity { get; set; }
        public string Tag { get; set; }
        public bool BasicIngredient { get; set; } = false;
    }
}