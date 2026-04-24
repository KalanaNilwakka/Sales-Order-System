using Microsoft.AspNetCore.Mvc;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;

namespace Sales_Order_System_Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    
    public OrderController(IOrderService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAllAsync()
    {
        var orders = await _service.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReadDTO>> GetByIdAsync(int id)
    {
        var order = await _service.GetByIdAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderReadDTO>> CreateAsync(OrderCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return Created(created.OrderId.ToString(), created);
    }

    // PUT: api/order/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrderCreateDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }
}