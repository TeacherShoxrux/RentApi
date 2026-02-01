using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;

namespace RentApi.Application.Repositries;
public class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
  public AdminRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<Admin?> GetBySecurityCodeAsync(string pin) =>
    await dbSet.AsNoTracking().Include(a => a.WareHouse).FirstOrDefaultAsync(a => a.SecurityCode == pin);

  public Task<IEnumerable<Admin>> GetByWarehouseAsync(int warehouseId)
  {
    throw new NotImplementedException();
  }
}