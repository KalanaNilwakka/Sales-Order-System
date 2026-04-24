using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Infrastructure.Data;

namespace Sales_Order_System_Backend.Application.Services;

public class ItemService : IItemService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ItemService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<ItemReadDTO>> GetAllAsync()
    {
        var items = await _dbContext.Items
            .ToListAsync();
        
        return _mapper.Map<List<ItemReadDTO>>(items);
    }

    public async Task<ItemReadDTO> GetByIdAsync(int id)
    {
        var item = await _dbContext.Items
            .FindAsync(id);

        if (item == null)
            throw new KeyNotFoundException($"Item with ID {id} was not found.");

        return _mapper.Map<ItemReadDTO>(item);
    }
}