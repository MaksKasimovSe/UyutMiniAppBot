namespace UyutMiniApp.Service.DTOs.Topings
{
    public class ViewTopingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid MenuItemId { get; set; }
    }
}
