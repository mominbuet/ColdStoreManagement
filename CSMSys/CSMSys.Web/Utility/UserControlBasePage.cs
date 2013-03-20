using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CSMSys.Lib.Utility;

namespace CSMSys.Web.Utility
{
    public class UserControlBasePage : System.Web.UI.UserControl
    {
        protected override void FrameworkInitialize()
        {
            string cultureCode = CSMSysConstants.SITE_CULTURE;

            if (Session[CSMSysConstants.SITE_CULTURE] != null)
            {
                cultureCode = Session[CSMSysConstants.SITE_CULTURE].ToString();
            }

            if (!string.IsNullOrEmpty(Request[CSMSysConstants.POST_BACK_EVENT_TARGET]) && Request.Form[Request[CSMSysConstants.POST_BACK_EVENT_TARGET]] != null)
            {
                cultureCode = Request.Form[Request[CSMSysConstants.POST_BACK_EVENT_TARGET]].ToString();
                Session.Add(CSMSysConstants.SITE_CULTURE, cultureCode);
            }

            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(cultureCode);

            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            base.FrameworkInitialize();
        }
    }
}