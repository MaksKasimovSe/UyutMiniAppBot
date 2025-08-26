using Microsoft.AspNetCore.Http;
using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Topings
{
    public class CreateTopingDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile FormFile { get; set; }
        public string ImageUrl { get; set; }
        public Guid MenuItemId { get; set; }
    }
}
