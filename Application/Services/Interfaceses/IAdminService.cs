namespace Application.Services.Interfaceses;
using Application.DTOs.Employee;
using RentApi.Application.DTOs;

public interface IAdminService
{
    Task<ResponseDto<AdminDto>> CreateAsync(AdminCreateDto createDto);
    
    Task<ResponseDto<AdminDto>> GetByIdAsync(int id);
    
    Task<ResponseDto<IEnumerable<AdminDto>>> GetAllAsync();

    
    Task<ResponseDto<AdminDto>> UpdateAsync(int id, AdminCreateDto updateDto);
    
    Task<ResponseDto<bool>> ToggleStatusAsync(int id);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
}