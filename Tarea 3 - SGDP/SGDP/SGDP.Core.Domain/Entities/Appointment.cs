using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Domain.Entities
{
    public class Appointment
    {
        public virtual int Id { get; set; }
        public string Patiente { get; set; }

        public int? PacienteId { get; set; }


        public string Doctor { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string why { get; set; }
        public string? NameTest { get; set; }
        public int? clave { get; set; }


        public int? medicoID { get; set; }
        public Medico Medico1 { get; set; }







        public int? LabTestId { get; set; }
        public LabTests LabTests1 { get; set; }

    }

}
