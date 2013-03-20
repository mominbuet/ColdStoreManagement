using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace CSMSys.Web.Pages.ACC
{
    public partial class ChartOfAccount : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        //private INVAccount _Account;
        //ResourceManager rm;
        //CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //rm = new ResourceManager("Resources.Strings",
                //         System.Reflection.Assembly.Load("App_GlobalResources"));
                //ci = Thread.CurrentThread.CurrentCulture;
                grvAccount.DataBind();
            }
            //else
            //{
            //    rm = new ResourceManager("Resources.Strings",
            //         System.Reflection.Assembly.Load("App_GlobalResources"));
            //    ci = Thread.CurrentThread.CurrentCulture;
            //}
            if (Request.QueryString["language"] == "bn")
            {
                //lblcustsetup.Text = "saad";
                //Response.Write("saad");
                //rm = new ResourceManager("Resources.Strings",
                //    System.Reflection.Assembly.Load("App_LocalResources"));
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("bn-BD");
                //lblcustsetup.Text = rm.GetString("customersetup", Thread.CurrentThread.CurrentCulture);

            }

        }

        #region Methods
        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtSearch.Text;
            // Setup the regular expression and add the Or operator.
            Regex RegExp = new Regex(SearchStr.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
            // Highlight keywords by calling the 
            //delegate each time a keyword is found.
            return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
        }

        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }
        #endregion

        #region Methods For Grid
        protected void grvAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    DeleteAccount(intAccountID);
                }
            }
        }

        protected void grvAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAccount.PageIndex = e.NewPageIndex;
            grvAccount.DataBind();
        }

        protected void grvAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvAccount.DataBind();
            grvAccount.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvAccount.DataBind();
            grvAccount.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DeleteAccount(int id)
        {
            //INVAccount _tempAccount = new INVAccount();
            ////_tempAccount.AccountID = id;
            //if (id > 0)
            //{
            //    _tempAccount = new AccountManager().GetAccountByID(id);
            //    if (new AccountManager().DeleteAccount(_tempAccount))
            //    {

            //        grvAccount.DataBind();
            //    }
            //}
        }
        #endregion
    }
}