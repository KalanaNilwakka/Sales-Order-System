using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Infrastructure.Data;
using Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

namespace Sales_Order_System_Backend.Infrastructure.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await _dbContext.Orders
            .Include(o => o.Client)
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}