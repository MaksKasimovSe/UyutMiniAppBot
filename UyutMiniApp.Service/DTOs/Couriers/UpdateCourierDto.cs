using System.Text.Json.Serialization;

namespace UyutMiniApp.Service.DTOs.Couriers;
public class UpdateCourierDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}