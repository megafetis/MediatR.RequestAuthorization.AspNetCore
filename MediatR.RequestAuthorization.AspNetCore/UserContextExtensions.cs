using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MediatR.RequestAuthorization.AspNetCore
{
    public static class UserContextExtensions
    {
        public static HttpContext HttpContext(this IUserContext context)
        {
            return ((HttpUserContext) context)?.Http.HttpContext;
        }
    }
}
