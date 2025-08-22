using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class OrderItemToping : Auditable
    {
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public Guid TopingId { get; set; }
        public Toping Toping { get; set; }
    }
}
