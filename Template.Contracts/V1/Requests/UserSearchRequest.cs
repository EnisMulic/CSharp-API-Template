using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Template.Contracts.V1.Requests
{
    public class UserSearchRequest : SortQuery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public IList<int> Roles { get; set; }
    }
}
