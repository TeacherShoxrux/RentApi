namespace RentApi.Data.Entities;
public class WareHouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Manager { get; set; }
    public string? Logo { get; set; }
    public string CreatedBy { get; set; } = string.Empty;

}