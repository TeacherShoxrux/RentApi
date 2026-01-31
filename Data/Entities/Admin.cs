namespace RentApi.Data.Entities;

public class Admin : BaseEntity
{
    // Shaxsiy ma'lumotlar
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty; // XML: Last
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public string? Details { get; set; }

    // Xavfsizlik va kirish huquqlari
    public string Role { get; set; } = "Staff"; // Masalan: "SuperAdmin", "Manager"
    public string? Permission { get; set; }      // Ruxsatlar (JSON yoki string formatda)
    public string? SecurityCode { get; set; }    // Ikki bosqichli autentifikatsiya yoki maxsus kod

    // --- Tashqi bog'liqlik ---
    
    // Admin qaysi omborga biriktirilgan (agar bo'lsa)
    public int? WarehouseId { get; set; }
    // public virtual Warehouse? Warehouse { get; set; }
}