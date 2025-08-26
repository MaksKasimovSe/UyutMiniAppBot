using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class BasketReplacementSelection : Auditable
    {
        public Guid MenuItemBasketId { get; set; }
        public MenuItemBasket MenuItemBasket { get; set; }
        public Guid OriginalSetItemId { get; set; }
        public SetItem OriginalSetItem { get; set; }

        public Guid ReplacementMenuItemId { get; set; }
        public MenuItem ReplacementMenuItem { get; set; }
    }
}
