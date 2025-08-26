using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class Toping : Auditable
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public string ImageUrl { get; set; }
    }
}
