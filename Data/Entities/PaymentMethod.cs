namespace RentApi.Data.Entities;

public class PaymentMethod : BaseEntity
{
    // To'lov usuli nomi (masalan: "Naqd", "Humo/UzCard", "Click", "Bank o'tkazmasi")
    public string Name { get; set; } = string.Empty;

    // To'lov turi haqida qisqacha ma'lumot (masalan: "Terminal orqali qabul qilinadi")
    public string? Description { get; set; }

    // Bu to'lov usuli hozirda faolmi yoki yo'q?
    public bool IsActive { get; set; } = true;

    // To'lov usuli uchun ikonka (masalan: FontAwesome icon nomi yoki rasm URL)
    public string? Icon { get; set; }
}