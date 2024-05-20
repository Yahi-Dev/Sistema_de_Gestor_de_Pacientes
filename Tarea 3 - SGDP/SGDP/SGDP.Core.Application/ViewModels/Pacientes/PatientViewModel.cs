

namespace SGDP.Core.Application.ViewModels.Pacientes
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? ImagePathMedico { get; set; }
        public string Phone { get; set; }
        public string Cedula { get; set; }
        public string Sex { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}
