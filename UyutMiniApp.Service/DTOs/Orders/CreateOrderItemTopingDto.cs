using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class CreateOrderItemTopingDto
    {
        public Guid MenuItemId { get; set; }
        public Guid TopingId { get; set; }
    }
}
