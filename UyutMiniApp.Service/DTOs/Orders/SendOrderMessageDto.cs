using System.ComponentModel.DataAnnotations;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class SendOrderMessageDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
