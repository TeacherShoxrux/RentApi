using RentApi.Application.Repositries.Interfaces;

namespace RentApi.Application.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
  IEquipmentRepository Equipments { get; }
  IEquipmentItemRepository EquipmentItems { get; }
  IRentalOrderRepository RentalOrders { get; }
  ICustomerRepository Customers { get; }
  IAdminRepository Admins { get; }
  IWarehouseRepository Warehouses { get; }

  Task<int> CompleteAsync();
}