using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Requests
{
    public class UserAccountAuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
