using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data;
using AppointmentSystem.Models;

namespace AppointmentSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // Takvim arayüzü için sayfa
        public IActionResult Index()
        {
            return View();
        }

        // FullCalendar'ın verileri çekmek için kullanacağı API uç noktası (GET)
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Select(a => new
                {
                    id = a.Id,
                    title = a.CustomerName + " (Dolu)",
                    start = a.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = a.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    color = "#dc3545" // Dolu saatler için Bootstrap kırmızı rengi
                })
                .ToListAsync();
            
            return Json(appointments);
        }

        // Takvimden yeni randevu eklemek için kullanacağımız uç nokta (Post)
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null || string.IsNullOrEmpty(appointment.CustomerName))
            {
                return BadRequest("Geçersiz randevu bilgisi.");
            }

            // 1. KONTROL: Kaydetmeden önce saat dolu mu diye bak (Yazılımsal Kontrol)
            bool isSlotTaken = await _context.Appointments
                .AnyAsync(a => a.StartDate == appointment.StartDate);
                
            if (isSlotTaken)
            {
                return Conflict("Üzgünüz, bu saat dilimi dolu. Lütfen sayfayı yenileyin.");
            }

            try
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Randevu başarıyla oluşturuldu!" });
            }
            catch (DbUpdateException) // 2. KONTROL: Gerçek Eşzamanlılık (Milisaniyelik Çakışma) Kontrolü
            {
                // Eğer iki kişi 1. kontrolü aynı milisaniyede geçerse, veritabanı Unique Constraint sayesinde patlar ve buraya düşer.
                return Conflict("Üzgünüz, bu saat dilimi tam şu an başka biri tarafından rezerve edildi!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Bir hata oluştu: " + ex.Message);
            }
        }
    }
}