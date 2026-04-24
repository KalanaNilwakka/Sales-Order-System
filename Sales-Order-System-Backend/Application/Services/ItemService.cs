using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Infrastructure.Data;
using Sales_Order_System_Backend.Infrastructure.Repositories.Implementations;

namespace Sales_Order_System_Backend.Application.Services;

public class ItemService : IItemService
{
    private readonly ItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemService(ItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<ItemReadDTO>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        
        return _mapper.Map<List<ItemReadDTO>>(items);
    }

    public async Task<ItemReadDTO> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            throw new KeyNotFoundException($"Item with ID {id} was not found.");

        return _mapper.Map<ItemReadDTO>(item);
    }
}