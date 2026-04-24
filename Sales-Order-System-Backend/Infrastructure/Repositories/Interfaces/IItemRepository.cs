using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();
    Task<Item?> GetByIdAsync(int id);
}