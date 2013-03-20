using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.Utility;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace CSMSys.Web
{
    public partial class DefaultMaster : System.Web.UI.MasterPage
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            string lang = Request.QueryString["language"];
            //Response.Write(lang);
            if (lang == "bn")
            {
                CSMSysSiteMapDataSource.SiteMapProvider = "CustomXmlSiteMapProviderBN";
                //Label lblcustomersetup = (Label)MainContent.FindControl("lblcustsetup");
                
            }
            else
            {
                CSMSysSiteMapDataSource.SiteMapProvider = "CustomXmlSiteMapProvider";
                

            }
            if (!IsPostBack)
            {
                if (Session[CSMSysConstants.SITE_CULTURE] != null)
                {
                    //rdoList.SelectedValue = Session[CSMSysConstants.SITE_CULTURE].ToString();
                }
            }

            ////For Menu Localization
            //if (rdoList.SelectedValue.Equals(CSMSysConstants.SITE_CULTURE))
            //{
            //    if (!CSMSysSiteMapDataSource.SiteMapProvider.ToString().Equals(CSMSysConstants.SITE_MAP))
            //    {
            //        CSMSysSiteMapDataSource.SiteMapProvider = CSMSysConstants.SITE_MAP;
            //    }

            //    //if (UtilityCommon.GetDirectorate() != null)
            //    //{
            //    //    lblDirectorateName.Text = UtilityCommon.GetDirectorate().OfficeName;
            //    //}
            //}
            //else
            //{
            //    if (!CSMSysSiteMapDataSource.SiteMapProvider.ToString().Equals(CSMSysConstants.SITE_MAP))
            //    {
            //        CSMSysSiteMapDataSource.SiteMapProvider = CSMSysConstants.SITE_MAP;
            //    }

            //    //if (UtilityCommon.GetDirectorate() != null)
            //    //{
            //    //    lblDirectorateName.Text = UtilityCommon.GetDirectorate().OfficeName;
            //    //}
            //}
        }
    }
}
