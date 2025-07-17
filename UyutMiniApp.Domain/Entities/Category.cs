using UyutMiniApp.Domain.Commons;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; }

        public CategoryFor CategoryFor { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }

}
