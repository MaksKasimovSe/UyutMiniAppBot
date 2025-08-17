using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.DTOs.Basket
{
    public class ViewMenuItemBasketDto
    {
        public Guid MenuItemId { get; set; }
        public ViewMenuItemDto MenuItem { get; set; }
        public int Quantity { get; set; }
    }
}
