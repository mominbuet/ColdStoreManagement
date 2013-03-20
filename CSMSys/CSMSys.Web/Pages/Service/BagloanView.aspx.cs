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

namespace CSMSys.Web.Pages.Service
{
    public partial class BagloanView : System.Web.UI.Page
    {

        private string strSearch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
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
            //if (e.CommandName.Equals("Edit"))
            //{
            //    int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

            //    if (intPartyID > 0)
            //    {
            //        //DeleteParty(intPartyID);
            //    }
            //}
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }

        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[0].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[0].Controls[1]).Text + "','" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                HyperLink hpl = (HyperLink)e.Row.FindControl("hplprint");
                hpl.NavigateUrl = "~/Pages/BagLoanReport/ReportBagLoan.aspx?PID=" + (DataBinder.Eval(e.Row.DataItem, "partyid")).ToString() + "&BID=" + (DataBinder.Eval(e.Row.DataItem, "BagLoanID")).ToString();
         
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
            if (rdbDate.Checked)
            {
                if (!checkValidity()) return;
                string query = @"SELECT   sbl.BagLoanID, sbl.PartyID, sbl.BagNumber, sbl.AmountPerBag, sbl.LoanAmount, sbl.Remarks,  CONVERT(VARCHAR(10),sbl.CreatedDate, 103) AS cdates, 
                                                                ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo, sbl.TMID,
(select sum(BagNumber) from SRVBagLoan where BagLoanID>=sbl.BagLoanID) as smbags       
                                                                FROM  dbo.SRVBagLoan AS sbl INNER JOIN
                                                                dbo.INVParty AS ip ON ip.PartyID = sbl.PartyID
                                                            where  (sbl.CreatedDate>='" + txtFromDate.Text + "' and sbl.CreatedDate<= '" + txtToDate.Text + "')    ORDER BY sbl.BagLoanID DESC";
                                   dsBagloans.SelectCommand = query;
                                     grvStockSerial.DataBind();
            }
        }
        #endregion

        protected void btntotal_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;
                Response.Redirect("~/Pages/BagLoanReport/AllPartyBagLoanReportViewer.aspx?FD=" + fromDate + "&TD=" + toDate);

            }
            catch (Exception ex)
            {
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;
                Response.Redirect("~/Pages/BagLoanReport/BagLoanRegister.aspx?FD=" + fromDate + "&TD=" + toDate);

            }
            catch (Exception ex)
            {
            }
        }

        private bool checkValidity()
        {
                DateTime dt = new DateTime();
                dt.ToString("yyyy/MM/dd")     ;
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
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                lblFailure.Text = "Select Date";
                txtFromDate.Focus();
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
            }
            else
            {
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
            }
        }

    }
}