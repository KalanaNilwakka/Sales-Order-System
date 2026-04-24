using Microsoft.AspNetCore.Mvc;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;

namespace Sales_Order_System_Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;

    public ItemController(IItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemReadDTO>>> GetAllAsync()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{itemCode}")]
    public async Task<ActionResult<ItemReadDTO>> GetByIdAsync(string itemCode)
    {
        var item = await _service.GetByIdAsync(itemCode);
        if (item == null)
            return NotFound();
        return Ok(item);
    }
}