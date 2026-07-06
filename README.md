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
* ASP.NET Core 8.0 (MVC Mimarisi)
* Entity Framework Core (Code-First Yaklaşımı)
* SQLite (Hafif ve taşınabilir yerel veritabanı)

**Frontend:**
* HTML5, CSS3
* JavaScript (ES6+ & Fetch API)
* [FullCalendar.js](https://fullcalendar.io/) (Görsel Takvim Kütüphanesi)
