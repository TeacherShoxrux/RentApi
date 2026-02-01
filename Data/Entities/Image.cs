namespace RentApi.Data.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    
    // Rasm asosiy (main) rasm ekanligini belgilash uchun
    public bool IsMain { get; set; }

    // Tashqi bog'liqlik (Uskuna bilan bog'lash)
    public int EquipmentId { get; set; }
    public string? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }
    // Navigatsiya property
    public virtual Equipment Equipment { get; set; } = null!;
}