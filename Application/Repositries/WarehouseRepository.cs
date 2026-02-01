using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;

namespace RentApi.Application.Repositries;
using Microsoft.EntityFrameworkCore;
public class WarehouseRepository : GenericRepository<WareHouse>, IWareHouseRepository
{
  public WarehouseRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<(int, decimal)> GetInventorySummaryAsync(int wId)
  {
    var items = _context.EquipmentItems.AsNoTracking().Where(i => i.WareHouseId == wId);
    var count = await items.CountAsync();
    var value = await items.SumAsync(i => i.Equipment.PricePerDay);
    return (count, value);
  }
}