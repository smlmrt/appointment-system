# 📅 İnteraktif Randevu ve Rezervasyon Sistemi (Interactive Appointment System)

ASP.NET Core MVC kullanılarak geliştirilmiş, **FullCalendar.js** entegreli ve eşzamanlılık (concurrency) çakışmalarına karşı tam korumalı, işletmeler (klinik, kuaför, spor tesisi vb.) için tasarlanmış modern bir randevu sistemidir.

Bu proje, standart bir CRUD uygulamasının ötesine geçerek JavaScript ile C# backend'ini asenkron (Fetch API) olarak konuşturur ve veritabanı seviyesinde "Race Condition" (Eşzamanlılık Çakışması) problemlerini çözer.

## 🚀 Öne Çıkan Özellikler

* **Dinamik Takvim Arayüzü:** FullCalendar.js kullanılarak entegre edilmiş, sürükle-bırak hissiyatı veren tam interaktif görsel takvim.
* **Eşzamanlılık (Concurrency) Kontrolü:** İki kullanıcının aynı milisaniyede aynı boş saati almaya çalışması durumunda, sistem çakışmayı veritabanı seviyesinde (Unique Constraint ve DbUpdateException) yakalar ve ikinci kullanıcıya "Bu saat az önce doldu" uyarısı verir.
* **Tam Esnek Zaman Seçimi:** Kullanıcı takvime tıkladığında saatler otomatik hesaplanır, ancak kısıtlanmaz. HTML5 `datetime-local` sayesinde randevu saatleri kullanıcı tarafından dakikasına kadar düzenlenebilir.
* **Asenkron Veri İletişimi:** Sayfa yenilenmeden arka planda C# Controller'ına JSON formatında veri gönderilir ve hata/başarı durumlarına göre takvim anında kendini günceller.
* **Mobil Uyumlu Tasarım:** Bootstrap 5 Modal yapısı ile temiz, sade ve kullanıcı dostu (UX) arayüz.

## 🛠️ Kullanılan Teknolojiler

**Backend:**
* C# 
* ASP.NET Core (.NET 10, MVC Mimarisi)
* Entity Framework Core (Code-First Yaklaşımı)
* SQLite (Hafif ve taşınabilir yerel veritabanı)

**Frontend:**
* HTML5, CSS3
* JavaScript (ES6+ & Fetch API)
* [FullCalendar.js](https://fullcalendar.io/) (Görsel Takvim Kütüphanesi)

## 🔌 Uç Noktalar

| Metot | Adres | Açıklama |
|-------|-------|----------|
| GET | `/Appointments` | Takvim sayfası |
| GET | `/Appointments/GetAppointments` | Takvim için randevuları JSON olarak döner |
| POST | `/Appointments/CreateAppointment` | Yeni randevu oluşturur (JSON body) |

Örnek istek:

```json
POST /Appointments/CreateAppointment
{
  "customerName": "Ahmet Yılmaz",
  "startDate": "2026-09-20T10:00:00",
  "endDate": "2026-09-20T11:00:00"
}
```

Aynı başlangıç saatine ikinci bir kayıt denendiğinde `409 Conflict` döner.

## 💻 Projeyi Kendi Bilgisayarında Çalıştırma

```bash
git clone https://github.com/smlmrt/appointment-system.git
cd appointment-system
dotnet restore
dotnet ef database update
dotnet run
```

Uygulama `http://localhost:5238` adresinde açılır. Takvim sayfası: `http://localhost:5238/Appointments` (çalışma saatleri 09:00–18:00, 1 saatlik bloklar).

## 📂 Proje Yapısı

```
AppointmentSystem/
├── Controllers/
│   ├── AppointmentsController.cs
│   └── HomeController.cs
├── Data/AppDbContext.cs             # StartDate üzerinde unique index
├── Models/Appointment.cs            # RowVersion ([Timestamp]) alanı
├── Views/Appointments/Index.cshtml  # FullCalendar arayüzü
├── Migrations/
└── Program.cs
```
