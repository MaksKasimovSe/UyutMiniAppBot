using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class FixedRecomendation : Auditable
    {
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}