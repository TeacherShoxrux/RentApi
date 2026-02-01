
using RentApi.Application.DTOs;
using RentApi.Application.DTOs.Payment;

namespace RentApi.Application.Services;

public interface IPaymentService
{
  Task<ResponseDto<int>> CreatePaymentMethodAsync(CreatePaymentMethodDto dto);
  Task<ResponseDto<IEnumerable<PaymentMethodDto>>> GetActiveMethodsAsync();
  Task<ResponseDto<bool>> ToggleStatusAsync(int id); // Aktiv/Passiv qilish
}