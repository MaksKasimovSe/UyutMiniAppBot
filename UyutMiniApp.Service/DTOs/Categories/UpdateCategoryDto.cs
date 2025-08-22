using Microsoft.AspNetCore.Http;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public CategoryFor CategoryFor { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
