namespace RentApi.Data.Entities;

public class EquipmentItem : BaseEntity
{
    // Har bir dona uskunaning takrorlanmas seriya raqami
    public string SerialNumber { get; set; } = string.Empty;

    // Hozirgi holati: "Available", "Rented", "Repairing", "Lost"
    public string Status { get; set; } = "Available";

    // Necha marta ijaraga berilganligi (eskirishini kuzatish uchun)
    public int UsageCount { get; set; }

    // Qaysi turdagi uskunaga tegishli ekanligi
    public int EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;

    // Qaysi omborda turganligi
    public int WarehouseId { get; set; }
    public virtual Warehouse Warehouse { get; set; } = null!;
}