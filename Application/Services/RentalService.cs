using System.Linq.Expressions;
using Application.DTOs.RentalOrder;
using Microsoft.EntityFrameworkCore;
using RentApi.Application.DTOs;
using RentApi.Application.DTOs.RentalOrder;
using RentApi.Application.Services.Interfaceses;
using RentApi.Application.UnitOfWork;
using RentApi.Data.Entities;

namespace RentApi.Application.Services;
public class RentalService: IRentalService
{
  private readonly IUnitOfWork _uow;

  public RentalService(IUnitOfWork uow) => _uow = uow;
  
  public async Task<ResponseDto<RentalOrderResultDto>> CreateRentalOrderAsync(CreateRentalOrderDto dto)
  {
    try
    {
      // 1. Validatsiya va Muddatni hisoblash (Eski kod bilan bir xil)
      if (dto.StartDate >= dto.ExpectedReturnDate)
        return ResponseDto<RentalOrderResultDto>.Fail("Sana xato kiritildi.");

      TimeSpan duration = dto.ExpectedReturnDate - dto.StartDate;
      double totalDays = Math.Ceiling(duration.TotalDays);
      if (totalDays <= 0) totalDays = 1;

      decimal calculatedStandardSum = 0;
      var orderItems = new List<RentalOrderItem>();

      // 2. LOGIKA O'ZGARDI: Har bir so'ralgan mahsulot turi bo'yicha aylanamiz
      foreach (var req in dto.Items)
      {
        if (req.Quantity <= 0) continue;

        // A) Bazadan shu turdagi va statusi "Available" bo'lgan itemlarni olamiz
        // Bizga faqat kerakli sonichasi (Take) kerak.
        var availableItems = await _uow.EquipmentItems.GetAllBoxedAsync(
          filter: i => i.EquipmentId == req.EquipmentId && i.Status == EEquipmentItemStatus.Available,
          includeProperties: "Equipment"
        );

        // Biz xotiraga yuklab olib (IEnumerable), keyin sanaymiz yoki 
        // Repositoryda to'g'ridan-to'g'ri Take(qty) qilish samaraliroq bo'lardi.
        // Hozirgi UnitOfWork imkoniyati bo'yicha eng oddiy yo'li:

        var pickedItems = availableItems.Take(req.Quantity).ToList();

        // B) Yetarlicha bormi tekshiramiz?
        if (pickedItems.Count < req.Quantity)
        {
          // Qaysi mahsulot yetmayotganini aniqlash uchun nomini olamiz
          var product = await _uow.Repository<Equipment>().GetByIdAsync(req.EquipmentId);
          string prodName = product?.Name ?? $"ID:{req.EquipmentId}";

          return ResponseDto<RentalOrderResultDto>.Fail(
            $"Yetarli zaxira yo'q! '{prodName}' dan {req.Quantity} ta so'raldi, lekin omborda {pickedItems.Count} ta bor.");
        }

        // C) Itemlarni Orderga qo'shish va Band qilish
        foreach (var item in pickedItems)
        {
          // Narxni hisoblash
          decimal itemTotal = item.Equipment.PricePerDay * (decimal)totalDays;
          calculatedStandardSum += itemTotal;

          // Orderga qo'shish
          orderItems.Add(new RentalOrderItem
          {
            EquipmentItemId = item.Id, // Bu yerda aniq SerialNumber ID si bog'lanadi
            PriceAtMoment= item.Equipment.PricePerDay,
            IsReturned = false
          });

          // Statusni o'zgartirish
          item.Status = EEquipmentItemStatus.Rented;
          // _uow.EquipmentItems.Update(item); // Update qilish shart (agar tracking o'chiq bo'lsa)
        }
      }

      // 3. QOLGAN QISMI BIR XIL (Narx, Order yaratish, Saqlash)

      decimal finalAmount = dto.AgreedTotalAmount ?? calculatedStandardSum;

      var order = new RentalOrder
      {
        CustomerId = dto.CustomerId,
        PaymentMethodId = dto.PaymentMethodId,
        StartDate = dto.StartDate,
        ExpectedReturnDate = dto.ExpectedReturnDate,
        TotalInitialAmount= finalAmount,
        PaidAmount = dto.PaidAmount,
        OrderStatus = EOrderStatus.Active,
        Items = orderItems, // Avtomatik tanlangan itemlar
        CreatedAt = DateTime.UtcNow
      };

      // Rasm bog'lash logikasi... (o'zgarmaydi)
      if (dto.ImageUrls != null)
      {
        foreach (var url in dto.ImageUrls)
        {
          order.Images.Add(new Image { ImageUrl = url, RentalOrder = order });
        }
      }

      await _uow.RentalOrders.AddAsync(order);
      await _uow.CompleteAsync();

      return ResponseDto<RentalOrderResultDto>.Success(new RentalOrderResultDto
      {
        OrderId = order.Id,
        StandardTotal = calculatedStandardSum,
        FinalTotal = finalAmount,
        TotalDays = totalDays,
        Message = $"Buyurtma №{order.Id} qabul qilindi. {orderItems.Count} ta uskuna ombordan chiqarildi."
      });
    }
    catch (Exception ex)
    {
      return ResponseDto<RentalOrderResultDto>.Fail(ex.Message);
    }
  }
  public async Task<PagedResponseDto<IEnumerable<RentalOrderListDto>>> GetPagedOrdersAsync(RentalFilterDto filter)
  {
    try
    {
      // 1. Dinamik so'rov (Query) yaratish
      Expression<Func<RentalOrder, bool>> predicate = order =>
        // a) Status bo'yicha filtr
        (!filter.Status.HasValue || order.OrderStatus== filter.Status) &&

        // b) Sana oralig'i (Masalan: Bugungi buyurtmalar)
        (!filter.FromDate.HasValue || order.StartDate >= filter.FromDate) &&
        (!filter.ToDate.HasValue || order.StartDate <= filter.ToDate) &&

        // c) Qidiruv (ID, Ism yoki Telefon orqali)
        (string.IsNullOrEmpty(filter.Search) ||
         order.Id.ToString().Contains(filter.Search) ||
         order.Customer.FirstName.Contains(filter.Search) ||
         order.Customer.LastName.Contains(filter.Search) ||
         (order.Customer.Phones.Any(p => p.PhoneNumber.Contains(filter.Search))));

      // 2. Umumiy sonini olish (Pagination uchun)
      var totalRecords = ( _uow.RentalOrders.FindAsync(predicate)).Count();

      // 3. Ma'lumotlarni yuklash (Include Customer)
      var orders = await _uow.RentalOrders.GetAllBoxedAsync(
        filter: predicate,
        orderBy: q => q.OrderByDescending(o => o.Id), // Eng yangilari tepada
        includeProperties: "Customer,Customer.Phones"
      );

      // 4. Pagination va Mapping
      var pagedData = orders
        .Skip((filter.Page - 1) * filter.Size)
        .Take(filter.Size)
        .Select(o => new RentalOrderListDto
        {
          Id = o.Id,
          CustomerId = o.CustomerId,
          CustomerName = $"{o.Customer.FirstName} {o.Customer.LastName}",

          // Telefon: Birinchisini olamiz yoki bo'sh qaytaramiz
          CustomerPhone = o.Customer.Phones.FirstOrDefault()?.PhoneNumber ?? "Yo'q",

          // Rasm: Agar mijozda rasm bo'lsa (Customer entityda ImageUrl bo'lsa)
          // CustomerImage = o.Customer.ImageUrl, 

          // Formatlash: 24.01.2026
          BookingDate = o.CreatedAt.ToString("dd.MM.yyyy"),

          // Formatlash: 10:00 - 20:00 (Agar bir kunda bo'lsa) yoki Sanalari bilan
          TimeRange =
            $"{o.StartDate:HH:mm} - {o.ExpectedReturnDate:HH:mm} ({GetDays(o.StartDate, o.ExpectedReturnDate)})",

          Status = o.OrderStatus.ToString(),
          TotalAmount = o.TotalInitialAmount,
          PaidAmount = o.PaidAmount,
          DebtAmount = o.DebtAmount
        });

      return new PagedResponseDto<IEnumerable<RentalOrderListDto>>(pagedData, filter.Page, filter.Size, totalRecords);
    }
    catch (Exception ex)
    {
      return new PagedResponseDto<IEnumerable<RentalOrderListDto>>(null, filter.Page, filter.Size, 0)
      {
        IsSuccess = false,
        Message = ex.Message
      };
    }
  }

  
  public Task<PagedResponseDto<List<RentalOrderDto>>> GetActiveRentalsAsync(string? searchTerm)
  {
    // var query = _uow.Repository<RentalOrder>()
    //   .FindAsync(o => o.OrderStatus == EOrderStatus.Active);

    // // Agar qidiruv so'zi yuborilgan bo'lsa
    // if (!string.IsNullOrWhiteSpace(searchTerm))
    // {
    //   searchTerm = searchTerm.ToLower(); // Kichik harflarga o'tkazamiz

    //   query = query.Where(o =>o.Customer?.FirstName.ToLower().Contains(searchTerm) || o?.Customer?.LastName.Contains(searchTerm));
    // }

    // return await query
    //   .Select(o => new RentalOrderListDto
    //   {
    //     OrderId = o.Id,
    //     CustomerName = o.Customer.FullName,
    //     PhoneNumber = o.Customer.Phone,
    //     RentalDate = o.CreatedAt,
    //     Status = o.Status
    //   })
    //   .ToListAsync();
    throw new NotImplementedException();
  }

  public async Task<ResponseDto<RentalOrderDetailDto>> GetOrderDetailsAsync(int orderId)
  {
    var order = await _uow.Repository<RentalOrder>().GetAllAsync()    
      .Include(o => o.Items)
      .Include(o => o.Customer)
      .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null) return null;

    return ResponseDto<RentalOrderDetailDto>.Success( new RentalOrderDetailDto
    {
      OrderId = order.Id,
      CustomerName = order?.Customer.FirstName + " " + order?.Customer.LastName,
      StartDate = order.StartDate,
      TotalAmount = order.Items.Sum(i => i.PriceAtMoment), // Smena logikasi bu yerga qo'shiladi
      PaidAmount = order.PaidAmount,
      Items = order.Items.Select(i => new RentalItemDto
      {
        ItemId = i.Id,
        EquipmentName = i.EquipmentItem.Equipment.Name,
        Price = i.PriceAtMoment,
        Quantity =1,
        IsReturned = i.IsReturned
      }).ToList()
    },"Buyurtma tafsilotlari yuklandi.");
  }

// Yordamchi metod: Kunlar farqini chiroyli ko'rsatish
  private string GetDays(DateTime start, DateTime end)
  {
    var days = (end - start).Days;
    return days > 0 ? $"+{days} kun" : "Bugun";
  }
  

}

