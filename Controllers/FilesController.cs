using Microsoft.AspNetCore.Mvc;
using RentApi.Application.DTOs;
using RentApi.Application.Services.Interfaceses;

namespace RentApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
  private readonly IFileStorageService _fileService;
  public FilesController(IFileStorageService fileService) => _fileService = fileService;

  [HttpPost("upload")]
  public async Task<IActionResult> Upload(IFormFile? file)
  {
    if (file == null || file.Length == 0) return BadRequest("Fayl bo'sh");
     string folder = "temp";
    // Faylni saqlaymiz va linkini olamiz
    var url = await _fileService.SaveFileAsync(file, folder);

    return Ok(ResponseDto<string>.Success(url) );
  }
}