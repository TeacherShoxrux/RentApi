namespace Application.DTOs.Employee;
public class AdminCreateDto
{
    public string FullName { get; set; } // Ism Familiya
    
    public string Pinfl { get; set; } // JSHSHIR (14 talik raqam)
    
    public DateTime BirthDate { get; set; } // Tug'ilgan kun
    
    public int RoleId { get; set; } // Tanlangan ro'lning IDsi
    
    public string PhoneNumber { get; set; } // Telefon raqam
    
    public string Password { get; set; } // Parol
}