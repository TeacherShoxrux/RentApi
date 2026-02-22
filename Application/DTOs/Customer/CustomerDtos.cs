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
  public string DocumentType { get; set; } = "Passport";
  public string PassportSeries { get; set; } = string.Empty; // Masalan: AA
  public string PassportNumber { get; set; } = string.Empty; // Masalan: 1234567
  public string? JShShIR { get; set; } = string.Empty; // PINFL
  public bool? IsWoman { get; set; }
  public bool IsOriginalDocumentLeft { get; set; } = false;
  public string? Address { get; set; }
  public string? Note { get; set; } // "Details" maydoni uchun
  public string? UserPhotoUrl { get; set; }
  public List<string>? DocumentScans { get; set; }
  public List<CreatePhoneDto> Phones { get; set; } = new List<CreatePhoneDto>();
}

