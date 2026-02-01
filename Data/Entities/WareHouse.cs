namespace RentApi.Data.Entities;
public class WareHouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Manager { get; set; }
    public string? Logo { get; set; }
    public string Address { get; set; } = string.Empty; // Manzil
    public string? Phone { get; set; } // Filial telefoni

    // --- Bog'liqliklar ---

    // Bu omborda ishlaydigan xodimlar
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    // Bu omborda turgan tovarlar
    public virtual ICollection<EquipmentItem> Items { get; set; } = new List<EquipmentItem>();

    // Bu omborda qilingan savdolar
    public virtual ICollection<RentalOrder> Orders { get; set; } = new List<RentalOrder>();

}