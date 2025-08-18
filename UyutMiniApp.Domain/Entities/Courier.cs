using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class Courier : Auditable
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public long TelegramUserId { get; set; }
        public bool IsWorking { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
        public ICollection<Order> Orders { get; set; }
    }
}
