namespace Application.DTOs.RentalOrder;

public class RentalOrderDetailDto
{
  public int OrderId { get; set; }
  public string CustomerName { get; set; }
  public DateTime StartDate { get; set; }
  public List<RentalItemDto> Items { get; set; }
  public decimal TotalAmount { get; set; }
  public decimal PaidAmount { get; set; }
  public decimal RemainingDebt => TotalAmount - PaidAmount;
}
public class RentalItemDto
{
  public int ItemId { get; set; }
  public string EquipmentName { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public bool IsReturned { get; set; }
}