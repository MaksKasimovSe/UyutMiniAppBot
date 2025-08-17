using System.ComponentModel.DataAnnotations;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class SendProcessMessageDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public OrderProcess OrderProcess { get; set; }
    }
}
