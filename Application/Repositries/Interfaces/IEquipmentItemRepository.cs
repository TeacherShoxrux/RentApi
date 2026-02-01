using RentApi.Data.Entities;

namespace RentApi.Application.Repositries.Interfaces;

public interface IEquipmentItemRepository : IGenericRepository<EquipmentItem>
{
  // Seriya raqami bo'yicha aniq bittasini topish (Scan qilishda kerak)
  Task<EquipmentItem?> GetBySerialNumberAsync(string serialNumber);

  // Ma'lum bir ombordagi faqat "Available" (Bo'sh) narsalarni olish
  Task<IEnumerable<EquipmentItem>> GetAvailableItemsByWarehouseAsync(int warehouseId);

  // Booking uchun: Berilgan vaqt oralig'ida bo'sh bo'lgan narsalarni tekshirish
  Task<bool> IsItemAvailableForDateAsync(int itemId, DateTime start, DateTime end);
  
}