using Microsoft.AspNetCore.Mvc;
using RentApi.Application.DTOs.RentalOrder;
using RentApi.Application.Services.Interfaceses;

namespace RentApi.API.Controllers;

[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
  private readonly IRentalService _service;

  public RentalsController(IRentalService service) => _service = service;

  [HttpPost]
  public async Task<IActionResult> CreateOrder([FromBody] CreateRentalOrderDto dto)
  {
    // Model validatsiyasi (DataAnnotaions bo'lsa)
    if (!ModelState.IsValid) return BadRequest(ModelState);

    var result = await _service.CreateRentalOrderAsync(dto);
    return Ok(result);
  }
  [HttpGet]
  public async Task<IActionResult> GetAll([FromQuery] RentalFilterDto filter)
  {
    var result = await _service.GetPagedOrdersAsync(filter);
    return Ok(result);
  }
}