using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class DeliveryInfo : Auditable
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public string MapLink { get; set; }
        public string Address { get; set; }

        public string Floor { get; set; }
        public string Entrance { get; set; }

        public string Comment { get; set; }

        public decimal Fee { get; set; }
    }
}
