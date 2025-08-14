using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyutMiniApp.Service.DTOs.Basket
{
    public class ViewBasketDto
    {
        public Guid Id { get; set; }
        public ICollection<ViewMenuItemBasketDto> MenuItemsBaskets { get; set; }
    }
}
