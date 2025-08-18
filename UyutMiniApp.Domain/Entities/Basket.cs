using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class Basket : Auditable
    {
        public ICollection<MenuItemBasket> MenuItemsBaskets { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
