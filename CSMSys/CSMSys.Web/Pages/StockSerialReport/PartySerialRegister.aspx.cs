using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Web.Pages.BagLoanRreport;

namespace CSMSys.Web.Pages.StockSerialReport
{
    public partial class PartySerialRegister : System.Web.UI.Page
    {
        #region Private Properties
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;
        private decimal TotalDebit = (decimal)0.0;
        private decimal TotalCredit = (decimal)0.0;

        SqlConnection formCon = null;

        private int VoucherType
        {
            get
            {
                if (ViewState["VoucherType"] == null)
                    ViewState["VoucherType"] = -1;
                return (int)ViewState["VoucherType"];
            }
            set
            {
                ViewState["VoucherType"] = value;
            }
        }

        private int TransactionID
        {
            get
            {
                if (ViewState["TransactionID"] == null)
                    ViewState["TransactionID"] = -1;
                return (int)ViewState["TransactionID"];
            }
            set
            {
                ViewState["TransactionID"] = value;
            }
        }

        DaTransaction objDaTrans = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Target = "_blank";
            MultiViewBagLoan.ActiveViewIndex = 0;

            GridViewHelper helper = new GridViewHelper(this.grvSerialRegister);
            helper.RegisterGroup("SerialDate", true, true);
            helper.RegisterSummary("Bags", SummaryOperation.Sum, "SerialDate");
            helper.RegisterSummary("Bags", SummaryOperation.Sum);
            helper.ApplyGroupSort();

            if (!IsPostBack)
            {

                DateTime parseFromDate = Convert.ToDateTime(Request.QueryString["FD"]);
                string fromDate = parseFromDate.ToShortDateString();
                DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
                string toDate = parseToDate.ToShortDateString();
                lblFromDate.Text = fromDate;
                lblToDate.Text = toDate;                       
            }
        }

       
        #region Method for Load
        public void LoadDataGrid(string fromDate, string toDate)
            {


            }
        #endregion

        protected void grvSerialRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvSerialRegister.PageIndex = e.NewPageIndex;
            grvSerialRegister.DataBind();
        }



       }
}