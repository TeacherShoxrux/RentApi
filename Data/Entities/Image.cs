namespace RentApi.Data.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    
    // Rasm asosiy (main) rasm ekanligini belgilash uchun
    public bool IsMain { get; set; }

    // Tashqi bog'liqlik (Uskuna bilan bog'lash)
    public int? RentalOrderId { get; set; }
    public virtual RentalOrder? RentalOrder { get; set; }
    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }
}