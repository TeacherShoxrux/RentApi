namespace RentApi.Data.Entities;

public class RentPhoto : BaseEntity
{
    // Faylning saqlangan manzili (URL yoki serverdagi yo'li)
    public string FilePath { get; set; } = string.Empty;

    // Qaysi ijara shartnomasiga tegishli ekanligi
    public int? RentalOrderId { get; set; }
    
    // Navigatsiya property
    public virtual RentalOrder? RentalOrder { get; set; }
}