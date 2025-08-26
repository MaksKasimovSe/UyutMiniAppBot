
namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class ViewBasketDto
    {
        public Guid Id { get; set; }
        public ICollection<ViewMenuItemBasketDto> MenuItemsBaskets { get; set; }
    }
}
