using Microsoft.EntityFrameworkCore;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;

namespace RentApi.Application.Repositries;
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
  public CustomerRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<Customer?> GetByDocumentAsync(string doc) =>
    await dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Documents.Any(d => d.SerialNumber == doc) || c.JShShIR == doc);

  public Task<IEnumerable<Customer>> GetBlacklistedCustomersAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Customer>> GetDebtorsAsync() =>
    await dbSet.AsNoTracking().Where(c => c.RentalOrders.Sum(r => r.DebtAmount) > 0).ToListAsync();
}