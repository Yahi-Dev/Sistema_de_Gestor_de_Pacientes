using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Domain.Entities
{
    public class Result
    {
        public virtual int Id { get; set; }
        public string NamePatient { get; set; }
        public string LastNamePatient { get; set; }
        public string cedulapatient { get; set; }
        public int? patientId { get; set; }
        public int? AppointmentId { get; set; }
        public string NameLabTest { get; set;}
        public string ResultTest { get; set;}
        public int? clave {  get; set; }
        public Patient? Patient1 { get; set; }
    }
}
