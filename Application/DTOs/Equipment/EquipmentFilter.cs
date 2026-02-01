namespace RentApi.Application.DTOs;

public class EquipmentFilterDto
{
  public int? BrandId { get; set; }
  public int? CategoryId { get; set; }
  public string? SearchTerm { get; set; }
  public int Page { get; set; } = 1;
  public int Size { get; set; } = 10;
}
