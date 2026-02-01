using RentApi.Application.DTOs;
using RentApi.Application.DTOs.Equipment;

namespace RentApi.Application.Services.Interfaceses;

public interface IEquipmentService
{
  // Pagenated ro'yxat (Filtrlar bilan)
  Task<PagedResponseDto<IEnumerable<EquipmentListDto>>> GetPagedEquipmentsAsync(EquipmentFilterDto filter);

  // Yangi texnika qo'shish (UI dagi "Mahsulot qo'shish" formasi uchun)
  Task<ResponseDto<int>> CreateEquipmentAsync(CreateEquipmentDto dto);
  Task<ResponseDto<int>> CreateBrandAsync(CreateBrandDto dto);
  Task<ResponseDto<int>> CreateCategoryAsync(CreateCategoryDto dto);
  Task<ResponseDto<IEnumerable<BrandDto>>> GetAllBrandsAsync();
  Task<ResponseDto<IEnumerable<CategoryDto>>> GetCategoriesByBrandAsync(int brandId);
}