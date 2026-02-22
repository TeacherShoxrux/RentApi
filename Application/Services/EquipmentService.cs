using Microsoft.EntityFrameworkCore;
using RentApi.Application.DTOs;
using RentApi.Application.Services.Interfaces;
using RentApi.Data.Entities;
using System.Linq.Expressions;
using RentApi.Application.DTOs.Equipment;
using RentApi.Application.Services.Interfaceses;
using RentApi.Application.UnitOfWork;

namespace RentApi.Application.Services;

public class EquipmentService : IEquipmentService
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IWebHostEnvironment _env;

  public EquipmentService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
  {
    _unitOfWork = unitOfWork;
    _env = env;
  }

  
  public async Task<PagedResponseDto<IEnumerable<EquipmentListDto>>> GetPagedEquipmentsAsync(EquipmentFilterDto filter)
  {
    try
    {
      // 1. Dinamik Filtr yaratish
      Expression<Func<Equipment, bool>> predicate = e =>
        (!filter.BrandId.HasValue || e.BrandId == filter.BrandId) &&
        (!filter.CategoryId.HasValue || e.CategoryId == filter.CategoryId) &&
        (string.IsNullOrEmpty(filter.SearchTerm) || e.Name.Contains(filter.SearchTerm));

      // 2. Jami sonini hisoblash
      var totalRecords = ( _unitOfWork.Equipments.FindAsync(predicate)).Count();

      // 3. Ma'lumotlarni yuklash (Performance uchun AsNoTracking ishlatiladi)
      var equipments = await _unitOfWork.Equipments.GetAllBoxedAsync(
        filter: predicate,
        orderBy: q => q.OrderByDescending(e => e.Id),
        includeProperties: "Brand,Category,Items"
      );

      // 4. Pagenation va DTO ga o'girish
      var pagedData = equipments
        .Skip((filter.Page - 1) * filter.Size)
        .Take(filter.Size)
        .Select(e => new EquipmentListDto
        {
          Id = e.Id,
          Name = e.Name,
          BrandName = e.Brand?.Name??"",
          CategoryName = e.Category?.Name??"",
          PricePerDay = e.PricePerDay,
          ImageUrl = e.ImageUrl,
          AvailableCount = e.Items.Count(i => i.Status == EEquipmentItemStatus.Available)
        });

      return new PagedResponseDto<IEnumerable<EquipmentListDto>>(pagedData, filter.Page, filter.Size, totalRecords);
    }
    catch (Exception ex)
    {
      return (PagedResponseDto<IEnumerable<EquipmentListDto>>)
        ResponseDto<IEnumerable<EquipmentListDto>>.Fail(ex.Message);
    }
  }

 public async Task<ResponseDto<int>> CreateBrandAsync(CreateBrandDto dto)
 {
   var brand = new Brand { Name = dto.Name, Details = dto.Details };

    if (dto.ImageUrl != null)
      brand.Image = dto.ImageUrl; 
    
   await _unitOfWork.Repository<Brand>().AddAsync(brand);
   await _unitOfWork.CompleteAsync();
   return ResponseDto<int>.Success(brand.Id, "Brend yaratildi");
 }

 public async Task<ResponseDto<int>> CreateCategoryAsync(CreateCategoryDto dto)
 {
   var category = new Category { Name = dto.Name,BrandId = dto.BrandId, Details = dto.Details ,Image = dto.Image};

   await _unitOfWork.Repository<Category>().AddAsync(category);
   await _unitOfWork.CompleteAsync();
   return ResponseDto<int>.Success(category.Id, "Kategoriya yaratildi");
 }

 // --- TEXNIKA (EQUIPMENT) QO'SHISH ---
 public async Task<ResponseDto<int>> CreateEquipmentAsync(CreateEquipmentDto dto)
 {
   // Brend va Kategoriya borligini tekshirish (Performance: AsNoTracking ishlatiladi)
   var brandExists = await _unitOfWork.Repository<Brand>().GetByIdAsync(dto.BrandId);
   var catExists = await _unitOfWork.Repository<Category>().GetByIdAsync(dto.CategoryId);

   if (brandExists == null || catExists == null)
     return ResponseDto<int>.Fail("Brend yoki Kategoriya xato!");

    
   var equipment = new Equipment
   {
     Name = dto.Name,
     BrandId = dto.BrandId,
     CategoryId = dto.CategoryId,
     PricePerDay = dto.PricePerDay,
     ReplacementValue = dto.ReplacementValue,
     Model = dto.Model,
     Details = dto.Description,
    IsMainProduct = dto.IsMainProduct,
    HasAccessories = dto.HasAccessories

   };
      if (dto.Image != null)
        equipment.ImageUrl = dto.Image;
   var e= await _unitOfWork.Equipments.AddAsync(equipment);
   await _unitOfWork.CompleteAsync();
  List<EquipmentItem> items = new List<EquipmentItem>();
  for (int i = 0; i < dto.Quantity; i++)
  {
    items.Add(new EquipmentItem
    {
      Status = EEquipmentItemStatus.Available,
      Condition = "Yangi",
      SerialNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
      WareHouseId = e.WareHouseId,
      EquipmentId=e.Id // Default ombor
    });
  }
   await _unitOfWork.Repository<EquipmentItem>().AddRangeAsync(items);

   await _unitOfWork.CompleteAsync();

   return ResponseDto<int>.Success(equipment.Id, "Texnika katalogga qo'shildi");
 }

 // --- YORDAMCHI: FAYL SAQLASH ---
 private async Task<string> SaveFileAsync(IFormFile file, string subFolder)
 {
   string root = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
   string folder = Path.Combine(root, "uploads", subFolder);

   if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

   string fileName = $"{Guid.NewGuid()}_{file.FileName}";
   string fullPath = Path.Combine(folder, fileName);

   using (var stream = new FileStream(fullPath, FileMode.Create))
   {
     await file.CopyToAsync(stream);
   }

   return $"/uploads/{subFolder}/{fileName}";
 }
 public async Task<ResponseDto<IEnumerable<BrandDto>>> GetAllBrandsAsync()
 {
   var brands = await _unitOfWork.Repository<Brand>().GetAllBoxedAsync(
     orderBy: q => q.OrderBy(b => b.Name)
   );

   var result = brands.Select(b => new BrandDto
   {
     Id = b.Id,
     Name = b.Name,
     ImageUrl = b.Image
   });

   return ResponseDto<IEnumerable<BrandDto>>.Success(result);
 }

 public async Task<ResponseDto<IEnumerable<CategoryDto>>> GetCategoriesByBrandAsync(int brandId)
 {
    var categories = await _unitOfWork.Repository<Category>().GetAllBoxedAsync(
      filter: c => c.BrandId == brandId,
      orderBy: q => q.OrderBy(c => c.Name)
    );

   // Unikal kategoriyalarni ajratib olish
   var result = categories
     .GroupBy(c => c.Id)
     .Select(g => g.First())
     .Select(c => new CategoryDto
     {
       Id = c.Id,
       Name = c.Name,
       ImageUrl = c.Image,
       Details = c.Details
     });

   return ResponseDto<IEnumerable<CategoryDto>>.Success(result);
 }
}