using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Infrastructure.Data;
using Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Implementations;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _dbContext;

    public ItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Item>> GetAllAsync()
    {
        return await _dbContext.Items.ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
    }
}