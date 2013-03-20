using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CSMSys.Lib.Manager.RptShad;
using System.Data;

namespace CSMSys.Web.HtmlReports
{
    public partial class LoanAfterRequisition : System.Web.UI.Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateFrom = string.IsNullOrEmpty(Request.QueryString["FDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["FDate"]);
                DateTo = string.IsNullOrEmpty(Request.QueryString["TDate"]) ? DateTime.Today : DateTime.Parse(Request.QueryString["TDate"]);
            }
        }
        #region for grid
        protected void grvPreRequisition_bound(object sender, GridViewRowEventArgs e)
        {


            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {

                int pid = int.Parse(DataBinder.Eval(e.Row.DataItem, "pid").ToString());


                DataTable prevloan = new LoanRequisition().getPrevloan(pid);
                DataTable afterloan = new LoanRequisition().getPrevloan(pid);
                prevloan.Rows.RemoveAt(prevloan.Rows.Count - 1);
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

                if (afterloan.Rows.Count > 0)
                    afterloan.Rows.RemoveAt(0);
                GridView grvAfterLoan = (GridView)e.Row.FindControl("grvAfterLoan");
                grvAfterLoan.DataSource = afterloan;
                grvAfterLoan.DataBind();



            }
        }
        int bagscnt = 0;
        protected void grvAfterLoan_bound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {

                Label lbltotapplied = (Label)e.Row.FindControl("lblAfterTotAmount");
                if (lbltotapplied != null)
                    lbltotapplied.Text = (int.Parse((DataBinder.Eval(e.Row.DataItem, "Bags")).ToString()) * float.Parse((DataBinder.Eval(e.Row.DataItem, "amount")).ToString())).ToString();
                bagscnt += int.Parse((DataBinder.Eval(e.Row.DataItem, "Bags")).ToString());
                lblsummary.Text = "মোট " + bagscnt + " বস্তার লোন দেয়া হয়েছে " + (float.Parse((DataBinder.Eval(e.Row.DataItem, "amount")).ToString()) * bagscnt) + " টাকা।";
            }
        }
        #endregion
    }
}