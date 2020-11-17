using Microsoft.AspNetCore.Identity;

namespace Template.Domain
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}
