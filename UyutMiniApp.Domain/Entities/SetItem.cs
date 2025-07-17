using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class SetItem : Auditable
    {
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public Guid IncludedItemId { get; set; }
        public MenuItem IncludedItem { get; set; }
        public bool IsReplaceable { get; set; } = false;

        public ICollection<SetItemReplacementOption> ReplacementOptions { get; set; }
    }

}
