using Microsoft.AspNetCore.Mvc;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Application.Interfaces;

namespace Sales_Order_System_Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IReportService _reportService;
    
    public OrderController(IOrderService service, IReportService reportService)
    {
        _service = service;
        _reportService = reportService;
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

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderReadDTO>> Update(int id, OrderCreateDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }
    
    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetInvoicePdf(long id)
    {
        var order = await _service.GetOrderByIdAsync(id);

        if (order == null)
            return NotFound();

        var pdfBytes = _reportService.GenerateOrderInvoice(order);

        return File(pdfBytes, "application/pdf", $"Invoice_{order.InvoiceNo}.pdf");
    }
}