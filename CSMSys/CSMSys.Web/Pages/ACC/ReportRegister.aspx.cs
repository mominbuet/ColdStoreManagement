using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Pages.ACC
{
    public partial class ReportRegister : System.Web.UI.Page
    {
        private decimal TotalDebit = (decimal)0.0;
        private decimal TotalCredit = (decimal)0.0;

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
                DateFrom = DateTime.Today;
                txtDateFrom.Text = DateFrom.ToShortDateString();
                DateTo = DateTime.Today;
                txtDateTo.Text = DateTo.ToShortDateString();
                grvRegister.DataBind();
            }
        }

        #region Method for Dr Accounts
        protected void grvRegister_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmt"));
                TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CreditAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebitTotal = (Label)e.Row.FindControl("lblDebitTotal");
                lblDebitTotal.Text = String.Format("{0:N}", TotalDebit);

                Label lblCreditTotal = (Label)e.Row.FindControl("lblCreditTotal");
                lblCreditTotal.Text = String.Format("{0:N}", TotalCredit);
            }
        }
        #endregion

        protected void grvRegister_PreRender(object sender, EventArgs e)
        {
            //MergeGridCels.MergeRows(grvRegister);
        }

        #region SqlDataSource Control Event Handlers
        protected void dsRegister_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@DateFrom"].Value = DateFrom;
            e.Command.Parameters["@DateTo"].Value = DateTo;
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
            DateTo = DateTime.Parse(txtDateTo.Text.ToString());
            grvRegister.DataBind();
        }
        #endregion
    }
}