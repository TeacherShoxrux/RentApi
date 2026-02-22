namespace RentApi.Application.DTOs.Equipment;

public class BrandDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? ImageUrl { get; set; }
}

public class CategoryDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? ImageUrl { get; set; }
  public string? Details { get; set; }
}