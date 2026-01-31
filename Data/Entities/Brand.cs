namespace Data.Entities;
public class Brand : BaseEntity
{
    public string Name { get; set; }
    public string? Image { get; set; }
    public string? Details { get; set; }
    public string CreatedBy { get; set; }
}