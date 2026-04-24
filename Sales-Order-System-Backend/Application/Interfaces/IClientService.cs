using Sales_Order_System_Backend.API.DTOs;

namespace Sales_Order_System_Backend.Application.Interfaces;

public interface IClientService
{
    Task<List<ClientReadDTO>> GetAllAsync();

    Task<ClientReadDTO> GetByIdAsync(int id);
}