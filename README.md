# 📦 Rental & Booking System (RentApi)

![Project Banner](https://via.placeholder.com/1200x300?text=Rental+%26+Booking+System+Banner)
<div align="center">

  [![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
  [![Flutter](https://img.shields.io/badge/Flutter-3.0-blue?style=for-the-badge&logo=flutter)](https://flutter.dev/)
  [![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
  [![Dart](https://img.shields.io/badge/Dart-3.0-0175C2?style=for-the-badge&logo=dart)](https://dart.dev/)
  [![SQLite](https://img.shields.io/badge/SQLite-Data-003B57?style=for-the-badge&logo=sqlite)](https://www.sqlite.org/)

</div>

<p align="center">
  <strong>Zamonaviy texnika ijarasi va ombor nazorati tizimi.</strong><br>
  Clean Architecture tamoyillari asosida qurilgan, yuqori tezlik va xavfsizlikni ta'minlovchi to'liq yechim.
</p>

---

## 📖 Loyiha Haqida

Ushbu loyiha ijara biznesini (texnika, uskuna, qurilmalar) avtomatlashtirish uchun ishlab chiqilgan. Tizim mijozlar bazasini yuritish, ombordagi qoldiqni real vaqtda nazorat qilish, ijara muddatlarini hisoblash va moliyaviy hisobotlarni shakllantirish imkonini beradi.

### 🔥 Asosiy Imkoniyatlar

* **🛠️ Texnika Katalogi:** Brendlar, Kategoriyalar va Modellarga ajratilgan qulay katalog.
* **📦 Ombor Nazorati (Inventory):** Har bir buyumning (Serial Number) holatini kuzatish (Available, Rented, Maintenance).
* **🤝 Ijara Rasmiylashtirish (Order Processing):**
    * Soatbay va kunlik hisob-kitob.
    * **Auto-Pick:** Kerakli sonni kiritganda tizim o'zi bo'sh turgan buyumlarni tanlab beradi.
    * **Custom Pricing:** Admin tomonidan kelishilgan narx belgilash imkoniyati.
* **🔄 Qisman Qaytarish (Partial Return):** Ijaraga olingan narsalarning bir qismini qaytarish va hisobni qayta kitob qilish.
* **👥 Mijozlar Bazasi (CRM):** Passport ma'lumotlari, rasm yuklash va qora ro'yxat nazorati.
* **📸 Rasm Yuklash:** Mahsulot va ijara holatini (Pre-upload pattern orqali) rasmga olib saqlash.
* **📊 Hisobotlar:** Qarzdorlar, kunlik tushum va o'z vaqtida qaytmagan buyumlar ro'yxati.

---

## 📸 Skrinshotlar

| Dashboard | Order Yaratish |
|:---:|:---:|
| <img src="URL_TO_YOUR_DASHBOARD_IMAGE" width="400"> | <img src="URL_TO_YOUR_ORDER_IMAGE" width="400"> |

| Mijozlar Ro'yxati | Mahsulotlar |
|:---:|:---:|
| <img src="URL_TO_YOUR_CUSTOMER_IMAGE" width="400"> | <img src="URL_TO_YOUR_PRODUCT_IMAGE" width="400"> |

---

## 🏗️ Texnologik Stek va Arxitektura

Loyiha **Clean Architecture** qoidalariga qat'iy rioya qilgan holda qurilgan:

### Backend (.NET 8 Web API)
* **Core (Domain):** Hech qanday tashqi kutubxonaga bog'liq bo'lmagan sof C# classlar (Entities, Enums, Interfaces).
* **Application:** Business logika, DTOs, Service Interfaces, Mapping.
* **Infrastructure:** Ma'lumotlar bazasi (EF Core), Fayl tizimi, Tashqi xizmatlar.
* **API:** Controllerlar va Middleware.

**Patternlar:**
* Repository & Unit of Work (Generic Implementation)
* Dependency Injection (DI)
* DTO Pattern (Data Transfer Object)

### Frontend (Flutter)
* **State Management:** Riverpod
* **Network:** Dio (Interceptorlar bilan)
* **UI:** Material Design 3, Responsive Layout

---

## 🚀 O'rnatish va Ishga Tushirish

Loyihani o'z kompyuteringizda ishga tushirish uchun quyidagi qadamlarni bajaring:

### 1. Talablar
* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Flutter SDK](https://flutter.dev/docs/get-started/install)
* Visual Studio Code yoki Visual Studio 2022

### 2. Backendni sozlash

```bash
# Repozitoriyni klonlash
git clone [https://github.com/username/RentApi.git](https://github.com/username/RentApi.git)

# Papkaga kirish
cd RentApi

# Paketlarni tiklash
dotnet restore

# Ma'lumotlar bazasini yaratish (SQLite)
dotnet ef database update

# Loyihani ishga tushirish
dotnet run
