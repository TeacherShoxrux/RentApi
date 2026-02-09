namespace Application.DTOs.Employee;
public class RoleLookupDto
{
    public int Id { get; set; }
    public string Name { get; set; } // Masalan: Admin, Manager, Operator
}

// Yoki oddiy Enum ko'rinishida:
public enum UserRole
{
    Admin = 1,
    Manager = 2,
    Employee = 3
}