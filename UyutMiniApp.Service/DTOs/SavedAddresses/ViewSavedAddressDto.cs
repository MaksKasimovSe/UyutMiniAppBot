namespace UyutMiniApp.Service.DTOs.SavedAddresses
{
    public class ViewSavedAddressDto
    {
        public Guid Id { get; set; }
        public string MapLink { get; set; }
        public string Address { get; set; }

        public string Floor { get; set; }
        public string Entrance { get; set; }
    }
}
