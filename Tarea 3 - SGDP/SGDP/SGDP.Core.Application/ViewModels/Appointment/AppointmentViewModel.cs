

namespace SGDP.Core.Application.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string Patiente { get; set; }
        public string LastNamePatient { get; set; }
        public string Medico { get; set; }
        public string LastNameMedico { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string why { get; set; }
        public int? clave { get; set; }
    }
}
