using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BzModelClass;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Security;
using WebSolution.Models;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using System.Security.Claims;
using DotNetOpenAuth.OAuth2;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.Owin.Security.Infrastructure;

namespace WebSolution.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        UserProfile _userProfile = new UserProfile();
        private WebServerClient _webServerClient;

        [HttpPost]
        public async Task<ActionResult> Login(FormCollection collection, string returnUrl)
        {
            //OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions();
            var authentication = HttpContext.GetOwinContext().Authentication;
            
            var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

            var tokenServiceUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "OAuth/Token";

            string username = Request.Form.Get("username");//collection["username"];
            string password = collection["password"];
            string keyCulture = collection["keyCulture"];
            string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            Utility.FunctionResult fResult = new Utility.FunctionResult();
            var userProfile = new Dictionary<string, string>();
            
            if (string.IsNullOrWhiteSpace(username)|| string.IsNullOrWhiteSpace(password))
            {
                fResult.Result = false;
                fResult.Message = "Input Error!";
                var data = new { Result = fResult };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session.RemoveAll();
                var verifyData = _userProfile.getUserList(username, encodedPassword);
                //var ctx = Request.GetOwinContext();
                if (!verifyData.Any())
                {
                    fResult.Result = false;
                    fResult.Message = "Login Info Error";
                    var data = new { Result = fResult };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = isPersistent },
                    new ClaimsIdentity(new[] { new Claim(
                        ClaimsIdentity.DefaultNameClaimType, username) }, 
                        DefaultAuthenticationTypes.ApplicationCookie));
                    //using (var client = new HttpClient())
                    //{
                    //    var requestParam = new List<KeyValuePair<string, string>>
                    //    {
                    //        new KeyValuePair<string, string>("grant_type","password"),
                    //        new KeyValuePair<string, string>("username",username),
                    //        new KeyValuePair<string, string>("password",encodedPassword)
                    //    };
                    //    var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParam);
                    //    var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                    //    var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                    //    var responseCode = tokenServiceResponse.StatusCode;
                    //    var responseMsg = new HttpResponseMessage(responseCode)
                    //    {
                    //        Content = new StringContent(responseString, Encoding.UTF8, "applicaition/json")
                    //    };
                    //}
                    var requestParam = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("grant_type","password"),
                            new KeyValuePair<string, string>("username",username),
                            new KeyValuePair<string, string>("password",encodedPassword)
                        };
                    var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParam);

                    var client = new HttpClient();
                    var tokenResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);

                    //var test = TestServer.Create<Startup>();
                    //var response = await TestServer.Create<Startup>().HttpClient.PostAsync("/OAuth/Token", requestParamsFormUrlEncoded);
                    

                    userProfile.Add("username", username);
                    userProfile.Add("keyCulture", keyCulture);
                    userProfile.Add("validateToken", encodedPassword);
                    //userProfile.Add("Token", tokenResponse.ToString());

                    fResult.Result = true;
                    fResult.Message = username;//

                    Response.AppendHeader("AuthToken", encodedPassword);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        username,
                        DateTime.Now,
                        DateTime.Now.AddHours(3),
                        isPersistent,
                        encodedPassword,
                        FormsAuthentication.FormsCookiePath
                        );

                    var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                    string encodedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookies = new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
                    cookies.HttpOnly = true;
                    Response.Cookies.Add(cookies);
                    var data = new
                    {
                        Result = fResult,
                        UserProfile = userProfile,
                        RedirectUrl = Url.Action("Index", "Home"),
                        isRedirect = true
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }   
            }
            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            Utility.FunctionResult fResult = new Utility.FunctionResult();
            if (User.Identity.IsAuthenticated)
            {
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignOut();
                //FormsAuthentication.SignOut();
                Session.Abandon();
                //clear authentication cookies!
                var clearedCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                clearedCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(clearedCookie);
                //clear session cookies!
                var sessionCookie = new HttpCookie("ASP.NET_SessionId", "");
                sessionCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(sessionCookie);

                fResult.Result = false;
                fResult.Message = "Log Out Complete!";


                var data = new {
                    Result = fResult,
                    RedirectUrl = Url.Action("Login", "Login")
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else FormsAuthentication.RedirectToLoginPage();

            return null;
        }
    }
}