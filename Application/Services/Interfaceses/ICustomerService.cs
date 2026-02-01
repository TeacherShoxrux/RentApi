using RentApi.Application.DTOs;

namespace RentApi.Application.Services.Interfaces;

public interface ICustomerService
{
  // Yangi mijoz qo'shish (Fayllar bilan birga)
  Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto);

  // Mijoz ma'lumotlarini o'zgartirish
  Task<CustomerDto> UpdateCustomerAsync(int id, CreateCustomerDto dto);

  // Barchasini olish (Filter va Paging bilan bo'lishi mumkin)
  Task<IEnumerable<CustomerDto>> GetAllAsync();

  // ID orqali olish
  Task<CustomerDto> GetByIdAsync(int id);

  // Passport yoki Ism bo'yicha qidirish (UI dagi "Qidirish" tugmasi uchun)
  Task<IEnumerable<CustomerDto>> SearchAsync(string query);

  // Zalogdagi hujjatni qaytarib berish
  Task ReturnOriginalDocumentAsync(int customerId);
}