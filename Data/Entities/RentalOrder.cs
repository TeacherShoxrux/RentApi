namespace RentApi.Data.Entities;

public class RentalOrder : BaseEntity
{
    // Vaqt va muddat
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int TotalDays { get; set; }

    // Moliyaviy qism
    public decimal DailyPrice { get; set; }
    public decimal TotalInitialAmount { get; set; } // Jami dastlabki summa
    public decimal PaidAmount { get; set; }         // To'langan summa
    public decimal DebtAmount { get; set; }         // Qarz summasi

    // Holat
    public string OrderStatus { get; set; } = "Pending";

    // --- Tashqi bog'liqliklar (Foreign Keys) ---
    public bool IsManuallyEdited { get; set; } = false;
    public DateTime ExpectedReturnDate { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer{ get; set; } = null!;
    
    public int WareHouseId { get; set; }
    public virtual WareHouse WareHouse{ get; set; } = null!;
    
    // Xodimlar (Staff va Adminlar odatda User jadvaliga bog'lanadi)
    public int AdminId { get; set; }
    public virtual Admin Admin { get; set; } = null!;
    public double TotalShifts { get; set; }
    // --- Navigatsiya propertylari ---
    // public virtual Customer Customer { get; set; } = null!;
    // public virtual Warehouse Warehouse { get; set; } = null!;
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<RentalOrderItem> Items { get; set; } = new List<RentalOrderItem>();
  
}