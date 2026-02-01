namespace RentApi.Data.Entities;
public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string? Image { get; set; }
    public string? Details { get; set; }
    public string? AdminId { get; set; }
    public virtual Admin? Admin { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
}