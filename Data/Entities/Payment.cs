namespace RentApi.Data.Entities;

public class Payment : BaseEntity
{
    // To'lov miqdori
    public decimal Amount { get; set; }

    // To'lov turi (masalan: "Oldindan to'lov", "To'liq to'lov", "Jarima")
    public string PaymentType { get; set; } = string.Empty;

    // To'lovni qabul qilgan xodim (ism yoki ID)
    public string ReceivedBy { get; set; } = string.Empty;

    // --- Tashqi bog'liqliklar (Foreign Keys) ---

    // Qaysi ijara buyurtmasi uchun to'lov qilindi
    public int RentalOrderId { get; set; }
    public virtual RentalOrder RentalOrder { get; set; } = null!;

    // To'lov usuli (Naqd, Karta va h.k.)
    public int PaymentMethodId { get; set; }
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    

    // Qaysi filial/ombor orqali to'lov qabul qilindi
    public int WarehouseId { get; set; }
    // public virtual Warehouse Warehouse { get; set; } = null!;
}