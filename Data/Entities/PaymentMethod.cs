namespace RentApi.Data.Entities;

public class PaymentMethod : BaseEntity
{
    // To'lov usuli nomi (masalan: "Naqd pul", "Uzcard/Humo", "Bank o'tkazmasi")
    public string Name { get; set; } = string.Empty;

    // Tizim uchun qisqa kod (masalan: "CASH", "CARD", "TRANSFER")
    public string Code { get; set; } = string.Empty;

    // Ushbu to'lov usuli orqali amalga oshirilgan to'lovlar ro'yxati (ixtiyoriy)
    // public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}