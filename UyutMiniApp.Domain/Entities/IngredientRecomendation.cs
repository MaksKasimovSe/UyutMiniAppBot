namespace UyutMiniApp.Domain.Entities
{
    public class IngredientRecommendation
    {
        public Guid Id { get; set; }

        public Guid SourceIngredientId { get; set; }
        public Ingredient SourceIngredient { get; set; }

        public Guid RecommendedIngredientId { get; set; }
        public Ingredient RecommendedIngredient { get; set; }

        public double Score { get; set; }
    }
}