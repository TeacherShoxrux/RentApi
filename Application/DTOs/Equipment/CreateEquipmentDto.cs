using System.ComponentModel.DataAnnotations;

namespace RentApi.Application.DTOs.Equipment;

public class CreateEquipmentDto
{
  [Required(ErrorMessage = "Mahsulot nomi kiritilishi shart")]
  public string Name { get; set; } = string.Empty;

  [Required(ErrorMessage = "Brendni tanlang")]
  public int BrandId { get; set; }

  [Required(ErrorMessage = "Kategoriyani tanlang")]
  public int CategoryId { get; set; }

  public int Quantity { get; set; }=1; // Mahsulot soni
  public string? Model { get; set; } // UI dagi Model yoki SKU uchun

  public string? Description { get; set; } // Mahsulot haqida batafsil

  [Range(0, double.MaxValue, ErrorMessage = "Narx noto'g'ri kiritildi")]
  public decimal PricePerDay { get; set; } // Bir kunlik ijara narxi

  [Range(0, double.MaxValue, ErrorMessage = "Qiymat noto'g'ri kiritildi")]
  public decimal ReplacementValue { get; set; } // Mahsulotning asl qiymati (zalog uchun)

  // --- UI dagi Qo'shimcha parametrlar ---

  public bool IsMainProduct { get; set; } = true; // Asosiy mahsulot yoki Qo'shimcha

  public bool HasAccessories { get; set; } = false; // Aksessuarlar mavjudligi

  // --- Fayllar (Rasmlar) ---
  // UI dagi "Fayl tanlash" qismi uchun
  public List<IFormFile>? Images { get; set; }
}