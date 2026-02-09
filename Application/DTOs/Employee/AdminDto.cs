namespace Application.DTOs.Employee;
public class AdminDto
{
    public int Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Pinfl { get; set; }
    
    public string? RoleName { get; set; } // Ro'lning nomi (masalan: "Admin")
    
    public string? PhoneNumber { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public bool IsActive { get; set; } // Xodim hozir ishlayaptimi yoki yo'q
}