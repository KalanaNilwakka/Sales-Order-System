using Microsoft.AspNetCore.Mvc;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;

namespace Sales_Order_System_Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;
    
    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientReadDTO>>> GetAllAsync()
    {
        var clients = await _service.GetAllAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientReadDTO?>> GetByIdAsync(int id)
    {
        var client = await _service.GetByIdAsync(id);
        if (client == null) 
            return NotFound();
        return Ok(client);
    }
}