using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UyutMiniApp.Domain.Enums;

namespace UyutMiniApp.Service.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }

        public CategoryFor CategoryFor { get; set; }
    }
}
