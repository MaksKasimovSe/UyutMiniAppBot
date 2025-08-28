using System.ComponentModel.DataAnnotations;

namespace UyutMiniApp.Service.DTOs.SetReplacementSelections;
public class CreateSetReplacementSelectionDto
{
    public Guid OrderItemId { get; set; }
    [Required]
    public Guid OriginalSetItemId { get; set; }
    [Required]
    public Guid ReplacementMenuItemId { get; set; }
}
