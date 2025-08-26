using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class ViewMenuItemBasketDto
    {
        public Guid MenuItemId { get; set; }
        public ViewMenuItemDto MenuItem { get; set; }
        public int Quantity { get; set; }

        public ICollection<ViewBasketTopingDto> Topings { get; set; }
        public ICollection<ViewBasketReplacementSelectionDto> ReplacementSelection { get; set; }
        
    }
}
