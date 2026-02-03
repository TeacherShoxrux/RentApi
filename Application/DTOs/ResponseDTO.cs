using Application.DTOs.RentalOrder;

namespace RentApi.Application.DTOs;

public class ResponseDto<T>
{
  // Amaliyot muvaffaqiyatli bo'ldimi?
  public bool IsSuccess { get; set; } = true;

  // Asosiy ma'lumot (Mijoz, Order, yoki Ro'yxat)
  public T? Data { get; set; }

  // Xatolik xabari (agar bo'lsa)
  public string Message { get; set; } = string.Empty;

  // HTTP Status kodi (ixtiyoriy, lekin foydali)
  public int StatusCode { get; set; } = 200;

  // --- YORDAMCHI METODLAR ---

  public static ResponseDto<T> Success(T data, string message = "Success") =>
    new() { IsSuccess = true, Data = data, Message = message, StatusCode = 200 };

  public static ResponseDto<T> Fail(string message, int statusCode = 400) =>
    new() { IsSuccess = false, Message = message, StatusCode = statusCode };
}
public class PagedResponseDto<T> : ResponseDto<T>
{
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
  public int TotalPages { get; set; }
  public int TotalRecords { get; set; }

  public PagedResponseDto(T data, int pageNumber, int pageSize, int totalRecords)
  {
    Data = data;
    PageNumber = pageNumber;
    PageSize = pageSize;
    TotalRecords = totalRecords;
    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    IsSuccess = true;
    StatusCode = 200;
  }
}