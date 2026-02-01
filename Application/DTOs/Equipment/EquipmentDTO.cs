namespace RentApi.Application.DTOs;

public class EquipmentListDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string BrandName { get; set; } = string.Empty;
  public string CategoryName { get; set; } = string.Empty;
  public decimal PricePerDay { get; set; }
  public string? ImageUrl { get; set; }
  public int AvailableCount { get; set; } // Omborlardagi bo'sh donalar soni
}