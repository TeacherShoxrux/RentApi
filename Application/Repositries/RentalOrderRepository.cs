
using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data.Entities;

namespace RentApi.Data.Repositories;

public class RentalOrderRepository : GenericRepository<RentalOrder>, IRentalOrderRepository
{
  public RentalOrderRepository(AppDbContext context) : base(context)
  {
  }

  public Task<IEnumerable<RentalOrder>> GetByCustomerAsync(int customerId)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<RentalOrder>> GetCustomerHistoryAsync(int customerId)
  {
    return await dbSet.AsNoTracking()
      .Where(o => o.CustomerId == customerId)
      .Include(o => o.Items).ThenInclude(i => i.EquipmentItem.Equipment)
      .OrderByDescending(o => o.OrderDate)
      .ToListAsync();
  }

  public async Task<IEnumerable<RentalOrder>> GetOverdueOrdersAsync()
  {
    return await dbSet.AsNoTracking()
      .Where(o => o.OrderStatus == EOrderStatus.Active && o.ExpectedReturnDate < DateTime.UtcNow)
      .Include(o => o.Customer)
      .ToListAsync();
  }

  public Task<IEnumerable<RentalOrder>> GetActiveOrdersAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<RentalOrder>> GetUpcomingBookingsAsync(int daysAhead)
  {
    var targetDate = DateTime.UtcNow.AddDays(daysAhead);
    return await dbSet.AsNoTracking()
      .Where(o => o.OrderStatus == EOrderStatus.Reserved && o.StartDate <= targetDate)
      .Include(o => o.Customer)
      .OrderBy(o => o.StartDate)
      .ToListAsync();
  }

  public Task<IEnumerable<RentalOrder>> GetBookingsByDateAsync(DateTime date)
  {
    throw new NotImplementedException();
  }
}