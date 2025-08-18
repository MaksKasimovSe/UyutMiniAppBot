using System.ComponentModel.DataAnnotations;

namespace UyutMiniApp.Service.DTOs.Basket
{
    public class CreateMenuItemBasketDto
    {
        [Required]
        public Guid MenuItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}