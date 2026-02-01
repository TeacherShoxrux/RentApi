using RentApi.Data.Entities;
namespace RentApi.Application.Repositries.Interfaces;

public interface IWareHouseRepository : IGenericRepository<WareHouse>
{
  // Ombordagi umumiy tovarlar soni va qiymatini hisoblash
  // Return: (Jami tovar soni, Jami summa)
  Task<(int TotalItems, decimal TotalValue)> GetInventorySummaryAsync(int warehouseId);
}