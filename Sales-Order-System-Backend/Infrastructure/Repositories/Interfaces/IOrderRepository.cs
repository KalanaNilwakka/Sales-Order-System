

using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}