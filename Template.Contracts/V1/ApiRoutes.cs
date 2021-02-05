using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string Register = Base + "/Auth/Register";

            public const string Authenticate = Base + "/Auth/Login";

            public const string Refresh = Base + "/Auth/Refresh";
        }

        public static class User
        {
            public const string Get = Base + "/User";

            public const string GetById = Base + "/User";

            public const string Post = Base + "/User";

            public const string Put = Base + "/User";

            public const string Delete = Base + "/User";

            public const string Me = Base + "/@me";
        }
    }
}
