using AutoMapper;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Infrastructure.Repositories.Interfaces;

namespace Sales_Order_System_Backend.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<OrderReadDTO>> GetAllAsync()
    {
        var orders = await _repository.GetAllAsync();
        
        return _mapper.Map<List<OrderReadDTO>>(orders);
    }

    public async Task<OrderReadDTO?> GetByIdAsync(int id)
    {
        var order = await  _repository.GetByIdAsync(id);

        if (order == null)
            return null;
        
        return _mapper.Map<OrderReadDTO>(order);
    }

    public async Task<OrderReadDTO> CreateAsync(OrderCreateDTO dto)
    {
        var order = _mapper.Map<Order>(dto);
        
        

        foreach (var item in order.Items)
        {
            item.ExclAmount = item.Price * item.Quantity;
            item.TaxAmount = item.ExclAmount * item.TaxRate/100;
            item.InclAmount = item.ExclAmount + item.TaxAmount;
            
            order.TotalExcl += item.ExclAmount;
            order.TotalTax += item.TaxAmount;
            order.TotalIncl += item.InclAmount;
        }

        await _repository.AddAsync(order);

        return _mapper.Map<OrderReadDTO>(order);
    }
    
    public async Task<OrderReadDTO> UpdateAsync(int id, OrderCreateDTO dto)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            throw new KeyNotFoundException($"Order with ID {id} was not found.");

        order.ClientId = dto.ClientId;
        order.InvoiceNo = dto.InvoiceNo;
        order.InvoiceDate = dto.InvoiceDate;
        order.ReferenceNo = dto.ReferenceNo;

        order.Items.Clear();

        order.Items = dto.Items.Select(x => new OrderItem
        {
            ItemCode = x.ItemCode,
            Description = x.Description,
            Note = x.Note,
            Quantity = x.Quantity,
            Price = x.Price,
            TaxRate = x.TaxRate
        }).ToList();

        order.TotalExcl = 0;
        order.TotalTax = 0;
        order.TotalIncl = 0;

        foreach (var item in order.Items)
        {
            item.ExclAmount = item.Price * item.Quantity;
            item.TaxAmount = item.ExclAmount * item.TaxRate/100;
            item.InclAmount = item.ExclAmount + item.TaxAmount;

            order.TotalExcl += item.ExclAmount;
            order.TotalTax += item.TaxAmount;
            order.TotalIncl += item.InclAmount;
        }

        await _repository.UpdateAsync(order);

        return _mapper.Map<OrderReadDTO>(order);
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        var order = await _repository.GetByIdAsync(id);
        
        if (order == null)
            return null;

        return order;
    }
}