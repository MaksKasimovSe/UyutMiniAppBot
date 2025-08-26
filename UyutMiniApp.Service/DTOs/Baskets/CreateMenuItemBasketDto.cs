using System.ComponentModel.DataAnnotations;
using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class CreateMenuItemBasketDto
    {
        [Required]
        public Guid MenuItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public ICollection<CreateBasketTopingDto> Topings { get; set; }
        public ICollection<CreateBasketReplacementSelectionDto> ReplacementSelection { get; set; }
    }
}