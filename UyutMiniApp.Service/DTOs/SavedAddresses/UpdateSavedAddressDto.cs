using System.Text.Json.Serialization;

namespace UyutMiniApp.Service.DTOs.SavedAddresses
{
    public class UpdateSavedAddressDto
    {
        public string MapLink { get; set; }
        public string Address { get; set; }

        public string Floor { get; set; }
        public string Entrance { get; set; }
        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
    }
}
