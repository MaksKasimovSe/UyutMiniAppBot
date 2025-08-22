using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Service.DTOs.Topings
{
    public class CreateTopingDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid MenuItemId { get; set; }
    }
}
