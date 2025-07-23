using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class UpdateOrderStatusDto
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
