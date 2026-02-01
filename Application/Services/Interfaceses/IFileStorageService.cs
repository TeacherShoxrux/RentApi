namespace RentApi.Application.Services.Interfaceses;

public interface IFileStorageService
{
  // Faylni saqlash va uning nisbiy yo'lini (URL) qaytarish
  Task<string> SaveFileAsync(IFormFile file, string subFolder);

  // Faylni o'chirish
  void DeleteFile(string filePath);
}