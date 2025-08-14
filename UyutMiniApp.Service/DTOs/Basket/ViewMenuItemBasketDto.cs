using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Entities;
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
