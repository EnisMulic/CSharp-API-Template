using Microsoft.AspNetCore.Identity;

namespace Template.Domain
{
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User User { get; set; }
    }
}
