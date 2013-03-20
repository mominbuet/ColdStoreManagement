using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace CSMSys.Web.Pages.INV
{
    public partial class StockSerial : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private INVStockSerial _Serial;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //grvStockSerial.DataBind();
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
            if (e.CommandName.Equals("Delete"))
            {
                int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intPartyID > 0)
                {
                    DeleteParty(intPartyID);
                }
            }
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }

        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                //Label cdate = ((Label)e.Row.FindControl("lblinserted"));
                //cdate.Text = Lib.Utility.LibCommonUtility.GetShortDateString_BN(DateTime.ParseExact(cdate.Text, "dd/MM/yyyy",
                //                  new CultureInfo("en-US")));
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");
                //StockSerialReport.SerialReport
                HyperLink hpl = (HyperLink)e.Row.FindControl("hplprint");
                hpl.NavigateUrl = "~/pages/StockSerialReport/SerialReport.aspx?serialid=" + DataBinder.Eval(e.Row.DataItem, "SerialID");
                //ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
            if (rdbDate.Checked)
            {
                if (!checkValidity()) return;
                btnReport.Visible = true;
                string query = @"SELECT ROW_NUMBER() OVER (ORDER BY ss.SerialID) As SlNo,ss.remarks,Convert(varchar(10),ss.SerialDate,
                                                                103) as Inserted, ss.SerialID, ss.Serial, ss.Bags, ss.SerialNo, ss.PartyID, ss.PartyCode, ip.PartyName, ip.ContactNo, ss.Remarks
                                                                FROM INVStockSerial AS ss INNER JOIN INVParty AS ip ON ss.PartyID = ip.PartyID 
                                                                WHERE ss.SerialDate between  '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY ss.SerialID DESC";
                dsStockSerial.SelectCommand = query;
                grvStockSerial.DataBind();
            }
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvStockSerial.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DeleteParty(int id)
        {
            INVStockSerial _tempSerial = new INVStockSerial();
            //_Serial.SerialID = id;
            if (id > 0)
            {
                _tempSerial = new SerialManager().GetSerialByID(id);
                if (new SerialManager().DeleteSerial(_tempSerial))
                {
                    grvStockSerial.DataBind();
                }
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
                Response.Redirect("~/Pages/StockSerialReport/SerialReportViewer.aspx?FD=" + fromDate + "&TD=" + toDate);

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