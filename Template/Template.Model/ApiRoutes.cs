using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts
{
    public static class ApiRoutes
    {
        public const string Base = "api";

        public static class UserAccount
        {
            public const string Register = Base + "/UserAccount/Register";

            public const string Authenticate = Base + "/UserAccount/Auth";
        }
    }
}
