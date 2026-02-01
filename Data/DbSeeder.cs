using Microsoft.EntityFrameworkCore;
using RentApi.Data.Entities;
using RentApi.Extensions; // ToSha256() ishlashi uchun

namespace RentApi.Data;

public static class DbSeeder
{
  public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
  {
    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
    {
      var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

      // Agar baza yangi bo'lsa, uni yaratib oladi
      await context.Database.MigrateAsync();

      // 1. OMBOR (WAREHOUSE)
      if (!await context.WareHouses.AnyAsync())
      {
        var warehouse = new WareHouse
        {
          Name = "Bosh Filial (Chilonzor)",
          Address = "Toshkent sh, Chilonzor 1-kvartal",
          Phone = "+998901234567",
          CreatedAt = DateTime.UtcNow
        };
        await context.WareHouses.AddAsync(warehouse);
        await context.SaveChangesAsync();
      }

      // 2. ADMIN (SUPER ADMIN)
      if (!await context.Admins.AnyAsync())
      {
        // Bosh omborni olamiz
        var mainWarehouse = await context.WareHouses.FirstAsync();

        var admin = new Admin
        {
          Name = "Super",
          LastName = "Admin",
          Role = AdminRole.SuperAdmin, // Enum
          DateOfBirth = new DateTime(1995, 1, 1),

          // MUHIM: "1111" ni hashlab yozamiz
          SecurityCode = "1111".ToSha256(),

          WarehouseId = mainWarehouse.Id,
          IsActive = true,
          CreatedAt = DateTime.UtcNow
        };

        await context.Admins.AddAsync(admin);
        await context.SaveChangesAsync();
      }

      // 3. KATEGORIYA VA BRENDLAR
      if (!await context.Categories.AnyAsync())
      {
        // Brendlar
        var bosch = new Brand { Name = "Bosch" };
        var makita = new Brand { Name = "Makita" };
        var total = new Brand { Name = "Total" };

        await context.Brands.AddRangeAsync(bosch, makita, total);
        await context.SaveChangesAsync();

        // Kategoriyalar
        var category1 = new Category { Name = "Drellar" };
        var category2 = new Category { Name = "Perforatorlar" };
        var category3 = new Category { Name = "Payvandlash (Svarka)" };

        await context.Categories.AddRangeAsync(category1, category2, category3);
        await context.SaveChangesAsync();

        // 4. USKUNALAR (KATALOG)
        if (!await context.Equipments.AnyAsync())
        {
          var drel = new Equipment
          {
            Name = "Bosch GSB 13 RE",
            Details = "Professional zarbali drel",
            PricePerDay = 50000, // 50 ming so'm
            //  = 300000,
            BrandId = bosch.Id,
            CategoryId = category1.Id,
            CreatedAt = DateTime.UtcNow
          };

          var perforator = new Equipment
          {
            Name = "Makita HR2470",
            Details= "Universal perforator",
            PricePerDay = 80000,
            // DepositAmount = 500000,
            BrandId = makita.Id,
            CategoryId = category2.Id,
            CreatedAt = DateTime.UtcNow
          };

          await context.Equipments.AddRangeAsync(drel, perforator);
          await context.SaveChangesAsync();

          // 5. FIZIK BUYUMLAR (ITEM)
          var mainWarehouse = await context.WareHouses.FirstAsync();

          var item1 = new EquipmentItem
          {
            EquipmentId = drel.Id,
            WareHouseId = mainWarehouse.Id,
            SerialNumber = "SN-1001",
            Status = EEquipmentItemStatus.Available, // Enum
            Condition = "Yangi",
            CreatedAt = DateTime.UtcNow
          };

          var item2 = new EquipmentItem
          {
            EquipmentId = drel.Id,
            WareHouseId = mainWarehouse.Id,
            SerialNumber = "SN-1002",
            Status = EEquipmentItemStatus.Available,
            Condition = "Yaxshi",
            CreatedAt = DateTime.UtcNow
          };

          var item3 = new EquipmentItem
          {
            EquipmentId = perforator.Id,
            WareHouseId = mainWarehouse.Id,
            SerialNumber = "MK-555",
            Status = EEquipmentItemStatus.Available,
            Condition = "Yangi",
            CreatedAt = DateTime.UtcNow
          };

          await context.EquipmentItems.AddRangeAsync(item1, item2, item3);
          await context.SaveChangesAsync();
        }
      }
    }
  }
}