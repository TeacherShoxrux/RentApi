using RentApi.Data.Entities;

namespace RentApi.Application.DTOs.RentalOrder;
public class RentalFilterDto
{
  public string? Search { get; set; } // Ism, Telefon yoki ID bo'yicha
  public EOrderStatus? Status { get; set; } // Tasdiqlangan, Kutilmoqda...

  // "Kunlik", "Haftalik" filtrlari uchun sana oralig'i
  public DateTime? FromDate { get; set; }
  public DateTime? ToDate { get; set; }

  public int Page { get; set; } = 1;
  public int Size { get; set; } = 10;
}