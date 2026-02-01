namespace RentApi.Data.Entities;

public class EquipmentItem : BaseEntity
{
    // Har bir dona uskunaning takrorlanmas seriya raqami
    public string SerialNumber { get; set; } = string.Empty;

    // Hozirgi holati: "Available", "Rented", "Repairing", "Lost"
    public EEquipmentItemStatus Status { get; set; }= EEquipmentItemStatus.Available;

    // Necha marta ijaraga berilganligi (eskirishini kuzatish uchun)
    public int UsageCount { get; set; }

    // Qaysi turdagi uskunaga tegishli ekanligi
    public int EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
    public string? Condition { get; set; }
    // Qaysi omborda turganligi
    public int WareHouseId { get; set; }
    public virtual WareHouse WareHouse { get; set; } = null!;
    public virtual ICollection<RentalOrderItem> RentalHistory { get; set; } = new List<RentalOrderItem>();
}