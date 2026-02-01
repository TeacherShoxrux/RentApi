using RentApi.Application.Repositries.Interfaces;
using RentApi.Data.Entities;

namespace RentApi.Application.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
  IEquipmentRepository Equipments { get; }
  IEquipmentItemRepository EquipmentItems { get; }
  IRentalOrderRepository RentalOrders { get; }
  ICustomerRepository Customers { get; }
  IAdminRepository Admins { get; }
  IWareHouseRepository WareHouses { get; }
  Task<int> CompleteAsync();
  IGenericRepository<T> Repository<T>() where T : BaseEntity;
  
}