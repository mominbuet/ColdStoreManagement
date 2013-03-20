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

namespace CSMSys.Web.Pages.INV
{
    public partial class StockLocation : System.Web.UI.Page
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
            //if (e.CommandName.Equals("Delete"))
            //{
            //    int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

            //    if (intPartyID > 0)
            //    {
            //        DeleteParty(intPartyID);
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
            //if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            //{
            //    ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
            //    btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

            //    ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
            //    btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            //}
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
                string query = @"select sl.LoadingID as lid,sl.SerialNo,sl.Bags,sl.SerialNo+'/'+CAST(sl.Bags AS VARCHAR(10)) as slb,sl.LoadedDate,CONVERT(VARCHAR(10),sl.LoadedDate, 103) AS cdates,
                                          sl.PartyCode,par.PartyName,par.ContactNo,par.PartyID, sl.ChamberNo,sl.Floor,sl.Pocket,sl.Line,sl.Remarks
                                            from INVStockLoading sl  INNER JOIN INVParty as par on par.PartyCode = sl.PartyCode
                                       WHERE sl.LoadedDate between  '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY sl.LoadingID DESC";

                dsStockSerial.SelectCommand = query;
                grvStockSerial.DataBind();
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                string fromDate = txtFromDate.Text;
                string toDate = txtToDate.Text;
                Response.Redirect("~/Pages/StockSerialReport/LoadReportViewer.aspx?FD=" + fromDate + "&TD=" + toDate);

            }
            catch (Exception ex)
            {
            }
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
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                lblFailure.Text = "Select Date";
                txtFromDate.Focus();
                return false;
            }
            return true;
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