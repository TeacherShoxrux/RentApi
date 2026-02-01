using Microsoft.AspNetCore.Http;
using RentApi.Application.DTOs.Customer;
using RentApi.Data.Entities; // Gender enum uchun

namespace RentApi.Application.DTOs;

public class CreateCustomerDto
{
  // Shaxsiy ma'lumotlar
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public DateTime? DateOfBirth { get; set; }

  // Hujjat ma'lumotlari
  public string PassportSeries { get; set; } = string.Empty; // Masalan: AA
  public string PassportNumber { get; set; } = string.Empty; // Masalan: 1234567
  public string? JShShIR { get; set; } = string.Empty; // PINFL
  public bool? IsWoman { get; set; }
  // --- MUHIM: ZALOG QISMI ---
  // UI dagi "Passport bormi?" degan joy yoki alohida checkbox
  // True bo'lsa -> Mijoz passportini tashlab ketdi (Zalog)
  public bool IsOriginalDocumentLeft { get; set; } = false;
  public string? Address { get; set; }
  public string? Note { get; set; } // "Details" maydoni uchun

  // Fayllar (Rasm va Scan)
  public IFormFile? UserPhoto { get; set; }
  public List<IFormFile>? DocumentScans { get; set; }
  public List<CreatePhoneDto> Phones { get; set; } = new List<CreatePhoneDto>();
}

public class CustomerDto : CreateCustomerDto
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }

  // Qo'shimcha: Hozirda mijoza qancha qarzi bor?
  public decimal CurrentDebt { get; set; }

  // Rasm URLlari
  public string? PhotoUrl { get; set; }
}