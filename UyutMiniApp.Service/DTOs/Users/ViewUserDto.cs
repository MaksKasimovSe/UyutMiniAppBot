using UyutMiniApp.Domain.Entities;
namespace UyutMiniApp.Service.DTOs.Users;
public class ViewUserDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public long TelegramUserId { get; set; }
}