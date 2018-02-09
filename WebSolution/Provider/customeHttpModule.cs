using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
    public class customeHttpModule : IHttpModule
    {
        private HttpApplication httpApp;
        public static ArrayList objArrayList = new ArrayList();

        public customeHttpModule(){}
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            //throw new NotImplementedException();
            this.httpApp = context;
            //httpApp.Context.Response.Clear();
            objArrayList.Clear();
            objArrayList.Add("HttpModule: Init()");

            httpApp.BeginRequest += new EventHandler(OnBeginrequest);
            httpApp.AuthenticateRequest += new EventHandler(OnAuthenticateRequest);
            httpApp.PostAuthenticateRequest += new EventHandler(OnPostAuthenticateRequest);
            httpApp.AuthorizeRequest += new EventHandler(OnAuthorizeRequest);
            httpApp.PostAuthorizeRequest += new EventHandler(OnPostAuthorizeRequest);
        }

        private void OnPostAuthorizeRequest(object sender, EventArgs e)
        {
            objArrayList.Add("httpModule:OnPostAuthorizeRequest");
        }

        private void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            objArrayList.Add("httpModule:OnPostAuthenticateRequest");
        }

        private void OnAuthorizeRequest(object sender, EventArgs e)
        {
            objArrayList.Add("HttpModule:OnAuthorizeRequest");
        }
        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            objArrayList.Add("httpModule: OnAutheticateRequest");
        }
        void OnUpdateRequestCache(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnUpdateRequestCache");
        }
        void OnReleaseRequestState(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnReleaseRequestState");
        }
        void OnPostRequestHandlerExecute(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnPostRequestHandlerExecute");
        }
        void OnPreRequestHandlerExecute(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnPreRequestHandlerExecute");
        }
        void OnAcquireRequestState(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnAcquireRequestState");
        }
        void OnResolveRequestCache(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnResolveRequestCache");
        }
        void OnAuthorization(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:OnAuthorization");
        }
        void OnAuthentication(object sender, EventArgs a)
        {

            objArrayList.Add("httpModule:AuthenticateRequest");
        }
        void OnBeginrequest(object sender, EventArgs a)
        {

            objArrayList.Add("httpModule:BeginRequest");
        }
        void OnEndRequest(object sender, EventArgs a)
        {
            objArrayList.Add("httpModule:EndRequest");
            objArrayList.Add("<hr>");
            foreach (string str in objArrayList)
            {
                httpApp.Context.Response.Write(str + "<br>");
            }

        }
    }