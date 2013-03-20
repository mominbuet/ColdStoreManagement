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
using CSMSys.Lib.Manager.SRV;

namespace CSMSys.Web.Pages.Service
{
    public partial class Registration : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private SRVRegistration _Registration;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvRegistration.DataBind();
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
        protected void grvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                int intRegistrationID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intRegistrationID > 0)
                {
                    DeleteRegistration(intRegistrationID);
                }
            }
        }

        protected void grvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRegistration.PageIndex = e.NewPageIndex;
            grvRegistration.DataBind();
        }

        protected void grvRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + (DataBinder.Eval(e.Row.DataItem,"SerialID")) + "');");

                
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
            grvRegistration.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvRegistration.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DeleteRegistration(int id)
        {
            _Registration = new SRVRegistration();
            _Registration.RegistrationID = id;
            if (new RegistrationManager().DeleteRegistration(_Registration))
            {
                grvRegistration.DataBind();
            }
        }
        #endregion
    }
}