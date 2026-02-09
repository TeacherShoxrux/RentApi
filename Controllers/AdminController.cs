using Application.DTOs.Employee;
using Application.Services.Interfaceses;
using Microsoft.AspNetCore.Mvc;

namespace RentApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _adminService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _adminService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AdminCreateDto dto)
    {
        var result = await _adminService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AdminCreateDto dto)
    {
        var result = await _adminService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var result = await _adminService.ToggleStatusAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _adminService.DeleteAsync(id));
}