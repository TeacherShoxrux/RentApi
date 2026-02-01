namespace RentApi.Data.Entities;

public class Phone : BaseEntity
{
    // Raqam kimga tegishli ekanligi (masalan: "O'zi", "Akasi", "Ish joyi")
    public string Name { get; set; } = string.Empty;

    // Telefon raqami
    public string PhoneNumber { get; set; } = string.Empty;

    // --- Tashqi bog'liqlik ---
    
    // Raqam qaysi mijozga tegishli
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } = null!;
    // Navigatsiya property (ixtiyoriy, Customer yaratilgach ishlatish mumkin)
    // public virtual Customer Customer { get; set; } = null!;
}