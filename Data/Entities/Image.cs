namespace RentApi.Data.Entities;

public class Image : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public int? RentalOrderId { get; set; }
    public virtual RentalOrder? RentalOrder { get; set; }
    public int? AdminId { get; set; }=1;
    public virtual Admin? Admin { get; set; }
}