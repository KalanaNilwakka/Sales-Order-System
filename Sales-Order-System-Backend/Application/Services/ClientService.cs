using AutoMapper;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

namespace Sales_Order_System_Backend.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IMapper _mapper;
    
    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _repository = clientRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ClientReadDTO>> GetAllAsync()
    {
        var clients = await _repository.GetAllAsync();
        return _mapper.Map<List<ClientReadDTO>>(clients);
    }

    public async Task<ClientReadDTO?> GetByIdAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);

        if (client == null)
            return null;
        
        return _mapper.Map<ClientReadDTO>(client);
    }
}