using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Models;

namespace AppointmentSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Appointment> Appointments { get; set; }
        
        // Veritabanı kurallarını yazılım üzerinden belirliyoruz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // AYNI SAATE YENİ KAYIT EKLENMESİNİ KESİN OLARAK ENGELLİYORUZ
            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.StartDate)
                .IsUnique();
        }
    }
}