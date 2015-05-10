using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace BadPony.WebApiHost.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        public Utility.AuthenticateMethod[] Get()
        {
            IOwinContext ctx = Request.GetOwinContext();
            IEnumerable<AuthenticationDescription> authTypes = ctx.Authentication.GetExternalAuthenticationTypes();
            return authTypes.Select
            (
                a => new Utility.AuthenticateMethod() 
                { 
                    Name = a.Caption, 
                    Code = a.AuthenticationType 
                }
            ).ToArray();
        }

        public IHttpActionResult Get(string id)
        {
            IOwinContext ctx = Request.GetOwinContext();
            if (ctx.Authentication.User != null)
            {
                if (id == "Logout")
                {
                    ctx.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                }

                return Redirect(new Uri("/#/", UriKind.Relative));
            }

            ExternalLoginInfo login = ctx.Authentication.GetExternalLoginInfo();
            if(login == null)
            {
                IEnumerable<AuthenticationDescription> authTypes = ctx.Authentication.GetExternalAuthenticationTypes();
                if (authTypes.Any(a => a.AuthenticationType == id))
                {
                    ctx.Authentication.Challenge(id);
                }
                
                return Unauthorized();
            }
            else
            {
                ClaimsIdentity localLogin = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                localLogin.AddClaims(login.ExternalIdentity.Claims.Where(c => c.Type != ClaimTypes.NameIdentifier));
                localLogin.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Login.LoginProvider + ":" + login.Login.ProviderKey));
                localLogin.AddClaim(new Claim(localLogin.RoleClaimType, "StandardUser"));

                /*Everyone's an admin lololol*/
                localLogin.AddClaim(new Claim(localLogin.RoleClaimType, "AdminUser"));
                    
                ctx.Authentication.SignIn(localLogin);
                return Redirect(new Uri("/#/", UriKind.Relative));
            }
        }
    }
}
