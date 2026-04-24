using Sales_Order_System_Backend.API.DTOs;

namespace Sales_Order_System_Backend.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderReadDTO>> GetAllAsync();

    Task<OrderReadDTO> GetByIdAsync(int id);

    Task<OrderReadDTO> CreateAsync(OrderCreateDTO dto);
}