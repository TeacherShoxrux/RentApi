using RentApi.Application.DTOs;
using RentApi.Application.DTOs.Payment;
using RentApi.Application.Services;
using RentApi.Application.UnitOfWork;
using RentApi.Data.Entities;

public class PaymentService : IPaymentService
{
  private readonly IUnitOfWork _uow;

  public PaymentService(IUnitOfWork uow) => _uow = uow;

  public async Task<ResponseDto<int>> CreatePaymentMethodAsync(CreatePaymentMethodDto dto)
  {
    var method = new PaymentMethod
    {
      Name = dto.Name,
      Description = dto.Description,
      Icon = dto.Icon,
      CreatedAt = DateTime.UtcNow
    };

    await _uow.Repository<PaymentMethod>().AddAsync(method);
    await _uow.CompleteAsync();

    return ResponseDto<int>.Success(method.Id, "To'lov usuli qo'shildi");
  }

  public async Task<ResponseDto<IEnumerable<PaymentMethodDto>>> GetActiveMethodsAsync()
  {
    // Faqat IsActive = true bo'lgan usullarni pagenationsiz tezkor yuklaymiz
    var methods = await _uow.Repository<PaymentMethod>().GetAllBoxedAsync(
      filter: m => m.IsActive == true,
      orderBy: q => q.OrderBy(m => m.Name)
    );

    var result = methods.Select(m => new PaymentMethodDto
    {
      Id = m.Id,
      Name = m.Name,
      Description = m.Description,
      IsActive = m.IsActive,
      Icon = m.Icon
    });

    return ResponseDto<IEnumerable<PaymentMethodDto>>.Success(result);
  }

  public async Task<ResponseDto<bool>> ToggleStatusAsync(int id)
  {
    var method = await _uow.Repository<PaymentMethod>().GetByIdAsync(id);
    if (method == null) return ResponseDto<bool>.Fail("Topilmadi", 404);

    method.IsActive = !method.IsActive;
    method.UpdatedAt = DateTime.UtcNow;

    _uow.Repository<PaymentMethod>().Update(method);
    await _uow.CompleteAsync();

    return ResponseDto<bool>.Success(method.IsActive, "Holat o'zgardi");
  }
}