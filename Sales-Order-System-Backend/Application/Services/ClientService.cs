using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Infrastructure.Data;

namespace Sales_Order_System_Backend.Application.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public ClientService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<ClientReadDTO>> GetAllAsync()
    {
        var clients = await _dbContext.Clients
            .ToListAsync();
        
        return _mapper.Map<List<ClientReadDTO>>(clients);
    }

    public async Task<ClientReadDTO> GetByIdAsync(int id)
    {
        var client = await _dbContext.Clients
            .FindAsync(id);
        
        if (client == null)
            throw new KeyNotFoundException($"Client with ID {id} was not found.");
        
        return _mapper.Map<ClientReadDTO>(client);
    }
}