namespace RentApi.Application.DTOs.RentalOrder;

public class RentalOrderListDto
{
  public int Id { get; set; }

  // Mijoz ma'lumotlari (Rasm, Ism, Tel)
  public int CustomerId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerPhone { get; set; } = string.Empty;
  public string? CustomerImage { get; set; } // Agar mijoz rasmi bo'lsa
  public string BookingDate { get; set; } = string.Empty; // "24.01.2026"
  public string TimeRange { get; set; } = string.Empty; // "10:00 - 20:00"
  public string Status { get; set; } = string.Empty;
  public decimal TotalAmount { get; set; }
  public decimal PaidAmount { get; set; }
  public decimal DebtAmount { get; set; }
}