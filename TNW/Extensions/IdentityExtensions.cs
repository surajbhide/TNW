using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");
            return (claim == null) ? String.Empty : claim.Value;
        }
    }
}
