using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace TestCore
{
    public class MockHttpContextAccessor: IHttpContextAccessor
    {
        public MockHttpContextAccessor()
        {
            

            HttpContext = new DefaultHttpContext()
            {
                User = null
            };

        }

        public void SetUser()
        {
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Role, "admins"),

                new Claim(ClaimTypes.NameIdentifier, "dzsfgdsfgafg"),
            }, "ApplicationCookie");
            var pr = new ClaimsPrincipal(identity);
            HttpContext.User = pr;
        }

        public HttpContext HttpContext { get; set; }
    }
}
