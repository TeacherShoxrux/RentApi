namespace RentApi.Data.Entities;

public class RentalOrderItem : BaseEntity
{
    // --- 1. Qaysi Buyurtmaga tegishli? ---
    public int RentalOrderId { get; set; }
    public virtual RentalOrder RentalOrder { get; set; } = null!;

    // --- 2. Qaysi Aniq Uskuna berildi? ---
    public int EquipmentItemId { get; set; }
    public virtual EquipmentItem EquipmentItem { get; set; } = null!;

    // --- 3. Moliyaviy va Holat ma'lumotlari ---
    
    // Uskuna berilgan paytdagi narx (Keyinchalik narx oshsa ham tarix o'zgarmasligi uchun)
    public decimal PriceAtMoment { get; set; }

    // Bu uskuna qaytarildimi? (Chunki bitta buyurtmadagi narsalar har xil vaqtda qaytishi mumkin)
    public bool IsReturned { get; set; } = false;
    
    // Qaytarilgan vaqt
    public DateTime? ReturnedAt { get; set; }
}