using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class Basket : Auditable
    {
        public ICollection<MenuItemBasket> MenuItemsBaskets { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
