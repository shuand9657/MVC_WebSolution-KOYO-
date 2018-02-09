using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WebSolution.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using BzModelClass;
using System.Linq;
using DBModelClass.DBModel;
using System.Collections.Concurrent;
using Microsoft.Owin.Security.Infrastructure;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Text;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using WebSolution.Provider;
using WebSolution.Format;
using System.Web.Configuration;
using System.Web.Security;
using System.Web;

namespace WebSolution
{
    public partial class Startup
    {
        UserProfile _userProfile = new UserProfile();
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions());

            // Enable Application Sign In Cookie
            app.SetDefaultSignInAsAuthenticationType("Application");
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Application",
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
                LoginPath = new PathString("/Login/Login"),
                LogoutPath = new PathString("/Login/Logout"),
                CookieSecure = CookieSecureOption.Always
            });

            // Enable External Sign In Cookie
            //app.SetDefaultSignInAsAuthenticationType("External");
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = "External",
            //    AuthenticationMode = AuthenticationMode.Passive,
            //    CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
            //    ExpireTimeSpan = TimeSpan.FromMinutes(30),
            //});

            // Setup Authorization Server
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {

                AuthorizeEndpointPath = new PathString("/OAuth/Authorize"),
                TokenEndpointPath = new PathString("/OAuth/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                ApplicationCanDisplayErrors = true,
                AllowInsecureHttp = true,
                
                // Authorization server provider which controls the lifecycle of Authorization Server
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnValidateClientAuthentication = ValidateClientAuthentication,
                    OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                    OnGrantClientCredentials = GrantClientCredetails
                },

                // Authorization code provider which creates and receives authorization code
                AuthorizationCodeProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateAuthenticationCode,
                    OnReceive = ReceiveAuthenticationCode,
                },

                // Refresh token provider which creates and receives referesh token
                RefreshTokenProvider = new AuthenticationTokenProvider
                {
                    OnCreate = CreateRefreshToken,
                    OnReceive = ReceiveRefreshToken,
                },


            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            #region old setting
            //// Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            //// Enable the application to use a cookie to store information for the signed in user
            //// and to use a cookie to temporarily store information about a user logging in with a third party login provider
            //// Configure the sign in cookie
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
            //    LoginPath = new PathString("/Login/Login"),
            //    LogoutPath = new PathString("/Login/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        // Enables the application to validate the security stamp when the user logs in.
            //        // This is a security feature which is used when you change a password or add an external login to your account.  
            //        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //            validateInterval: TimeSpan.FromMinutes(30),
            //            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //    }
            //});            
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //// Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            //// Enables the application to remember the second login verification factor such as phone or email.
            //// Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            //// This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //// Uncomment the following lines to enable logging in with third party login providers
            ////app.UseMicrosoftAccountAuthentication(
            ////    clientId: "",
            ////    clientSecret: "");

            ////app.UseTwitterAuthentication(
            ////   consumerKey: "",
            ////   consumerSecret: "");

            ////app.UseFacebookAuthentication(
            ////   appId: "",
            ////   appSecret: "");

            ////app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            ////{
            ////    ClientId = "",
            ////    ClientSecret = ""
            ////});
            #endregion
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/OAuth2/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://www.koyosms.com")
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            
            context.Validated();
            return Task.FromResult(0);
        }

        private Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(context.Password));
            if (_userProfile.CheckUserValidation(context.UserName, encodedPassword))
            {
                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(new GenericIdentity(context.UserName ,context.Options.AuthenticationType),context.Scope.Select(x=>new Claim("urn: oauth:scope", x)));
                oAuthIdentity.AddClaim(new Claim("user", context.UserName,DefaultAuthenticationTypes.ExternalBearer));
                context.Validated(oAuthIdentity);

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        context.UserName,
                        DateTime.Now,
                        DateTime.Now.AddHours(3),
                        true,
                        encodedPassword,
                        FormsAuthentication.FormsCookiePath
                        );
                string encodedTicket = FormsAuthentication.Encrypt(ticket);
                var cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
                cookies.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(cookies);
                
                //context.Response.Headers.Add("AuthorizationToken", new[] { "*" });
            }
            else
            {
                context.SetError("invalid_grant", "Username or Password is invalid!");
            }
            return Task.FromResult(0);
        }

        private Task GrantClientCredetails(OAuthGrantClientCredentialsContext context)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType), context.Scope.Select(x => new Claim("urn:oauth:scope", x)));

            context.Validated(identity);

            return Task.FromResult(0);
        }

        private readonly ConcurrentDictionary<string, string> _authenticationCodes =
        new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        private void CreateAuthenticationCode(AuthenticationTokenCreateContext context)
        {
            //context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            context.SetToken("aaaaaa");
            _authenticationCodes[context.Token] = context.SerializeTicket();
        }

        private void ReceiveAuthenticationCode(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authenticationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        {
            context.SetToken(context.SerializeTicket());
        }

        private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}