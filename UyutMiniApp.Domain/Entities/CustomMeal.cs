using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class CustomMeal : Auditable
    {
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CustomMealIngredient> Ingredients { get; set; }
    }
}
