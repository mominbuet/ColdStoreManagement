using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.Manager.RptShad;
using System.Data;

namespace CSMSys.Web.HtmlReports
{
    public partial class LoanPreRequisition : System.Web.UI.Page
    {
        private decimal loanApplied = (decimal)0.0;

        private DateTime DateFrom
        {
            get
            {
                if (ViewState["DateFrom"] == null)
                    ViewState["DateFrom"] = -1;
                return (DateTime)ViewState["DateFrom"];
            }
            set
            {
                ViewState["DateFrom"] = value;
            }
        }

        private DateTime DateTo
        {
            get
            {
                if (ViewState["DateTo"] == null)
                    ViewState["DateTo"] = -1;
                return (DateTime)ViewState["DateTo"];
            }
            set
            {
                ViewState["DateTo"] = value;
            }
        }

        string partycode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                partycode = string.IsNullOrEmpty(Request.QueryString["PartyCode"]) ? null : (Request.QueryString["partycode"]).ToString();
                lblamount.Text = Request.QueryString["amountBag"];
                if (partycode == null)
                {
                    DateFrom = string.IsNullOrEmpty(Request.QueryString["FDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["FDate"]);
                    DateTo = string.IsNullOrEmpty(Request.QueryString["TDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["TDate"]);
                    lblfrom.Text = DateFrom.Day + "/" + DateFrom.Month + "/" + DateFrom.Year;
                    lblto.Text = DateTo.Day + "/" + DateTo.Month + "/" + DateTo.Year;

                    dsParty.SelectCommand = @"select DISTINCT sr.PartyID as pid, INVParty.PartyCode,INVParty.PartyName
                            from SRVRegistration as sr
                            INNER JOIN INVParty on sr.PartyID=INVParty.PartyID 
                            where sr.Requisitioned='Applied For Loan' order by PartyCode asc; ";
                    grvPreRequisition.DataBind();
                }

                else
                {
                    DateFrom = string.IsNullOrEmpty(Request.QueryString["FDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["FDate"]);
                    DateTo = string.IsNullOrEmpty(Request.QueryString["TDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["TDate"]);
                    lblfrom.Text = DateFrom.Day + "/" + DateFrom.Month + "/" + DateFrom.Year;
                    lblto.Text = DateTo.Day + "/" + DateTo.Month + "/" + DateTo.Year;
                    dsParty.SelectCommand = @"select DISTINCT sr.PartyID as pid, INVParty.PartyCode,INVParty.PartyName
                            from SRVRegistration as sr
                            INNER JOIN INVParty on sr.PartyID=INVParty.PartyID 
                            where sr.Requisitioned='Applied For Loan' and PartyCode=N'" + partycode + "' order by PartyCode asc;";
                    grvPreRequisition.DataBind();
                }
            }
        }
        #region method for grid
        protected void grvPreRequisition_changing(object sender, GridViewPageEventArgs e)
        {
            grvPreRequisition.PageIndex = e.NewPageIndex;
            grvPreRequisition.DataBind();
        }
        int bagscnt = 0;
        protected void grvAppliedBags_bound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                
                Label lbltotapplied = (Label)e.Row.FindControl("lbltotapplied");
                if (lbltotapplied != null)
                    lbltotapplied.Text = (int.Parse((DataBinder.Eval(e.Row.DataItem, "applied")).ToString()) * float.Parse((Request.QueryString["amountBag"]).ToString())).ToString();
                bagscnt += int.Parse((DataBinder.Eval(e.Row.DataItem, "applied")).ToString());
                lblsummary.Text = "মোট " + bagscnt + " বস্তা আবেদন করেছে " + (float.Parse((Request.QueryString["amountBag"]).ToString())*bagscnt)+" টাকার জন্য";
            }
        }
        protected void grvPreRequisition_bound(object sender, GridViewRowEventArgs e)
        {


            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {

                int pid = int.Parse(DataBinder.Eval(e.Row.DataItem, "pid").ToString());


                DataTable prevloan = new LoanRequisition().getPrevloan(pid);
                //DataTable afterloan = new LoanRequisition().getPrevloan(pid);
                GridView grvPrevLoan = (GridView)e.Row.FindControl("grvPrevLoan");
                grvPrevLoan.DataSource = prevloan;
                grvPrevLoan.DataBind();

                GridView grvAppliedBags = (GridView)e.Row.FindControl("grvAppliedBags");
                DataTable app = new LoanRequisition().getAppliedBags(pid, DateFrom, DateTo);
                grvAppliedBags.DataSource = app;
                grvAppliedBags.DataBind();


                IList<Int32> prevbags = new LoanRequisition().getApprovedBags(pid);
                DataTable dt = new DataTable("Rows");
                dt.Columns.Add(" ", typeof(string));
                dt.Rows.Add(new string[1] { 0.ToString() });
                foreach (Int32 r in prevbags)
                {
                    dt.Rows.Add(new string[1] { new LoanRequisition().getBalance(pid, r).ToString() });
                }
                GridView grdBalance = (GridView)e.Row.FindControl("grdBalance");
                grdBalance.DataSource = dt;
                grdBalance.DataBind();

            }
        }

        //protected void grvAppliedBags_bound(object sender, GridViewRowEventArgs e)
        //{
        //    if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[0].Controls.Count > 0))
        //    {
        //        loanApplied += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "applied"));
        //    }
        //    else if (e.Row.RowType == DataControlRowType.Footer)
        //    {
        //        Label lblLoanApplied = (Label)e.Row.FindControl("lblLoanApplied");
        //        lblLoanApplied.Text = String.Format("{0:N}", loanApplied);
        //    }
        //}
        #endregion
    }
}