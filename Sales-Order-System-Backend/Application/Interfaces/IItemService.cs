using Sales_Order_System_Backend.API.DTOs;

namespace Sales_Order_System_Backend.Application.Interfaces;

public interface IItemService
{
    Task<List<ItemReadDTO>> GetAllAsync();

    Task<ItemReadDTO?> GetByIdAsync(string code);
}