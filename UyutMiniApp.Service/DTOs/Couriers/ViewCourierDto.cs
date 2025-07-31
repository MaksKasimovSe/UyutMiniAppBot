using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Orders;

namespace UyutMiniApp.Service.DTOs.Couriers;
public class ViewCourierDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public int NumberOfOrders { get; set; }
}