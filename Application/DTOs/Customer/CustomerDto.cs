using Microsoft.AspNetCore.Http;
using RentApi.Application.DTOs.Customer;
using RentApi.Data.Entities; // Gender enum uchun

namespace RentApi.Application.DTOs;
public class CustomerDto : CreateCustomerDto
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }

  // Qo'shimcha: Hozirda mijoza qancha qarzi bor?
  public decimal CurrentDebt { get; set; }

  // Rasm URLlari
  public string? PhotoUrl { get; set; }
}