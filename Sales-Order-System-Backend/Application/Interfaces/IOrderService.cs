using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Application.Interfaces;

public interface IOrderService
{
    Task<List<OrderReadDTO>> GetAllAsync();

    Task<OrderReadDTO?> GetByIdAsync(int id);

    Task<OrderReadDTO> CreateAsync(OrderCreateDTO dto);

    Task<OrderReadDTO> UpdateAsync(int id, OrderCreateDTO dto);

    Task<Order?> GetOrderByIdAsync(long id);
}