using RentApi.Data.Entities;

namespace RentApi.Application.Repositries.Interfaces;

public interface IEquipmentRepository : IGenericRepository<Equipment>
{
  // Brend yoki Kategoriya bo'yicha qidirish
  Task<IEnumerable<Equipment>> GetByCategoryAsync(int categoryId);

  // Eng ko'p ijaraga olinadigan TOP uskunalar (Statistika uchun)
  Task<IEnumerable<Equipment>> GetTopPopularAsync(int count);
}