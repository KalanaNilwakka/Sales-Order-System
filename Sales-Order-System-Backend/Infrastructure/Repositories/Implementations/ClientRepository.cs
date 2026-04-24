using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Infrastructure.Data;
using Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Implementations;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Client>> GetAllAsync()
    {
        return await _dbContext.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id);
    }
}