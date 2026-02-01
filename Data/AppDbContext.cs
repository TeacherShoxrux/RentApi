using Microsoft.EntityFrameworkCore;
using RentApi.Data.Entities;

namespace RentApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // --- 1. Katalog ---
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Image> Images { get; set; }

    // --- 2. Inventar ---
    public DbSet<EquipmentItem> EquipmentItems { get; set; }

    // --- 3. Mijozlar ---
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Document> Documents { get; set; }

    // --- 4. Tranzaksiya ---
    public DbSet<RentalOrder> RentalOrders { get; set; }
    public DbSet<RentalOrderItem> RentalOrderItems { get; set; }
    public DbSet<RentPhoto> RentPhotos { get; set; }
    public DbSet<OrderExtension> OrderExtensions { get; set; }
    // --- 5. Moliya ---
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    // --- 6. Admin ---
    public DbSet<Admin> Admins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- A. DECIMAL SOZLAMALARI ---
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        // =========================================================
        //                 BOG'LIQLIKLAR (RELATIONSHIPS)
        // =========================================================

        // 1. Brand/Category -> Equipment (HIMOYA)
        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.Brand)
            .WithMany(b => b.Equipments)
            .HasForeignKey(e => e.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.Category)
            .WithMany(c => c.Equipments)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. Equipment -> EquipmentItem (YANGI HIMOYA)
        // Katalogdan "Drel" o'chsa, ombordagi "Drel SN-001" o'chib ketmasin.
        modelBuilder.Entity<EquipmentItem>()
            .HasOne(ei => ei.Equipment)
            .WithMany(e => e.Items)
            .HasForeignKey(ei => ei.EquipmentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // 3. Status -> EquipmentItem (YANGI HIMOYA - Lookup Table)
        // "Available" statusi o'chsa, uskunalar buzilmasin.
        modelBuilder.Entity<EquipmentItem>()
                .Property(e => e.Status)
                .HasConversion<string>();
        
        modelBuilder.Entity<OrderExtension>()
            .HasOne(oe => oe.RentalOrder)
            .WithMany() // Buyurtma ichida Extensions ro'yxati bo'lmasa ham bo'ladi
            .HasForeignKey(oe => oe.RentalOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        // 4. Customer -> Phone/Document (TOZALASH)
        // Mijoz o'chsa, uning telefoni va hujjati bazadan o'chib ketsin (Cascade).
        modelBuilder.Entity<Phone>()
            .HasOne<Customer>() // Customer navigation property bo'lmasa ham ishlaydi
            .WithMany(c => c.Phones)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Document>()
            .HasOne<Customer>()
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // 5. Customer -> RentalOrder (HIMOYA)
        // Qarzi bor yoki tarixi bor mijozni o'chirish mumkin emas.
        modelBuilder.Entity<RentalOrder>()
            .HasOne(ro => ro.Customer)
            .WithMany(c => c.RentalOrders)
            .HasForeignKey(ro => ro.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // 6. RentalOrder -> RentalOrderItem (ORDER ICHIDAGI NARSA)
        // Buyurtma o'chsa, ichidagi ro'yxat o'chishi kerak.
        modelBuilder.Entity<RentalOrderItem>()
            .HasOne(roi => roi.RentalOrder)
            .WithMany(ro => ro.Items)
            .HasForeignKey(roi => roi.RentalOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // 7. EquipmentItem -> RentalOrderItem (TARIXNI SAQLASH - ENG MUHIMI)
        // Uskuna o'chsa, tarix o'chmasin.
        modelBuilder.Entity<RentalOrderItem>()
            .HasOne(roi => roi.EquipmentItem)
            .WithMany(ei => ei.RentalHistory)
            .HasForeignKey(roi => roi.EquipmentItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // 8. PaymentMethod -> Payment (YANGI HIMOYA - Lookup Table)
        // "Naqd" o'chsa, to'lovlar o'chmasin.
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.PaymentMethod)
            .WithMany() // PaymentMethod ichida Collection bo'lmasa bo'sh qoldiramiz
            .HasForeignKey(p => p.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        // 9. RentalOrder -> Payment (MOLIYAVIY XAVFSIZLIK - O'ZGARTIRILDI)
        // Tavsiya: Agar to'lov bo'lgan bo'lsa, buyurtmani o'chirishni taqiqlash (Restrict).
        // Agar Cascade qilsangiz, kassir buyurtmani o'chirsangiz, pul ham "g'oyib" bo'ladi.
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.RentalOrder)
            .WithMany(ro => ro.Payments)
            .HasForeignKey(p => p.RentalOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EquipmentItem>()
            .HasOne(ei => ei.WareHouse)
            .WithMany(w => w.Items)
            .HasForeignKey(ei => ei.WareHouseId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. Warehouse -> Admin (Ombor yopilsa, xodimlar ko'chada qolmasin)
        modelBuilder.Entity<Admin>()
            .HasOne(a => a.WareHouse)
            .WithMany(w => w.Admins)
            .HasForeignKey(a => a.WarehouseId)
            .OnDelete(DeleteBehavior.SetNull); // Ombor o'chsa, xodim "Omborsiz" holatga tushadi

        // 3. Warehouse -> Orders (Hisobot buzilmasligi uchun)
        modelBuilder.Entity<RentalOrder>()
            .HasOne(ro => ro.WareHouse)
            .WithMany(w => w.Orders)
            .HasForeignKey(ro => ro.WareHouseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EquipmentItem>()
            .HasIndex(ei => ei.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.JShShIR)
            .IsUnique();
        modelBuilder.Entity<Admin>()
            .Property(a => a.Role)
            .HasConversion<string>();
        modelBuilder.Entity<PaymentMethod>()
            .HasIndex(p => p.Code)
            .IsUnique();

        modelBuilder.Entity<Brand>()
            .HasIndex(b => b.Name)
            .IsUnique(); // Bir xil nomli brend bo'lmasin
    }

    // --- AUDIT ---
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