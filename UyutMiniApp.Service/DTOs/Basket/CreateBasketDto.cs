using System.ComponentModel.DataAnnotations;

namespace UyutMiniApp.Service.DTOs.Basket
{
    public class CreateBasketDto
    {
        [Required]
        public ICollection<CreateMenuItemBasketDto> MenuItemsBaskets { get; set; }
    }
}
