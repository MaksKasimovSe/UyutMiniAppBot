using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class SetReplacementSelection : Auditable
    {
        public Guid OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

        public Guid OriginalSetItemId { get; set; }
        public SetItem OriginalSetItem { get; set; }

        public Guid ReplacementMenuItemId { get; set; }
        public MenuItem ReplacementMenuItem { get; set; }
    }
}
