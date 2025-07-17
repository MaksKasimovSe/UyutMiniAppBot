namespace UyutMiniApp.Service.DTOs.SetReplacementSelections;
public class CreateSetReplacementSelectionDto
{
    public Guid OrderItemId { get; set; }
    public Guid OriginalSetItemId { get; set; }
    public Guid ReplacementMenuItemId { get; set; }
}
