using Microsoft.EntityFrameworkCore;
using RentApi.Data.Entities;

namespace RentApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // --- 1. Katalog (Catalog) ---
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Image> Images { get; set; }

    // --- 2. Inventar (Inventory) ---
    public DbSet<EquipmentItem> EquipmentItems { get; set; }
    public DbSet<Status> Statuses { get; set; }

    // --- 3. Mijozlar (Customers) ---
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Document> Documents { get; set; }

    // --- 4. Tranzaksiya va Buyurtmalar (Transactions) ---
    public DbSet<RentalOrder> RentalOrders { get; set; }
    public DbSet<RentalOrderItem> RentalOrderItems { get; set; } // Biz qo'shgan ko'prik jadval
    public DbSet<RentPhoto> RentPhotos { get; set; }

    // --- 5. Moliya (Finance) ---
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    // --- 6. Boshqaruv (Admin) ---
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- A. PULLIK MAYDONLAR (DECIMAL PRECISION) ---
        // SQL Serverda decimal tiplar aniqlik talab qiladi (18 ta raqam, shundan 2 tasi tiyin)
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        // --- B. BOG'LIQLIKLAR VA O'CHIRISH QOIDALARI (RELATIONSHIPS) ---

        // 1. Brand va Category o'chirilganda, Uskunalar o'chib ketmasin (Xavfsizlik)
        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.Brand)
            .WithMany(b => b.Equipments) // Brand modelida ICollection<Equipment> bo'lishi kerak
            .HasForeignKey(e => e.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Equipments)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. RentalOrder va Items (Ko'prik jadval)
        modelBuilder.Entity<RentalOrderItem>()
            .HasOne(roi => roi.RentalOrder)
            .WithMany(ro => ro.Items)
            .HasForeignKey(roi => roi.RentalOrderId)
            .OnDelete(DeleteBehavior.Cascade); // Buyurtma o'chsa, ichidagi qatorlar ham o'chadi

        // 3. EquipmentItem va RentalHistory (ENG MUHIMI)
        // Uskuna o'chirilganda, tarix (RentalOrderItem) o'chib ketmasligi shart!
        modelBuilder.Entity<RentalOrderItem>()
            .HasOne(roi => roi.EquipmentItem)
            .WithMany(ei => ei.RentalHistory)
            .HasForeignKey(roi => roi.EquipmentItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // 4. Mijoz va Buyurtmalar
        // Mijoz o'chsa, uning qarzlari va buyurtmalari o'chib ketmasin
        modelBuilder.Entity<RentalOrder>()
            .HasOne(ro => ro.Customer)
            .WithMany(c => c.RentalOrders)
            .HasForeignKey(ro => ro.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 5. To'lovlar
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.RentalOrder)
            .WithMany(ro => ro.Payments)
            .HasForeignKey(p => p.RentalOrderId)
            .OnDelete(DeleteBehavior.Cascade); // Buyurtma o'chsa, to'lovlar tarixi o'chishiga ruxsat berilishi mumkin (yoki Restrict qilsa ham bo'ladi)

        // --- C. UNIQUE INDEXES (Takrorlanmaslik) ---
        modelBuilder.Entity<EquipmentItem>()
            .HasIndex(ei => ei.SerialNumber)
            .IsUnique(); // Bir xil seriya raqamli uskuna bo'lmasligi kerak

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.JShShIR)
            .IsUnique(); // Bir xil PINFL li odam ikki marta kiritilmasin
            
        modelBuilder.Entity<PaymentMethod>()
            .HasIndex(p => p.Code)
            .IsUnique();
    }

    // --- D. AUDIT (Created/Updated Time) ---
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && 
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}