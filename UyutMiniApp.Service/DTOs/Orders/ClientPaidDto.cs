using System.ComponentModel.DataAnnotations;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class ClientPaidDto
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public bool Pay { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
