namespace RentApi.Application.DTOs.RentalOrder;

public class RentalItemRequestDto
{
  public int EquipmentId { get; set; } // Masalan: Bosch Drel (Katalogdagi ID)
  public int Quantity { get; set; } // Masalan: 2 dona
}

public class CreateRentalOrderDto
{
  // ... Mijoz, Sana, To'lov, Rasm (eski maydonlar) ...
  public int CustomerId { get; set; }
  public int PaymentMethodId { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime ExpectedReturnDate { get; set; }
  public decimal? AgreedTotalAmount { get; set; }
  public decimal PaidAmount { get; set; }
  public List<string>? ImageUrls { get; set; }

  // O'ZGARISH SHU YERDA: ID lar ro'yxati o'rniga Soni bilan keladi
  public List<RentalItemRequestDto> Items { get; set; } = new();
}