using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
