namespace Data.Entities;
public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Kategoriya nomi kiritilishi shart")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Image { get; set; }

        [StringLength(1000)]
        public string? Details { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        // --- Foreign Key (Bog'liqlik) ---

        [Required]
        public int BrandId { get; set; } // Brendning Id-si

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; } // Navigatsiya property
    }