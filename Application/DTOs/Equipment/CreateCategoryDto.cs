namespace RentApi.Application.DTOs;

// Kategoriya yaratish uchun
public class CreateCategoryDto
{
   public string? Details { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
  public string? Image { get; set; } = string.Empty;
  public int BrandId { get; set; }
}