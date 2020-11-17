using Microsoft.AspNetCore.Identity;

namespace Template.Domain
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public virtual User User { get; set; }
    }
}
