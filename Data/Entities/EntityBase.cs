namespace Namespace.Data.Entities
public abstract class BaseEntity
{
    // Primary Key
    public int Id { get; set; }

    // Audit ustunlari (ma'lumot qachon yaratilgan va o'zgartirilganligini bilish uchun)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Soft Delete (ma'lumotni bazadan butunlay o'chirib tashlamaslik uchun)
    public bool IsDeleted { get; set; } = false;
}