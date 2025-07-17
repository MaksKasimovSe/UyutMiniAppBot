namespace UyutMiniApp.Service.DTOs.Couriers;

public class CreateCourierDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public long TelegramUserId { get; set; }
}