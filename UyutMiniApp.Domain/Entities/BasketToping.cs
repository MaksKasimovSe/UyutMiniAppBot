using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class BasketToping : Auditable
    {
        public Guid MenuItemBasketId { get; set; }
        public MenuItemBasket MenuItemBasket { get; set; }
        public Guid TopingId { get; set; }
        public Toping Toping { get; set; }
    }
}
