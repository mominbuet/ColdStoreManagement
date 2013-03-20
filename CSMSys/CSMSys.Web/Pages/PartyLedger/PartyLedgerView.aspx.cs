using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace CSMSys.Web.Pages.PartyLedger
{
    public partial class PartyLedgerView : System.Web.UI.Page
    {

        private string strSearch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime now = new DateTime();
                now.ToString("yyyy/MM/dd");
                txtToDate.Text = (DateTime.TryParse(DateTime.Now.ToString(), out now))?now.ToString():"";
                txtFromDate.Text = "2013/02/25";
                grvStockSerial.DataBind();
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
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //if (e.CommandName.Equals("Select"))
            //{
            //    int partyID = Convert.ToInt32(e.CommandArgument.ToString());
            //    //GridViewRow selectedRow = ((GridView)e.CommandSource).Rows[1];

            //    if (partyID > 0)
            //    {
            //        if (!checkValidity()) return;
            //        string fromDate = txtFromDate.Text;
            //        string toDate = txtToDate.Text;
            //        Response.Redirect("~/Pages/PartyLedger/PartyWiseLedger.aspx?PID=" + partyID + "FD=" + fromDate + "&TD=" + toDate); ;

            //    }
            //}
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }
        int bagcount = 0;
        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                bagcount += int.Parse(DataBinder.Eval(e.Row.DataItem, "BagLoaded").ToString());
                lblsummary.Text = bagcount + " বস্তা লোন নেয়া হয়েছে।";
            }
        }
        #endregion
        #region Methods For Button
        protected void imgRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text.Trim();
           // string strQuery = "exec SP_Party_Ledger"+;
            if (rdbDate.Checked)
            {
                if (!checkValidity()) return;
                lblFailure.Text = "";
                //grvStockSerial.DataSource = Search();
                dsPartyLedger.FilterParameters.Clear();
                dsPartyLedger.FilterExpression = "CreatedDate>='{0}' AND CreatedDate<='{1}'";
               // TextBox1.Text.ToString("MM/dd/yyyy");
                //DateTime txtFromDate =Convert.ToDateTime(txtFromDate.ToString("MM/dd/yyyy"));
                //dsPartyLedger.FilterParameters.Add("CreatedDate", txtFromDate.Text.ToString("MM/dd/yyyy"));
                string fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy");
                string toDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                dsPartyLedger.FilterParameters.Add("CreatedDate", fromDate);
                dsPartyLedger.FilterParameters.Add("CreatedDate", toDate);
                grvStockSerial.DataBind();
            }
        }
                
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                lblFailure.Text = "";
                string fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM/dd/yyyy");
                string toDate = Convert.ToDateTime(txtToDate.Text).ToString("MM/dd/yyyy");
                Response.Redirect("~/Pages/PartyLedger/PartyLedgerReport.aspx?FD=" + fromDate + "&TD=" + toDate);

            }
            catch (Exception ex)
            {
            }
        }

        private bool checkValidity()
        {

            DateTime dt = new DateTime();
            dt.ToString("yyyy/MM/dd");
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                
                //lblFailure.Text = "Select Date";
                //txtFromDate.Focus();
                //return false;

            }
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                
                //lblFailure.Text = "Select Date";
                //txtFromDate.Focus();
                //return false;
            }
            //String.Format("{0:yyyy/MM/dd}", dt);
            if (!DateTime.TryParse(txtFromDate.Text, out dt))
            {
                lblFailure.Text = "Select From Date";
                txtFromDate.Focus();
                return false;
            }

            if (!DateTime.TryParse(txtToDate.Text, out dt))
            {
                lblFailure.Text = "Select To Date";
                txtToDate.Focus();
                return false;

            }
         
            return true;
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }

        protected void rdbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDate.Checked == true)
            {
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                btnReport.Enabled = true;
                btnReport.Visible = true;
            }
            else
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                btnReport.Enabled = false;
                btnReport.Visible = false;
            }
        }

        protected void grvStockSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int partyID = Convert.ToInt32(grvStockSerial.SelectedRow.Cells[1].Text);
            string partyID = grvStockSerial.SelectedRow.Cells[1].Text;
            //GridViewRow selectedRow = ((GridView)e.CommandSource).Rows[1];

                if (!checkValidity()) return;
                lblFailure.Text = "";
                string fromDate = Convert.ToDateTime(txtFromDate.Text).ToString("MM/dd/yyyy");
                string toDate = Convert.ToDateTime(txtToDate.Text).ToString("MM/dd/yyyy");
                Response.Redirect("~/Pages/PartyLedger/PartyWiseLedger.aspx?PID=" + partyID + "&FD=" + fromDate + "&TD=" + toDate); ;

        }

    }
}