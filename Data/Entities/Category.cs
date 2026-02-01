using System.ComponentModel.DataAnnotations;

namespace RentApi.Data.Entities;
public class Category : BaseEntity
    {

        public string Name { get; set; }= string.Empty;

        [StringLength(500)]
        public string? Image { get; set; }

        [StringLength(1000)]
        public string? Details { get; set; }

        public string? AdminId { get; set; }
        public virtual Admin? Admin { get; set; }

        // --- Foreign Key (Bog'liqlik) ---

        public int? BrandId { get; set; } // Brendning Id-si
        public virtual Brand? Brand { get; set; } // Navigatsiya property
        public virtual ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();

    }