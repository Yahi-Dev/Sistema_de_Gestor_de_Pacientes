using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SGDP.Core.Application.ViewModels.Appointment
{
    public class SaveAppointmentViewModel
    {
        public int Id { get; set; }


        public string? PatienteName { get; set; }
        public string? PatienteLastName { get; set; }

        public int? patientId { get; set; }
        public string? CedulaPatient { get; set; }



        [Required(ErrorMessage = "Debe asignar un doctor")]
        [DataType(DataType.Text)]
        public string? Doctor { get; set; }




        [Required(ErrorMessage = "Debe colocar una fecha valida")]
        [DataType(DataType.Text)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "Debe colocar una hora valida")]
        [DataType(DataType.Text)]
        public TimeSpan Time { get; set; }



        [Required(ErrorMessage = "Debe colocar la causa de la cita")]
        [DataType(DataType.Text)]
        public string why { get; set; }


        public string? NameTest { get; set; }

        public int? IdNameTest { get; set; }
        public int? clave { get; set; }

        public List<ListPatient> ListPatient { get; set; } = new List<ListPatient>();
        public List<ListMedico> ListMedico { get; set; } = new List<ListMedico>();
        public List<ListLabTest> ListLabTest { get; set; } = new List<ListLabTest>();
    }
}
