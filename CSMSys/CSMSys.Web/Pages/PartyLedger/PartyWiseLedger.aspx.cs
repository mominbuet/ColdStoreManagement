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

namespace CSMSys.Web.Pages.PartyLedger
{
    public partial class PartyWiseLedger : System.Web.UI.Page
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
            helper.RegisterSummary("BagLoaded", SummaryOperation.Sum);
            helper.RegisterSummary("BagNumber", SummaryOperation.Sum);
            helper.RegisterSummary("BagLoan", SummaryOperation.Sum);
            helper.RegisterSummary("CarryingLoan", SummaryOperation.Sum);
            helper.RegisterSummary("LoanAmount", SummaryOperation.Sum);
            helper.RegisterSummary("TotalLoan", SummaryOperation.Sum);
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Today.ToString("dd/MM/yyyy"); 
                int intPartyID = Convert.ToInt32(Request.QueryString["PID"]);
                DateTime parseFromDate = Convert.ToDateTime(Request.QueryString["FD"]);
                string fromDate = parseFromDate.ToShortDateString();
                DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
                string toDate = parseToDate.ToShortDateString();
                lblFromDate.Text = fromDate;
                lblToDate.Text = toDate;
                LoadAllValue(intPartyID);
            }
        }
        
        #region Method for Load
            public void LoadAllValue(int intPartyID)
            {
                 
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);


            cnn.Open();
            string PartyQuery = "exec SP_Party_Information " + intPartyID;
            SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
            dsPartyInfo ds = new dsPartyInfo();
            daItem.Fill(ds, "PartyDetails");

            lblCode.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[1].ToString();
            lblName.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[2].ToString();
            lblFName.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[3].ToString();
            lblPartyType.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[9].ToString();
            lblVillage.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[5].ToString();
            lblPO.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[6].ToString();
            lblUpazilla.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[7].ToString();
            lblDistrict.Text = ds.Tables["PartyDetails"].Rows[0].ItemArray[8].ToString();
            cnn.Close();
            }
        #endregion

            protected void grvSerialRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                grvSerialRegister.PageIndex = e.NewPageIndex;
                grvSerialRegister.DataBind();
            }

       }
}