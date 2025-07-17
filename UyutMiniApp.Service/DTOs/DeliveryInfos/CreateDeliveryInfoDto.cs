namespace UyutMiniApp.Service.DTOs.DeliveryInfos;
public class CreateDeliveryInfoDto
{
    public Guid OrderId { get; set; }
    public string Address { get; set; }
    public string Floor { get; set; }
    public string Comment { get; set; }
    public string Entrance { get; set; }
    public decimal Fee { get; set; }
}
