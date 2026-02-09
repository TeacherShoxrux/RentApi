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
    public Task<ResponseDto<AdminDto>> CreateAsync(AdminCreateDto createDto)
    {
        try
        {
            var existingAdmin = _unitOfWork.Admins.AddAsync(new Admin
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

        }
        catch (System.Exception)
        {

            throw;
        }
        throw new NotImplementedException();
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