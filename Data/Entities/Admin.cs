using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public AdminRole Role { get; set; } // Masalan: "SuperAdmin", "Manager"
    public string? Permission { get; set; }      // Ruxsatlar (JSON yoki string formatda)
    [Required]
    [StringLength(64)] // C# validatsiyasi uchun
    [Column(TypeName = "char(64)")] // Bazada (SQL) aniq turini belgilash uchun
    public string SecurityCode { get; set; } = string.Empty;  // Ikki bosqichli autentifikatsiya yoki maxsus kod
    // --- Tashqi bog'liqlik ---
    
    // Admin qaysi omborga biriktirilgan (agar bo'lsa)
    public int? WarehouseId { get; set; }
    public virtual WareHouse? WareHouse { get; set; }
    // public virtual Warehouse? Warehouse { get; set; }
}