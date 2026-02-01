using RentApi.Application.DTOs;
using RentApi.Application.DTOs.RentalOrder;

namespace RentApi.Application.Services.Interfaceses;

public interface IRentalService
{
  Task<ResponseDto<RentalOrderResultDto>> CreateRentalOrderAsync(CreateRentalOrderDto dto);
  Task<PagedResponseDto<IEnumerable<RentalOrderListDto>>> GetPagedOrdersAsync(RentalFilterDto filter);
}