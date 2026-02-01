using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;

namespace RentApi.Application.Repositries;

public class EquipmentItemRepository : GenericRepository<EquipmentItem>, IEquipmentItemRepository
{
  public EquipmentItemRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<EquipmentItem?> GetBySerialNumberAsync(string serialNumber)
  {
    return await dbSet.AsNoTracking()
      .Include(ei => ei.Equipment)
      .Include(ei => ei.WareHouse)
      .FirstOrDefaultAsync(ei => ei.SerialNumber == serialNumber);
  }

  public Task<IEnumerable<EquipmentItem>> GetAvailableItemsByWarehouseAsync(int warehouseId)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<EquipmentItem>> GetAvailableItemsInWarehouseAsync(int warehouseId)
  {
    return await dbSet.AsNoTracking()
      .Where(ei => ei.WareHouseId == warehouseId && ei.Status == EEquipmentItemStatus.Available)
      .Include(ei => ei.Equipment)
      .ToListAsync();
  }

  // --- ENG MUHIM JOYI: BOOKING CHECK ---
  public async Task<bool> IsItemAvailableForDateAsync(int itemId, DateTime start, DateTime end)
  {
    // Agar quyidagi shartlarga tushadigan biron buyurtma bo'lsa, demak BAND.
    bool isBooked = await _context.RentalOrderItems.AsNoTracking()
      .AnyAsync(roi =>
        roi.EquipmentItemId == itemId &&
        (roi.RentalOrder.OrderStatus == EOrderStatus.Reserved || roi.RentalOrder.OrderStatus == EOrderStatus.Active) &&
        // Vaqtlar kesishuvi (Overlap Logic)
        roi.RentalOrder.StartDate < end &&
        roi.RentalOrder.ExpectedReturnDate > start
      );

    return !isBooked;
  }
}