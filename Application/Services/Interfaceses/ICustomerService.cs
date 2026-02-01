using RentApi.Application.DTOs;

namespace RentApi.Application.Services.Interfaces;

public interface ICustomerService
{
  // Yangi mijoz qo'shish (Fayllar bilan birga)
  Task<ResponseDto<CustomerDto>> CreateCustomerAsync(CreateCustomerDto dto);

  // Mijoz ma'lumotlarini o'zgartirish
  Task<ResponseDto<CustomerDto>> UpdateCustomerAsync(int id, CreateCustomerDto dto);

  // Barchasini olish (Filter va Paging bilan bo'lishi mumkin)
  Task<ResponseDto<IEnumerable<CustomerDto>>> GetAllAsync();

  // ID orqali olish
  Task<CustomerDto> GetByIdAsync(int id);

  // Passport yoki Ism bo'yicha qidirish (UI dagi "Qidirish" tugmasi uchun)
  Task<ResponseDto<IEnumerable<CustomerDto>>> SearchAsync(string query);

  Task<ResponseDto<bool>> TogglePassportLocationAsync(int customerId);
  // Zalogdagi hujjatni qaytarib berish
  Task ReturnOriginalDocumentAsync(int customerId);
  Task<PagedResponseDto<IEnumerable<CustomerDto>>> GetPagedCustomersAsync(string? searchTerm ,int pageNumber=1, int pageSize=20);
}