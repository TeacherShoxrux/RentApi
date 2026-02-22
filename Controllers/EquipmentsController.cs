using Microsoft.AspNetCore.Mvc;
using RentApi.Application.DTOs;
using RentApi.Application.DTOs.Equipment;
using RentApi.Application.Services.Interfaces;
using RentApi.Application.Services.Interfaceses;

namespace RentApi.Controllers;

[Route("api/[controller]")]
public class EquipmentsController : ControllerBase
{
  private readonly IEquipmentService _service;

  public EquipmentsController(IEquipmentService service) => _service = service;

  [HttpGet]
  public async Task<IActionResult> Get([FromQuery] EquipmentFilterDto filter)
  {
    var result = await _service.GetPagedEquipmentsAsync(filter);
    return Ok(result);
  }
  [HttpPost("brands")]
  public async Task<IActionResult> AddBrand(CreateBrandDto dto)
    => Ok(await _service.CreateBrandAsync(dto));

  [HttpPost("categories")]
  public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
    => Ok(await _service.CreateCategoryAsync(dto));

  [HttpPost]
  public async Task<IActionResult> AddEquipment([FromBody] CreateEquipmentDto dto)
    => Ok(await _service.CreateEquipmentAsync(dto));
 
 
  [HttpGet("brands")]
  public async Task<IActionResult> GetBrands()
  {
    var result = await _service.GetAllBrandsAsync();
    return Ok(result);
  }

  [HttpGet("brands/{brandId}/categories")]
  public async Task<IActionResult> GetCategoriesByBrand(int brandId)
  {
    var result = await _service.GetCategoriesByBrandAsync(brandId);
    return Ok(result);
  }
}