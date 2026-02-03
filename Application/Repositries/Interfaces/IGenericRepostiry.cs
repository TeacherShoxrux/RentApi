using System.Linq.Expressions;
using RentApi.Data.Entities;

// BaseEntity shu yerda deb faraz qilamiz

namespace RentApi.Application.Repositries.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
  // 1. ID bo'yicha olish
  Task<T?> GetByIdAsync(int id);

  // 2. Barchasini olish (Ro'yxat)
  IQueryable<T> GetAllAsync();

  // 3. Shart bo'yicha qidirish (Masalan: faqat "Available" bo'lganlar)
  IQueryable<T> FindAsync(Expression<Func<T, bool>> expression);

  // 4. Qo'shish
  Task<T> AddAsync(T entity);
  Task AddRangeAsync(IEnumerable<T> entities);

  // 5. O'chirish
  void Remove(T entity);
  void RemoveRange(IEnumerable<T> entities);

  // 6. Yangilash
  void Update(T entity);

  // 7. Universal "Get" (Eng kuchli qurol)
  // Filter, Sortirovka va Include (Join) hammasi bittada
  Task<IEnumerable<T>> GetAllBoxedAsync(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    string includeProperties = "");

  // 8. O'zgarishlarni saqlash (Agar UnitOfWork ishlatmasangiz)
  Task<bool> SaveChangesAsync();
}