
namespace UyutMiniApp.Service.DTOs.Basket
{
    public class ViewBasketDto
    {
        public Guid Id { get; set; }
        public ICollection<ViewMenuItemBasketDto> MenuItemsBaskets { get; set; }
    }
}
