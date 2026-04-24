using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(long id);
}