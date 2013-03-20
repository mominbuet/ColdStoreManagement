using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Pages.LoanRequisitionReport
{
    public partial class LoanRequisitionView : System.Web.UI.Page
    {
        private string strSearch = "";
        protected void txtfromchanged(object sender, EventArgs e)
        {
            rdbDate.Checked = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rdbDate.Checked = true;
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
        public string removespan(string input)
        {
            input = input.Replace("<span class=highlight>", "");
            input = input.Replace("</span", "");
            return input;
        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }

        private bool checkValidity()
        {
            DateTime dt = new DateTime();
            dt.ToString("yyyy/MM/dd");
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
            if (txtFromDate.Text == "" && txtToDate.Text == "")
            {
                lblFailure.Text = "Select Date";
                txtFromDate.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            //txtparty.Text = string.Empty;
            ////txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            //txtbags.Text = string.Empty;
            //txtchamber.Text = string.Empty;
            ////ddlCustomer.SelectedIndex = 0;
            //txtpocket.Text = string.Empty;
            //txtfloor.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            //txtline.Text = string.Empty;
            //txtSerialNo.Text = string.Empty;
        }
        #endregion
        #region Methods for Search   & refresh
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
            if (rdbDate.Checked)
            {
                if (!checkValidity()) return;
                btnReport.Enabled = true;
                string query = @"select sr.RegistrationID,sr.serialID,sr.BagLoan,sr.CarryingLoan,sr.SerialNo,ip.PartyName,ip.PartyCode,ip.partyid,ip.ContactNo,convert(varchar(10),sr.createddate,10) as cdate
                                                            from SRVRegistration as sr
                                                            inner JOIN INVParty as ip on sr.PartyID=ip.PartyID
                                                            where sr.Requisitioned='Applied For Loan' and
                                                            (sr.createddate>='" + txtFromDate.Text + "' and sr.createddate<= '" + txtToDate.Text + "') ORDER BY sr.RegistrationID DESC";

                dsParty.SelectCommand = query;
                grvParty.DataBind();
            }
        }
        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvParty.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvParty.DataBind();
        }
        #endregion
        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
                Label lblBagLoan = (Label)e.Row.FindControl("lblBagLoan");
                //lblBagLoan.Text=(DataBinder.Eval(e.Row.DataItem, "PartyID").ToString());
                lblBagLoan.Text = (new BagLoanDAOLinq().getAllBagLoansByparty(int.Parse(DataBinder.Eval(e.Row.DataItem, "PartyID").ToString()))).ToString();
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity())
                    Response.Redirect("~/HtmlReports/LoanPreRequisition.aspx?PartyCode=" + txtSearch.Text + "&FDate=2013/02/25&TDate=2013/06/25&amountBag="+txtamnt.Text);
                else
                    Response.Redirect("~/HtmlReports/LoanPreRequisition.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&amountBag=" + txtamnt.Text);
            }
            catch (Exception ex)
            {
            }
        }

        protected void rdbDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDate.Checked == true)
            {
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
            }
            else
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
            }
        }

    }
}