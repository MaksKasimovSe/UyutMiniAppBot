using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class CreateBasketReplacementSelectionDto
    {
        public Guid OriginalSetItemId { get; set; }

        public Guid ReplacementMenuItemId { get; set; }
    }
}
