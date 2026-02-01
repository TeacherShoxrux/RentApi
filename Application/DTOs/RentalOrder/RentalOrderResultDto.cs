namespace RentApi.Application.DTOs.RentalOrder;

public class RentalOrderResultDto
{
  public int OrderId { get; set; }
  public decimal StandardTotal { get; set; } // Aslida qancha bo'lishi kerak edi
  public decimal FinalTotal { get; set; } // Yakuniy narx (Chegirma bilan)
  public double TotalDays { get; set; } // Necha kunga olindi
  public string Message { get; set; } = string.Empty;
}