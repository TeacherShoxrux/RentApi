using RentApi.Data.Entities;

namespace RentApi.Application.Repositries.Interfaces;

public interface IAdminRepository : IGenericRepository<Admin>
{
  // PIN kod (Hash) bo'yicha adminni topish (Login uchun)
  Task<Admin?> GetBySecurityCodeAsync(string hashedPin);

  // Ma'lum bir omborda ishlaydigan xodimlarni olish
  Task<IEnumerable<Admin>> GetByWarehouseAsync(int warehouseId);
}