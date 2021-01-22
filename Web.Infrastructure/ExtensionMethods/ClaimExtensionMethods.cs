using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Web.Infrastructure.ExtensionMethods
{
    public static class ClaimExtensionMethods
    {
        public static int GetId(this IEnumerable<Claim> claims)
        {
            return Convert.ToInt32(claims.Where(x=>x.Type=="Id").FirstOrDefault().Value);
        }
    }
}