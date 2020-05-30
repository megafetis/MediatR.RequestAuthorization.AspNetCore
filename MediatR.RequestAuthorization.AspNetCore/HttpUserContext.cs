using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MediatR.RequestAuthorization.AspNetCore
{
    public class HttpUserContext:IUserContext
    {
        public IHttpContextAccessor Http { get; }

        public HttpUserContext(IHttpContextAccessor http)
        {
            Http = http;
            User = http?.HttpContext?.User;
        }

        public virtual string ExtraAttribute(string key)
        {
            return null;
        }

        public ClaimsPrincipal User { get; }
        public string Id
        {
            get
            {
                if (User?.Identity != null && User.Identity.IsAuthenticated)
                {
                    return User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }

                return null;
            }
        }
        public string Name => User?.Identity.Name;
        public bool IsAuthenticated => User?.Identity != null && User.Identity.IsAuthenticated;
        public virtual string ClaimValue(string claimType)
        {
            return User?.Claims?.FirstOrDefault(p => p.Type == claimType)?.Value;
        }

        public virtual bool HasClaim(string type, string value)
        {
            return User?.HasClaim(type, value) ?? false;
        }

        public virtual bool HasClaim(Predicate<Claim> match)
        {
            return User?.HasClaim(match) ?? false;
        }
    }

}
