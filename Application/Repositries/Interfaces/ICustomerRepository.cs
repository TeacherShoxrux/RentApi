using RentApi.Data.Entities;

namespace RentApi.Application.Repositries.Interfaces;
public interface ICustomerRepository : IGenericRepository<Customer>
{
  // Pasport yoki JSHSHIR bo'yicha qidirish (Unikal)
  Task<Customer?> GetByDocumentAsync(string passportData);

  // "Qora ro'yxat"dagi (Blacklist) mijozlarni olish
  Task<IEnumerable<Customer>> GetBlacklistedCustomersAsync();

  // Qarzi bor mijozlarni topish (Filter)
  Task<IEnumerable<Customer>> GetDebtorsAsync();
}