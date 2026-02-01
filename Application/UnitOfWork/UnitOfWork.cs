using RentApi.Application.Repositries;
using RentApi.Application.Repositries.Interfaces;
using RentApi.Data;
using RentApi.Data.Entities;
using RentApi.Data.Repositories;

namespace RentApi.Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
  private readonly AppDbContext _context;

  // Private fields (Keshlab turish uchun)
  private IEquipmentRepository? _equipments;
  private IEquipmentItemRepository? _equipmentItems;
  private IRentalOrderRepository? _rentalOrders;
  private ICustomerRepository? _customers;
  private IAdminRepository? _admins;
  private IWareHouseRepository? _wareHouses;

  public UnitOfWork(AppDbContext context)
  {
    _context = context;
  }

  // Lazy Loading properties
  public IEquipmentRepository Equipments =>
    _equipments ??= new EquipmentRepository(_context);

  public IEquipmentItemRepository EquipmentItems =>
    _equipmentItems ??= new EquipmentItemRepository(_context);

  public IRentalOrderRepository RentalOrders =>
    _rentalOrders ??= new RentalOrderRepository(_context);

  public ICustomerRepository Customers =>
    _customers ??= new CustomerRepository(_context);

  public IAdminRepository Admins =>
    _admins ??= new AdminRepository(_context);

  public IWareHouseRepository WareHouses =>
    _wareHouses ??= new WarehouseRepository(_context);

  public async Task<int> CompleteAsync()
  {
    return await _context.SaveChangesAsync();
  }

  public IGenericRepository<T> Repository<T>() where T : BaseEntity=>new GenericRepository<T>(_context);

  public void Dispose()
  {
    _context.Dispose();
    GC.SuppressFinalize(this);
  }
}