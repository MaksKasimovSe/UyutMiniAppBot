using System.ComponentModel.DataAnnotations;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class CreateBasketDto
    {
        [Required]
        public ICollection<CreateMenuItemBasketDto> MenuItemsBaskets { get; set; }
    }
}
