using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class ClientPaidDto
    {
        public Guid OrderId { get; set; }
        public bool Pay { get; set; }
    }
}
