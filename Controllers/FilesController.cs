using Microsoft.AspNetCore.Mvc;
using RentApi.Application.Services.Interfaceses;

namespace RentApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
  private readonly IFileStorageService _fileService;
  public FilesController(IFileStorageService fileService) => _fileService = fileService;

  [HttpPost("upload")]
  public async Task<IActionResult> Upload(IFormFile? file, string folder = "temp")
  {
    if (file == null) return BadRequest("Fayl yo'q");

    // Faylni saqlaymiz va linkini olamiz
    var url = await _fileService.SaveFileAsync(file, folder);

    // Faqat linkni qaytaramiz
    return Ok(new { link = url });
  }
}