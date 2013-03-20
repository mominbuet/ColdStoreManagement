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
namespace CSMSys.Web.Pages.INV
{
    public partial class PartySetup : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private INVParty _Party;
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //rm = new ResourceManager("Resources.Strings",
                //         System.Reflection.Assembly.Load("App_GlobalResources"));
                //ci = Thread.CurrentThread.CurrentCulture;
                grvParty.DataBind();
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
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Delete"))
            //{
            //    int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

            //    if (intPartyID > 0)
            //    {
            //        DeleteParty(intPartyID);
            //    }
            //}
        }

        protected void grvParty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvParty.PageIndex = e.NewPageIndex;
            grvParty.DataBind();
        }

        protected void grvParty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                //ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
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
            grvParty.DataBind();
            grvParty.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvParty.DataBind();
            grvParty.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DeleteParty(int id)
        {
            INVParty _tempParty = new INVParty();
            //_tempParty.PartyID = id;
            if (id > 0)
            {
                _tempParty = new PartyManager().GetPartyByID(id);
                if (new PartyManager().DeleteParty(_tempParty))
                {
                   
                    grvParty.DataBind();
                }
            }
        }
        #endregion

    }
}