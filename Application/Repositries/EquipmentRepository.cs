using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data.Entities;

namespace RentApi.Data.Repositories;

public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
{
  public EquipmentRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Equipment>> GetTopPopularByCategoryAsync(int categoryId, int count)
  {
    return await dbSet.AsNoTracking()
      .Where(e => e.CategoryId == categoryId)
      .OrderByDescending(e => e.Items.Count) // Hozircha soni bo'yicha
      .Take(count)
      .ToListAsync();
  }

  public Task<IEnumerable<Equipment>> GetByCategoryAsync(int categoryId)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Equipment>> GetTopPopularAsync(int count)
  {
    throw new NotImplementedException();
  }
}