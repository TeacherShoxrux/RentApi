namespace RentApi.Data.Entities;

public class OrderExtension : BaseEntity
{
  // Qaysi buyurtma uzaytirilyapti?
  public int RentalOrderId { get; set; }
  public virtual RentalOrder RentalOrder { get; set; } = null!;

  // Necha kun qo'shildi? (Masalan: 2 kun)
  public int DaysAdded { get; set; }

  // Bu uzaytirish uchun qancha qo'shimcha pul olindi?
  public decimal ExtensionPrice { get; set; }

  // Qachon boshlanadi bu uzaytirish? (Eski tugash sanasi)
  public DateTime StartDate { get; set; }

  // Yangi tugash sanasi
  public DateTime EndDate { get; set; }

  // Kim uzaytirib berdi? (Admin)
  public int AdminId { get; set; }
  // public virtual Admin Admin { get; set; }
}