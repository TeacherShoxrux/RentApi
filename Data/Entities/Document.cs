namespace RentApi.Data.Entities;

public class Document : BaseEntity
{
    // Hujjat turi (masalan: "Passport", "ID Card", "Haydovchilik guvohnomasi")
    public string DocumentType { get; set; } = string.Empty;

    // Hujjat nomi (masalan: "Fuqarolik pasporti")
    public string Name { get; set; } = string.Empty;

    // Hujjat seriyasi va raqami (masalan: "AA 1234567")
    public string SerialNumber { get; set; } = string.Empty;

    // Jismoniy shaxsning shaxsiy identifikatsiya raqami (14 talik raqam)
    public string JShShR { get; set; } = string.Empty;

    // Hujjatning elektron nusxasi (fayl yo'li)
    public string FilePath { get; set; } = string.Empty;

    public string? Details { get; set; }
    public string? Status { get; set; } // Masalan: "Active", "Expired"

    // --- Tashqi bog'liqlik ---
    
    // Hujjat qaysi mijozga tegishli
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    // public virtual Customer Customer { get; set; } = null!;
}