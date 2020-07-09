using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
