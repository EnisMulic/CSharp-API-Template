using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Template.Domain
{
    public class Role : IdentityRole<int>
    {
        public virtual IList<UserRole> UserRoles { get; set; }
        public virtual IList<RoleClaim> RoleClaims { get; set; }
    }
}
