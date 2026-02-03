using Application.DTOs.RentalOrder;
using RentApi.Application.DTOs;
using RentApi.Application.DTOs.RentalOrder;

namespace RentApi.Application.Services.Interfaceses;

public interface IRentalService
{
  Task<ResponseDto<RentalOrderResultDto>> CreateRentalOrderAsync(CreateRentalOrderDto dto);
  Task<PagedResponseDto<IEnumerable<RentalOrderListDto>>> GetPagedOrdersAsync(RentalFilterDto filter);
  Task<PagedResponseDto<List<RentalOrderDto>>> GetActiveRentalsAsync(string? searchTerm);
  Task<ResponseDto<RentalOrderDetailDto>> GetOrderDetailsAsync(int orderId);
}