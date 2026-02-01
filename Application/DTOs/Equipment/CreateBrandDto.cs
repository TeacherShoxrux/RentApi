namespace RentApi.Application.DTOs;

// Brend yaratish uchun
public class CreateBrandDto
{
  public string Name { get; set; } = string.Empty;
  public IFormFile? Image { get; set; }
}
