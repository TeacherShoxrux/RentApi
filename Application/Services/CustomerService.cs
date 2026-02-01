using RentApi.Application.DTOs;
using RentApi.Application.Services.Interfaces;
using RentApi.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using RentApi.Application.UnitOfWork; // Fayl saqlash uchun

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

  public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
  {
    // 1. Validatsiya: JSHSHIR bo'yicha tekshirish
    var existing = await _unitOfWork.Customers.GetByDocumentAsync(dto.JShShIR);
    if (existing != null)
      throw new Exception($"Bu JSHSHIR ({dto.JShShIR}) bilan mijoz allaqachon mavjud!");

    // 2. Mijoz (Customer) yaratish
    var customer = new Customer
    {
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      IsWoman = dto.IsWoman,
      DateOfBirth = dto.DateOfBirth ?? DateTime.MinValue,
      JShShIR = dto.JShShIR,
      Note = dto.Note,
      // Telefonlarni birlashtirib saqlaymiz: "991234567,901234567"
      Phones= dto.Phones.Select(e=> new Phone
      {
        PhoneNumber = e.PhoneNumber,
        Name = e.Name
      }).ToList(),
      CreatedAt = DateTime.UtcNow
    };

    // 3. Hujjat (Document) yaratish
    // UI dagi Seriya (AA) va Raqam (1234567) ni birlashtiramiz
    string fullSerial = $"{dto.PassportSeries} {dto.PassportNumber}".Trim();

    var document = new Document
    {
      DocumentType = "Passport", // Yoki DTO dan keladigan DocumentType
      Name = "Shaxsni tasdiqlovchi hujjat",
      SerialNumber = fullSerial,
      JShShR = dto.JShShIR, // Sizning klassdagi nom bilan bir xil
      Details = dto.Note,

      // --- ZALOG LOGIKASI ---
      IsOriginalLeft = dto.IsOriginalDocumentLeft,
      LeftAt = dto.IsOriginalDocumentLeft ? DateTime.UtcNow : null,
      Status = dto.IsOriginalDocumentLeft ? "Zalogda (Seyfda)" : "Mijozda",

      // Fayl yo'li (Pastda yuklaymiz)
      FilePath = ""
    };

    // 4. Faylni yuklash (Passport Scan)
    if (dto.DocumentScans != null && dto.DocumentScans.Count > 0)
    {
      // Faqat birinchi faylni olamiz (yoki ko'p fayl uchun alohida jadval kerak)
      var file = dto.DocumentScans[0];
      string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "documents");

      // Papka yo'q bo'lsa yaratamiz
      if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

      // Unikal nom beramiz: GUID + original nom
      string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
      string filePath = Path.Combine(uploadsFolder, uniqueFileName);

      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(fileStream);
      }

      // Bazaga faqat fayl nomini yoki nisbiy yo'lni yozamiz
      document.FilePath = $"/uploads/documents/{uniqueFileName}";
    }

    // Hujjatni mijozga qo'shamiz
    customer.Documents.Add(document);

    // 5. Bazaga saqlash
    await _unitOfWork.Customers.AddAsync(customer);
    await _unitOfWork.CompleteAsync();

    return MapToDto(customer);
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
    doc.Status = "Qaytarildi";
    doc.ReturnedAt = DateTime.UtcNow;

    _unitOfWork.Customers.Update(customer); // Yoki Documents reposi orqali update
    await _unitOfWork.CompleteAsync();
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
  public Task<IEnumerable<CustomerDto>> SearchAsync(string query) => throw new NotImplementedException();
  public Task<CustomerDto> GetByIdAsync(int id) => throw new NotImplementedException();
  public Task<IEnumerable<CustomerDto>> GetAllAsync() => throw new NotImplementedException();
  public Task<CustomerDto> UpdateCustomerAsync(int id, CreateCustomerDto dto) => throw new NotImplementedException();
}