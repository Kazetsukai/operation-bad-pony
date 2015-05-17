using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Owin;
using System.Security.Claims;

namespace BadPony.WebApiHost.Utility
{
    public static class UserInfo
    {
        public static string GetCurrentUserName(HttpRequestMessage request)
        {
            IOwinContext ctx = request.GetOwinContext();
            ClaimsPrincipal user = ctx.Authentication.User;
            if (user == null)
            {
                return null;
            }
            else
            {
                return user.Identity.Name;
            }
        }

        public static string GetCurrentUserId(HttpRequestMessage request)
        {
            IOwinContext ctx = request.GetOwinContext();
            ClaimsPrincipal user = ctx.Authentication.User;
            if (user == null || !user.HasClaim((c) => c.Type == ClaimTypes.NameIdentifier))
            {
                return null;
            }
            else
            {
                Claim idClaim = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
                return idClaim.Value;
            }
        }
    }
}
