using Microsoft.AspNetCore.Mvc;
using RentApi.Application.DTOs;
using RentApi.Application.Services.Interfaces;

namespace RentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
  private readonly ICustomerService _service;

  public CustomersController(ICustomerService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 20,
    [FromQuery] string? search = null)
  {
    var result = await _service.GetPagedCustomersAsync(search,page, size);
    return Ok(result);
  }

  [HttpPatch("{id}/toggle-passport")]
  public async Task<IActionResult> TogglePassport(int id)
  {
    var result = await _service.TogglePassportLocationAsync(id);
    return Ok(result);
  }



  [HttpPost]
  // [FromForm] ishlatamiz, chunki rasm va fayllar keladi
  public async Task<IActionResult> Create([FromForm] CreateCustomerDto dto)
  {
    try
    {
      var result = await _service.CreateCustomerAsync(dto);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message });
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id)
  {
    var result = await _service.GetByIdAsync(id);
    return Ok(result);
  }

  [HttpGet("search")]
  public async Task<IActionResult> Search([FromQuery] string q)
  {
    var result = await _service.SearchAsync(q);
    return Ok(result);
  }

  // Maxsus API: Passportni egasiga qaytarib berish
  [HttpPost("{id}/return-document")]
  public async Task<IActionResult> ReturnDocument(int id)
  {
    try
    {
      await _service.ReturnOriginalDocumentAsync(id);
      return Ok(new { message = "Hujjat muvaffaqiyatli qaytarildi" });
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}