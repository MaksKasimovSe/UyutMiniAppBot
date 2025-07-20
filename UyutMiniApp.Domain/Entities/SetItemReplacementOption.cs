using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class SetItemReplacementOption : Auditable
    {
        public Guid SetItemId { get; set; }
        public SetItem SetItem { get; set; }
        public Guid ReplacementMenuItemId { get; set; }
        public MenuItem ReplacementMenuItem { get; set; }
    }
}
