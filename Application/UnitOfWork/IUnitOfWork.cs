using RentApi.Application.Repositries.Interfaces;

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
}