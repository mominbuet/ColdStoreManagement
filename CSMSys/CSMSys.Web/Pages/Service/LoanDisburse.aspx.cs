using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Manager.INV;
namespace CSMSys.Web.Pages.Service
{
    public partial class LoanDisburse1 : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private IList<INVStockSerial> iss;
        //private INVStockSerial _Serial;
        protected void Page_Load(object sender, EventArgs e)
        {
            iss = new SerialManager().GetAllSerial(); 
            if (!IsPostBack)
            {
                
                grvloan.DataBind();
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
        protected void grvloan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Select"))
            //{
            //    int loanid = Convert.ToInt32(e.CommandArgument.ToString());

            //    if (loanid > 0)
            //    {
            //        DisburseLoan(loanid);
                    
            //        Response.Redirect("~/Pages/LoanDisburseReport/LoanReportViwer.aspx?caseID=" + new LoanManager().GetLoanDByID(loanid).caseID);

            //    }
            //}
        }

        protected void grvloan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvloan.PageIndex = e.NewPageIndex;
            grvloan.DataBind();
        }

        protected void grvloan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgselect");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + DataBinder.Eval(e.Row.DataItem, "LoanID") + "');");
                //Label cdate = ((Label) e.Row.FindControl("lblCreatedDate"));
                //cdate.Text = Lib.Utility.LibCommonUtility.GetShortDateString_BN(DateTime.ParseExact(cdate.Text, "dd/MM/yyyy",
                //                  new CultureInfo("en-US")));

                

                string[] serids = DataBinder.Eval(e.Row.DataItem, "serialIDs").ToString().Split(',');

                int bagcount = 0;
                foreach (string serid in serids)
                {
                    if (serid != "")
                    {
                        Label serials = (Label) e.Row.FindControl("lblserials");
                        INVStockSerial isto = iss.Where(t => t.SerialID.Equals(long.Parse(serid))).First();
                        serials.Text += isto.SerialNo + ",";
                        Label lbltotalbag = (Label)e.Row.FindControl("lbltotalbag");
                        lbltotalbag.Text = (float.Parse(lbltotalbag.Text) + isto.Bags).ToString();
                        //bagcount += new StockSerialNo().getbagcount(isto.SerialNo);

                    }


                }
                //((Label) e.Row.FindControl("lbltotalbagi")).Text = bagcount.ToString();
                ((Label)e.Row.FindControl("lbltotalLoan")).Text =
                            (float.Parse(((Label)e.Row.FindControl("lbltotalbagi")).Text) *
                             float.Parse(DataBinder.Eval(e.Row.DataItem, "rndloan").ToString())).ToString();
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
                string query = @"SELECT ld.LoanID,ROUND(ld.LoanAmount,2) as rndloan,ld.serialIDs,convert(varchar(10),ld.CreatedDate,103) as cdate,ld.caseID,ld.Bags,
                                    ip.PartyName,ip.PartyCode,ld.Remarks,ld.PartyID
                                    from SRVLoanDisburse as ld inner join INVParty ip on  ld.PartyID = ip.PartyID
                       WHERE ld.CreatedDate between  '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY ld.LoanID DESC";
                dsParty.SelectCommand = query;
                grvloan.DataBind();
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

        protected void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                //string fromDate = txtFromDate.Text;
                //string toDate = txtToDate.Text;
                //Response.Redirect("~/Pages/LoanRequisitionReport/RequisitionReportViewer.aspx?FD=" + fromDate + "&TD=" + toDate + "&ReportID=2");
                Response.Redirect("~/HtmlReports/LoanAfterRequisition.aspx");
            }
            catch (Exception ex)
            {
            }

        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvloan.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvloan.DataBind();
        }
        #endregion

        #region Methods For Delete
        private void DisburseLoan(int id)
        {
            SRVLoanDisburse srv = new LoanDAOLinq().PickByID(id);
            string[] serialids = srv.serialIDs.Split(',');
            foreach (string serialid in serialids)
            {
                if (serialid != "")
                {
                    INVStockSerial invStockSerial = new SerialManager().GetSerialByID(int.Parse(serialid));
                    new LoanManager().updateSRVRegistration(invStockSerial.SerialNo, int.Parse(srv.PartyID.ToString()),
                        int.Parse(srv.caseID.ToString()));
                }

            }
            //print that report here//
        }
        #endregion

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