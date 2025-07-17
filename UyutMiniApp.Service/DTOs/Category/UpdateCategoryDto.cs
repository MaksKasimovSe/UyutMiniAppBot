using System.Text.Json.Serialization;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }

        public CategoryFor CategoryFor { get; set; }
        [JsonIgnore]
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
