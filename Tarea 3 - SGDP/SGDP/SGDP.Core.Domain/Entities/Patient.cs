using SGDP.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDP.Core.Domain.Entities
{
    public class Patient : AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? ImagePathMedico { get; set; }
        public string Phone { get; set; }
        public string Direccion { get; set; }
        public string Cedula { get; set; }
        public string Sex { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }

        public ICollection<Result> results { get; set; }
    }
}
