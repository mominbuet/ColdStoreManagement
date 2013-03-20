using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using CSMSys.Lib.Utility;

namespace CSMSys.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            //Session.Add(CSMSysConstants.SITE_CULTURE, System.Globalization.CultureInfo.CurrentUICulture);
            //Session.Add(CSMSysConstants.SITE_CULTURE, System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            //Session.Add(CSMSysConstants.SITE_CULTURE, "en-US");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}