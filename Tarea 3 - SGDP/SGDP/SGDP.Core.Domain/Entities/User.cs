using SGDP.Core.Domain.Common;

namespace SGDP.Core.Domain.Entities
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TypeUser {  get; set; }
    }
}
