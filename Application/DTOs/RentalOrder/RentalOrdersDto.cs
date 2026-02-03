namespace RentApi.Application.DTOs.RentalOrder;

public class RentalOrderDto
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RentalDate { get; set; }
    public string Status { get; set; } // Ochiq yoki Yopiq
}