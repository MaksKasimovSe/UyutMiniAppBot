using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Baskets;
using UyutMiniApp.Service.DTOs.Topings;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class ViewBasketTopingDto
    {
        public ViewTopingDto Toping { get; set; }
    }
}
