namespace RentApi.Data.Entities;

public enum EOrderStatus
{
    Active = 1,             // Ijara jarayoni ketmoqda
    PartiallyReturned = 2,  // Uskunalarning bir qismi qaytarilgan
    Completed = 3,          // Barcha uskunalar qaytarilgan va hisob-kitob yopilgan
    Canceled = 4            // Buyurtma bekor qilingan
}