namespace RentApi.Data.Entities;

public class Document : BaseEntity
{
    // Hujjat turi (Passport, ID Card) - UI dagi "Passport turi" dropdownidan keladi
    public string DocumentType { get; set; } = "Passport";

    // Hujjat nomi 
    public string Name { get; set; } = string.Empty;

    // Seriya va Raqam (UI da alohida, bu yerda bitta qilib saqlash tavsiya qilinadi: "AA 1234567")
    public string SerialNumber { get; set; } = string.Empty;

    // JSHSHIR (PINFL)
    public string JShShR { get; set; } = string.Empty; // Sizning yozilishingiz bo'yicha

    // Scan varianti (Rasm yo'li)
    public string FilePath { get; set; } = string.Empty;

    public string? Details { get; set; }

    // Status: "Active", "Expired", "InSafe" (Seyfda), "Returned" (Qaytarildi)
    public string? Status { get; set; }

    // --- YANGI QO'SHILGAN QISM (ZALOG UCHUN) ---

    // Hujjatning originali bizda qoldimi? (Checkbox uchun)
    public bool IsOriginalLeft { get; set; } = false;

    // Qachon tashlab ketdi?
    public DateTime? LeftAt { get; set; }

    // Qachon qaytarib oldi?
    public DateTime? ReturnedAt { get; set; }

    // --- Bog'liqlik ---
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
}