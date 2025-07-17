using System.Text.Json.Serialization;

public class UpdateUserDto
{
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}