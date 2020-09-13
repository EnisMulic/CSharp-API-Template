using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Responses
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
