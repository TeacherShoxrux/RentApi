namespace RentApi.Application.DTOs;

// Kategoriya yaratish uchun
public class CreateCategoryDto
{
  public string Name { get; set; } = string.Empty;
  public IFormFile? Image { get; set; }
}