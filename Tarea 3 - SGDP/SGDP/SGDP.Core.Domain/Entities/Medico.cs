using SGDP.Core.Domain.Common;

namespace SGDP.Core.Domain.Entities
{
    public class Medico : BaseModel
    {
        public string Phone {  get; set; }
        public string Specialization { get; set; }
        public string IdCedula { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
