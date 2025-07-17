using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class User : Auditable
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public long TelegramUserId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public SavedAddress SavedAddress { get; set; }
    }
}
