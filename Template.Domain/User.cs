using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Template.Domain
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<UserRole> Roles { get; set; }
        public virtual IList<UserClaim> Claims { get; set; }
        public virtual IList<UserLogin> Logins { get; set; }
        public virtual IList<UserToken> Tokens { get; set; }
    }
}
