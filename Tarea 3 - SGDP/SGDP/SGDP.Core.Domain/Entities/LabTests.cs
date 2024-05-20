using SGDP.Core.Domain.Common;

namespace SGDP.Core.Domain.Entities
{
    public class LabTests : AuditableBaseEntity
    {
        public string TestName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
