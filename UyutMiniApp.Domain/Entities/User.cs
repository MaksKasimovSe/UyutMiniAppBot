using UyutMiniApp.Domain.Commons;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Domain.Entities
{
    public class User : Auditable
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public long TelegramUserId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Guid? SavedAddressId { get; set; }
        public SavedAddress SavedAddress { get; set; }
        public Role Role { get; set; }
    }
}
