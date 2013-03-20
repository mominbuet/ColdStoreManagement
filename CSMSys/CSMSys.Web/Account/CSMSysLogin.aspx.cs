using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Account
{
    public partial class CSMSysLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            Login login = (Login)sender;

            Session.Add(CSMSysConstants.ASPNET_USER, login.UserName);
            Response.Redirect("~/Pages/Default.aspx");
        }
    }
}
