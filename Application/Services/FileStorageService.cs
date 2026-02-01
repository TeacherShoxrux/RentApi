using RentApi.Application.Services.Interfaceses;

namespace RentApi.Application.Services;

public class FileStorageService : IFileStorageService
{
  private readonly IWebHostEnvironment _env;

  public FileStorageService(IWebHostEnvironment env)
  {
    _env = env;
  }

  public async Task<string> SaveFileAsync(IFormFile? file, string subFolder)
  {
    if (file == null || file.Length == 0) return string.Empty;

    // 1. Asosiy yo'lni aniqlash
    string rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    string finalDirectory = Path.Combine(rootPath, "uploads", subFolder);

    // 2. Papka bo'lmasa yaratish
    if (!Directory.Exists(finalDirectory))
      Directory.CreateDirectory(finalDirectory);

    // 3. Unikal nom yaratish (Overwrite oldini olish)
    string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
    string fullPath = Path.Combine(finalDirectory, fileName);

    // 4. Faylni saqlash
    using (var stream = new FileStream(fullPath, FileMode.Create))
    {
      await file.CopyToAsync(stream);
    }

    // 5. Bazada saqlash uchun nisbiy yo'lni qaytarish
    return $"/uploads/{subFolder}/{fileName}";
  }

  public void DeleteFile(string relativePath)
  {
    if (string.IsNullOrEmpty(relativePath)) return;

    string rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    string fullPath = Path.Combine(rootPath, relativePath.TrimStart('/'));

    if (File.Exists(fullPath))
      File.Delete(fullPath);
  }
}