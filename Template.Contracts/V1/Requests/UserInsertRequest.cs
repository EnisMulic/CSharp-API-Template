using System.ComponentModel.DataAnnotations;

namespace Template.Contracts.V1.Requests
{
    public class UserInsertRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
