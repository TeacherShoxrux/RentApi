using System.Linq.Expressions;
using RentApi.Application.DTOs;
using RentApi.Application.Services.Interfaces;
using RentApi.Data.Entities;
using RentApi.Application.UnitOfWork;
using System.Text.Json; // Fayl saqlash uchun

namespace RentApi.Application.Services;

public class CustomerService : ICustomerService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _env; // Rasmlarni papkaga saqlash uchun

  public CustomerService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
  {
    _unitOfWork = unitOfWork;
    _env = env;
  }

  public async Task<ResponseDto<CustomerDto>> CreateCustomerAsync(CreateCustomerDto dto)
  {
    try{
    var existing = await _unitOfWork.Customers.GetByDocumentAsync(dto.JShShIR!);
    if (existing != null)
      throw new Exception($"Bu JSHSHIR ({dto.JShShIR}) bilan mijoz allaqachon mavjud!");

    var customer = new Customer
    {
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      IsWoman = dto.IsWoman,
      DateOfBirth = dto.DateOfBirth ?? DateTime.MinValue,
      JShShIR = dto.JShShIR,
      Note = dto.Note,
      Phones= dto.Phones.Select(e=> new Phone
      {
        PhoneNumber = e.PhoneNumber,
        Name = e.Name
      }).ToList(),
      CreatedAt = DateTime.UtcNow
    };

    string fullSerial = $"{dto.PassportSeries}{dto.PassportNumber}".Trim().ToUpper();

    var document = new Document
    {
      DocumentType = "Passport",
      Name = "Shaxsni tasdiqlovchi hujjat",
      SerialNumber = fullSerial,
      JShShR = dto.JShShIR,
      Details = dto.Note,
      IsOriginalLeft = dto.IsOriginalDocumentLeft,
      LeftAt = dto.IsOriginalDocumentLeft ? DateTime.UtcNow : null,
      Status = EDocumentStatus.Active,
      FilePath =JsonSerializer.Serialize(dto.DocumentScans)
    };

    // Hujjatni mijozga qo'shamiz
    customer.Documents.Add(document);

    // 5. Bazaga saqlash
    await _unitOfWork.Customers.AddAsync(customer);
    await _unitOfWork.CompleteAsync();

    var result = MapToDto(customer);
    return ResponseDto<CustomerDto>.Success(result);
    }
    catch (Exception ex)
    {
      return ResponseDto<CustomerDto>.Fail($"Xatolik: {ex.Message}");
    }

  }

  // Hujjatni qaytarib berish funksiyasi
  public async Task ReturnOriginalDocumentAsync(int customerId)
  {
    var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
    if (customer == null) throw new Exception("Mijoz topilmadi");

    // Hozir "Zalogda" turgan hujjatni topamiz
    var doc = customer.Documents.FirstOrDefault(d => d.IsOriginalLeft);

    if (doc == null)
      throw new Exception("Bu mijozning hujjati bizda emas (Zalogda yo'q).");

    // Statusni o'zgartiramiz
    doc.IsOriginalLeft = false;
    doc.Status = EDocumentStatus.Returned;
    doc.ReturnedAt = DateTime.UtcNow;

    _unitOfWork.Customers.Update(customer); // Yoki Documents reposi orqali update
    await _unitOfWork.CompleteAsync();
  }

  public async Task<PagedResponseDto<IEnumerable<CustomerDto>>> GetPagedCustomersAsync(string? searchTerm = null,int pageNumber=1, int pageSize=20)
  {
    try
    {
      // 1. Filtrlash mantiqi
      Expression<Func<Customer, bool>> filter = c =>
        string.IsNullOrEmpty(searchTerm) ||
        c.FirstName.Contains(searchTerm) ||
        c.LastName.Contains(searchTerm) ||
        c.JShShIR.Contains(searchTerm);

      // 2. Jami yozuvlar sonin
      // i olish (Pagenation uchun)
      var totalRecords = ( _unitOfWork.Customers.FindAsync(filter)).Count();

      // 3. Ma'lumotlarni qismlarga bo'lib olish (Skip & Take)
      var customers = await _unitOfWork.Customers.GetAllBoxedAsync(
        filter: filter,
        orderBy: q => q.OrderByDescending(c => c.Id),
        includeProperties: "Documents,Phones"
        // Repository'da skip/take imkoniyati bo'lsa shuni ishlating
      );

      // Pagenationni amalga oshirish
      var pagedData = customers
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(MapToDto);

      return new PagedResponseDto<IEnumerable<CustomerDto>>(pagedData, pageNumber, pageSize, totalRecords);
    }
    catch (Exception ex)
    {
      var response = new PagedResponseDto<IEnumerable<CustomerDto>>(null, pageNumber, pageSize, 0);
      response.IsSuccess = false;
      response.Message = ex.Message;
      return response;
    }
  }
public async Task<ResponseDto<bool>> TogglePassportLocationAsync(int customerId)
{
  var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
  if (customer == null) return ResponseDto<bool>.Fail("Mijoz topilmadi", 404);

  var doc = customer.Documents.FirstOrDefault();
  if (doc == null) return ResponseDto<bool>.Fail("Hujjat topilmadi", 404);

  // Holatni teskarisiga o'zgartiramiz
  doc.IsOriginalLeft = !doc.IsOriginalLeft;
  doc.Status = doc.IsOriginalLeft ? EDocumentStatus.InSafe : EDocumentStatus.Active;
  doc.LeftAt = doc.IsOriginalLeft ? DateTime.Now : null;

  _unitOfWork.Customers.Update(customer);
  await _unitOfWork.CompleteAsync();

  return ResponseDto<bool>.Success(doc.IsOriginalLeft, $"Passport joylashuvi: {doc.Status}");
}

  // Yordamchi Mapper
  private CustomerDto MapToDto(Customer c)
  {
    var mainDoc = c.Documents.FirstOrDefault(); // Asosiy hujjat

    return new CustomerDto
    {
      Id = c.Id,
      FirstName = c.FirstName,
      LastName = c.LastName,
      JShShIR = c.JShShIR,
      // Agar SerialNumber "AA 1234567" bo'lsa, uni ajratib ko'rsatish mumkin
      PassportSeries = mainDoc?.SerialNumber.Split(' ')[0] ?? "",
      PassportNumber = mainDoc?.SerialNumber.Split(' ')[1] ?? "",

      IsOriginalDocumentLeft = mainDoc?.IsOriginalLeft ?? false,
      // ... boshqa maydonlar
    };
  }

  // Boshqa interfeys metodlari (Search, Update...) shu yerda bo'ladi
  public async Task<ResponseDto<IEnumerable<CustomerDto>>> SearchAsync(string query)
  {
    if (string.IsNullOrWhiteSpace(query))
      return ResponseDto<IEnumerable < CustomerDto >>.Success(Enumerable.Empty<CustomerDto>());

    // UnitOfWork orqali repositorydan qidiramiz
    // AsNoTracking va IQueryable ishlatilgani uchun bu juda tez ishlaydi
    var customers = await _unitOfWork.Customers.GetAllBoxedAsync(
      filter: c => c.FirstName.Contains(query) ||
                   c.LastName.Contains(query) ||
                   c.JShShIR.Contains(query) ||
                   c.Documents.Any(d => d.SerialNumber.Contains(query)),
      includeProperties: "Documents,Phones" // Bog'liqliklarni ham yuklaymiz
    );

    return ResponseDto<IEnumerable<CustomerDto>>.Success(customers.Select(MapToDto));
  }
  public Task<CustomerDto> GetByIdAsync(int id) => throw new NotImplementedException();
  public Task<ResponseDto<IEnumerable<CustomerDto>>> GetAllAsync() => throw new NotImplementedException();
  public Task<ResponseDto<CustomerDto>> UpdateCustomerAsync(int id, CreateCustomerDto dto) => throw new NotImplementedException();
}