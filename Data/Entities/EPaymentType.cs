namespace RentApi.Data.Entities;

public enum PaymentType
{
    PrePayment = 1,   // Oldindan to'lov
    PostPayment = 2,  // Uskunani qaytarishda qilinadigan to'lov
    Deposit = 3       // Zalog (garov) uchun olingan summa
}