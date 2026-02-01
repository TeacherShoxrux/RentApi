using RentApi.Data.Entities;

namespace RentApi.Application.Repositries.Interfaces;

public interface IRentalOrderRepository : IGenericRepository<RentalOrder>
{
  // Mijozning barcha buyurtmalari (Tarix)
  Task<IEnumerable<RentalOrder>> GetByCustomerAsync(int customerId);
  Task<IEnumerable<RentalOrder>> GetCustomerHistoryAsync(int customerId);

  // Muddatidan o'tib ketgan (Kechikkan) buyurtmalar ro'yxati
  Task<IEnumerable<RentalOrder>> GetOverdueOrdersAsync();

  // Faqat "Active" (Hozir ijarada yurgan) buyurtmalar
  Task<IEnumerable<RentalOrder>> GetActiveOrdersAsync();
  Task<IEnumerable<RentalOrder>> GetUpcomingBookingsAsync(int daysAhead);

  // Ma'lum bir kunda bron qilinganlarni topish (Planner/Kalendar uchun)
  Task<IEnumerable<RentalOrder>> GetBookingsByDateAsync(DateTime date);
}