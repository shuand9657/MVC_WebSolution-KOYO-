using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;

namespace WebSolution
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Remove(typeof(CancellationToken));
            ModelBinders.Binders.Add(typeof(CancellationToken), new FixedCancellationTokenModelBinder());
        }



        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            CultureInfo ci = null;
        //    try
        //    {
        //        if (cultureCookie != null)
        //        {
        //            ci = new CultureInfo(cultureCookie.Value);
        //        }
        //        else
        //        {
        //            var userLanguage = Request.UserLanguages;
        //            if (userLanguage.Length > 0)
        //            {
        //                try
        //                {
        //                    ci = new CultureInfo(userLanguage[0]);
        //                }
        //                catch (CultureNotFoundException)
        //                {
        //                    ci = CultureInfo.InvariantCulture;
        //                }
        //            }
        //            else
        //            {
        //                ci = CultureInfo.InvariantCulture;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        //        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        //    }
        }
    }
}
