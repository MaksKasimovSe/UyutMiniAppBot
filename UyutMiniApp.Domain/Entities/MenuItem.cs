using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class MenuItem : Auditable
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsSet { get; set; } = false;

        public Guid CategoryId { get; set; }

        public ICollection<SetItem> SetItems { get; set; }
    }
}
