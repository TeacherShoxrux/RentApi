namespace RentApi.Data.Entities;

public class Equipment : BaseEntity
{
    // Asosiy ma'lumotlar
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? Type { get; set; }
    public string? Details { get; set; }

    // Narxlar
    public decimal PricePerDay { get; set; }
    public decimal ReplacementValue { get; set; }

    // Audit
    public string CreatedBy { get; set; } = string.Empty;

    // Bog'liqliklar (Foreign Keys)
   
    public int CategoryId { get; set; }
    public int WareHouseId { get; set; }
    public virtual WareHouse WareHouse { get; set; } = null!;
    public int? CustomerId { get; set; } // Hozirda kimdadir bo'lsa
    public virtual Customer? Customer { get; set; }
    public bool IsAvailable { get; set; } = true;

    public virtual ICollection<EquipmentItem> Items { get; set; } = null!;

    // Navigatsiya propertylari (EF Core uchun)
    public int BrandId { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}