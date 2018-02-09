using BzModelClass;
using DBModelClass;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebSolution.Models;

namespace WebSolution.Provider
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string clientId = string.Empty,
            //    clientSecrect = string.Empty,
            //    symmetricKeyAsBase64 = string.Empty;

            //if(!context.TryGetBasicCredentials(out clientId, out clientSecrect))
            //{
            //    context.TryGetFormCredentials(out clientId, out clientSecrect);
            //}
            
            //if(context.ClientId == null)
            //{
            //    context.SetError("Invalid_ClientID", "client_Id is not set");
            //    return Task.FromResult<object>(null);
            //}

            //var audience = AudienceStore.FindAudience(context.ClientId);

            //if(audience == null)
            //{
            //    context.SetError("invalid_clientId", string.Format("Invalid Client_id '{0}'", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            UserProfile _userProfile = new UserProfile();
            string encodedPwd = Convert.ToBase64String(Encoding.UTF8.GetBytes(context.Password));

            if (! _userProfile.CheckUserValidation(context.UserName, encodedPwd))
            {
                context.SetError("invalid_grant", "The Username or Password is invalid");
                return;
            }
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return;

            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            //if(user == null)
            //{
            //    context.SetError("invalid_grant", "The Username or password is incorrect");
            //    return;
            //}

            //ClaimsIdentity oauthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

            //var ticket = new AuthenticationTicket(oauthIdentity, null);
            //context.Validated(ticket);
        }
    }
}