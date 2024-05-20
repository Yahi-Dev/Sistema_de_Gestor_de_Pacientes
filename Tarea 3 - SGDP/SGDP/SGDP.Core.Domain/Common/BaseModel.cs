namespace SGDP.Core.Domain.Common
{
    public class BaseModel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ImagePathMedico { get; set; }
    }
}
