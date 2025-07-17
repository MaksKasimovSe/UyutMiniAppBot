using Mapster;
using UyutMiniApp.Data.IRepositories;
using UyutMiniApp.Domain.Entities;
using UyutMiniApp.Service.DTOs.Orders;
using UyutMiniApp.Service.Exceptions;
using UyutMiniApp.Service.Interfaces;

namespace UyutMiniApp.Service.Services
{
    public class OrderService(IGenericRepository<Order> genericRepository, IGenericRepository<SavedAddress> addressRepository) : IOrderService
    {
        public async Task<ViewOrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = await genericRepository.CreateAsync(dto.Adapt<Order>());

            var address = new SavedAddress()
            {
                Address = dto.DeliveryInfo.Address,
                Floor = dto.DeliveryInfo.Floor,
                Entrance = dto.DeliveryInfo.Entrance
            };

            await addressRepository.CreateAsync(address);
            await genericRepository.SaveChangesAsync();
            await addressRepository.SaveChangesAsync();
            return order.Adapt<ViewOrderDto>();
        }

        public async Task<ViewOrderDto> GetAsync(Guid id)
        {
            var existOrder = await genericRepository.GetAsync(o => o.Id == id);

            if (existOrder is null)
                throw new HttpStatusCodeException(404, "order not found");

            return existOrder.Adapt<ViewOrderDto>();
        }
    }
}
