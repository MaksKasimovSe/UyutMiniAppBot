using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Basket
{
    public class CreateMenuItemBasketDto
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}