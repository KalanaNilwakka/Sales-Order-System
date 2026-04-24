using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;
using Sales_Order_System_Backend.Domain.Entities;
using Sales_Order_System_Backend.Infrastructure.Data;

namespace Sales_Order_System_Backend.Application.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<OrderReadDTO>> GetAllAsync()
    {
        var orders = await _dbContext.Orders
            .ToListAsync();
        
        return _mapper.Map<List<OrderReadDTO>>(orders);
    }

    public async Task<OrderReadDTO> GetByIdAsync(int id)
    {
        var order = await  _dbContext.Orders
            .Include(o  => o.OrderItems)
            .Include(o => o.Client)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            throw new KeyNotFoundException($"Order with ID {id} was not found.");
        
        return _mapper.Map<OrderReadDTO>(order);
    }

    public async Task<OrderReadDTO> CreateAsync(OrderCreateDTO dto)
    {
        var order = _mapper.Map<Order>(dto);

        foreach (var item in order.OrderItems)
        {
            item.ExclAmount = item.Price * item.Quantity;
            item.TaxAmount = item.ExclAmount * item.TaxRate/100;
            item.InclAmount = item.ExclAmount + item.TaxAmount;
        }

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<OrderReadDTO>(order);
    }
    
    public async Task<OrderReadDTO> UpdateAsync(int id, OrderCreateDTO dto)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return null;

        order.ClientId = dto.ClientId;
        order.InvoiceNo = dto.InvoiceNo;
        order.InvoiceDate = dto.InvoiceDate;
        order.ReferenceNo = dto.ReferenceNo;

        _dbContext.OrderItems.RemoveRange(order.OrderItems);

        order.OrderItems = dto.Items.Select(x => new OrderItem
        {
            ItemId = x.ItemId,
            Description = x.Description,
            Note = x.Note,
            Quantity = x.Quantity,
            Price = x.Price,
            TaxRate = x.TaxRate
        }).ToList();

        order.TotalExcl = 0;
        order.TotalTax = 0;
        order.TotalIncl = 0;

        foreach (var item in order.OrderItems)
        {
            item.ExclAmount = item.Price * item.Quantity;
            item.TaxAmount = item.ExclAmount * item.TaxRate/100;
            item.InclAmount = item.ExclAmount + item.TaxAmount;

            order.TotalExcl += item.ExclAmount;
            order.TotalTax += item.TaxAmount;
            order.TotalIncl += item.InclAmount;
        }

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<OrderReadDTO>(order);
    }
}