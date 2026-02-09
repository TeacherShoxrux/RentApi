using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentApi.Data.Entities;

public class Admin : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public string? Details { get; set; }
    public bool IsActive{ get; set; }=true;
    public EAdminRole Role { get; set; }
    public string? Permission { get; set; }
    [Required]
    [StringLength(64)]
    [Column(TypeName = "char(64)")]
    public string SecurityCode { get; set; } = string.Empty;
    public int? WarehouseId { get; set; }
    public virtual WareHouse? WareHouse { get; set; }
}