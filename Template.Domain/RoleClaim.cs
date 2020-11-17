using Microsoft.AspNetCore.Identity;

namespace Template.Domain
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }
    }
}
