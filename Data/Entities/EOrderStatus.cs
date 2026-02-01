namespace RentApi.Data.Entities;

public enum EOrderStatus
{
    Reserved = 1,
    Active = 2,             // Ijara jarayoni ketmoqda
    PartiallyReturned = 3,  // Uskunalarning bir qismi qaytarilgan
    Completed = 4,          // Barcha uskunalar qaytarilgan va hisob-kitob yopilgan
    Canceled = 5            // Buyurtma bekor qilingan
}