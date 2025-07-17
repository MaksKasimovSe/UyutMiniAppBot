using UyutMiniApp.Domain.Commons;

namespace UyutMiniApp.Domain.Entities
{
    public class SavedAddress : Auditable
    {
        public string MapLink { get; set; }
        public string Address { get; set; }

        public string Floor { get; set; }
        public string Entrance { get; set; }
    }
}
