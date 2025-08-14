using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Service.DTOs.Basket;

namespace UyutMiniApp.Service.Interfaces
{
    public interface IBasketService
    {
        Task CreateAsync(CreateBasketDto dto);
        Task RemoveItemAsync(Guid menuItemId);
        Task AddItemAsync(CreateMenuItemBasketDto dto);
        Task ChangeQuantity(CreateMenuItemBasketDto dto);
        Task<ViewBasketDto> GetBasket();
    }
}
