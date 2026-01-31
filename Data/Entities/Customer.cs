namespace RentApi.Data.Entities;

public class Customer : BaseEntity
{
    // Shaxsiy ma'lumotlar
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string JShShIR { get; set; } = string.Empty; // PINFL
    public string? Gender { get; set; }
    public DateTime Birthday { get; set; }
    
    // Hujjat holati
    public bool HasPassport { get; set; }
    public string? PassportStatus { get; set; } // XML: PassportStatuse
    public string? UserPhoto { get; set; }      // XML: UserPhote

    // Qo'shimcha ma'lumotlar
    public string? InvitedBy { get; set; }
    public string? Details { get; set; }
    public string? CreatedByDetail { get; set; }

    // --- Navigatsiya propertylari (Relationships) ---

    // Bir mijozda bir nechta telefon raqami va hujjat bo'lishi mumkin
    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    
    // Mijozning barcha ijara buyurtmalari
    public virtual ICollection<RentalOrder> RentalOrders { get; set; } = new List<RentalOrder>();
}