using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class MenuItemBasket : Auditable
    {
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public ICollection<BasketToping> Topings { get; set; }
        public ICollection<BasketReplacementSelection> ReplacementSelection { get; set; }
        public int Quantity { get; set; }
    }
}
