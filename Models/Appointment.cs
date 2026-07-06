using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        
        [Required]
        public string CustomerName { get; set; } // Randevuyu alan kişinin adı

        [Required]
        public DateTime StartDate { get; set; } // Randevu başlangıç saati

        [Required]
        public DateTime EndDate { get; set; } // Randevu bitiş saati


        // Eşzamanlılık kontrolü için olması gereken kritik sütun
        // Aynı zamanda iki kişi aynı saati çalıştırırsa sistem çakışmayı buradan buradan fark edecek
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}