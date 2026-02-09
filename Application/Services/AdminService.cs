namespace Application.Services;

using Application.DTOs.Employee;
using Application.Services.Interfaceses;
using RentApi.Application.DTOs;
using RentApi.Application.UnitOfWork;
using RentApi.Data;
using RentApi.Data.Entities;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public AdminService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }
    public async Task<ResponseDto<AdminDto>> CreateAsync(AdminCreateDto createDto)
    {
        try
        {
            var existingAdmin =await _unitOfWork.Admins.AddAsync(new Admin
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                DateOfBirth = createDto.BirthDate,
                Phone = createDto.PhoneNumber,
                SecurityCode = createDto.SecurityCode??"12340",
                IsActive = true,
                Role = EAdminRole.Manager,
                CreatedAt = DateTime.UtcNow
            });
          await _unitOfWork.CompleteAsync();
            return ResponseDto<AdminDto>.Success(new AdminDto
            {
                Id = existingAdmin.Id,
                FullName = $"{existingAdmin.FirstName} {existingAdmin.LastName}",
                RoleName = existingAdmin.Role.ToString(),
                PhoneNumber = existingAdmin.Phone,
                BirthDate = existingAdmin.DateOfBirth
            });
        }
        catch (System.Exception)
        {
            return ResponseDto<AdminDto>.Fail("Admin yaratishda xatolik yuz berdi.");
        }
       
    }

    public Task<ResponseDto<AdminDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<IEnumerable<AdminDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<AdminDto>> UpdateAsync(int id, AdminCreateDto updateDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<bool>> ToggleStatusAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}