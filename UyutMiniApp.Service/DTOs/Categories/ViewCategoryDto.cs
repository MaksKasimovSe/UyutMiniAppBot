using UyutMiniApp.Domain.Enums;
using UyutMiniApp.Service.DTOs.Ingredients;
using UyutMiniApp.Service.DTOs.MenuItems;

namespace UyutMiniApp.Service.DTOs.Category
{
    public class ViewCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public CategoryFor CategoryFor { get; set; }

        public ICollection<ViewMenuItemDto> MenuItems { get; set; }

        public ICollection<ViewIngredientDto> Ingredients { get; set; }
    }
}
