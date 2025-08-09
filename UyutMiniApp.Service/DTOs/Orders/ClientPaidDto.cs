using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class ClientPaidDto
    {
        public Guid OrderId { get; set; }
        public bool Pay { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
