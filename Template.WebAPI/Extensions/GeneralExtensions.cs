using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Template.WebAPI.Extensions
{
    public static class GeneralExtensions
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return null;
            }

            return Convert.ToInt32(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }
    }
}
