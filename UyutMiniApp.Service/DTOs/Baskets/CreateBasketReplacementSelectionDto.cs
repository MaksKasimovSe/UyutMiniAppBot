using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Baskets
{
    public class CreateBasketReplacementSelectionDto
    {
        [Required]
        public Guid OriginalSetItemId { get; set; }
        [Required]
        public Guid ReplacementMenuItemId { get; set; }
    }
}
