using UyutMiniApp.Domain.Commons;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Domain.Entities
{
    public class Order : Auditable
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public OrderType Type { get; set; } // Delivery or InCafe

        public DeliveryInfo DeliveryInfo { get; set; }

        public decimal TotalAmount { get; set; }

        public int OrderNumber { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public Guid? CourierId { get; set; }
        public Courier Courier { get; set; }
    }
}
