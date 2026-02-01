using Microsoft.AspNetCore.Mvc;
using RentApi.Application.DTOs.Payment;
using RentApi.Application.Services;

[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
  private readonly IPaymentService _service;
  public PaymentsController(IPaymentService service) => _service = service;

  [HttpPost("methods")]
  public async Task<IActionResult> AddMethod(CreatePaymentMethodDto dto)
    => Ok(await _service.CreatePaymentMethodAsync(dto));

  [HttpGet("methods/active")]
  public async Task<IActionResult> GetActive()
    => Ok(await _service.GetActiveMethodsAsync());

  [HttpPatch("methods/{id}/toggle")]
  public async Task<IActionResult> Toggle(int id)
    => Ok(await _service.ToggleStatusAsync(id));
}