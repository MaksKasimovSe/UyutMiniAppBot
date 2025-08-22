using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.MenuItems;
using UyutMiniApp.Service.DTOs.Topings;

namespace UyutMiniApp.Service.DTOs.Orders
{
    public class ViewOrderItemTopingDto
    {
        public Guid Id { get; set; }
        public ViewMenuItemDto MenuItem { get; set; }
        public ViewTopingDto Toping { get; set; }
    }
}
