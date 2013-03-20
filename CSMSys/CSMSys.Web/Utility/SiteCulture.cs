using System;
using System.Web;
using System.Threading;
using CSMSys.Lib.Utility;

namespace CSMSys.Web.Utility
{
    public class SiteCulture : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(OnAcquireRequestState);
        }

        public void Dispose()
        {

        }

        public void OnAcquireRequestState(Object i_object, EventArgs i_eventArgs)
        {
            //HttpApplication l_httpApplication = i_object as HttpApplication;

            //// check whether the language change parameter has been passed
            //var l_language = l_httpApplication.Request.Params[CSMSysConstants.SITE_CULTURE];
            //var l_boolLanguageChanged = false;
            //if (l_language == null)
            //{
            //    // if language parameter is not sent, then take language from session
            //    l_language = (string)l_httpApplication.Session[CSMSysConstants.SITE_CULTURE];
            //}
            //else
            //{
            //    // If language parameter is indeed sent, then user wants to change language.
            //    // I will make sure I tag this in order to redirect to.
            //    l_boolLanguageChanged = true;
            //}

            //// having the language a thand, let us set it.
            //var l_culture = new System.Globalization.CultureInfo(l_language);

            //Thread.CurrentThread.CurrentCulture = l_culture;
            //Thread.CurrentThread.CurrentUICulture = l_culture;

            //// save language to session
            //l_httpApplication.Session[CSMSysConstants.SITE_CULTURE] = l_language;

            //// check whether I have redirect
            //if (l_boolLanguageChanged && l_httpApplication.Request.UrlReferrer != null)
            //{
            //    l_httpApplication.Response.Redirect(l_httpApplication.Request.UrlReferrer.AbsolutePath);
            //}

        } // OnAcquireRequestState	
    }
}