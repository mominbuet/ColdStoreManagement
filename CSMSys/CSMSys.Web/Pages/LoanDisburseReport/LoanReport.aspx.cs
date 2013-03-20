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

namespace CSMSys.Web.Pages.LoanDisburseReport
{
    public partial class LoanReport : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                lblDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                int intCaseID = Convert.ToInt32(Request.QueryString["caseID"]);
                LoadAllValue(intCaseID);
            }
        }
        
        #region Method for Load
        public void LoadAllValue(int intCaseID)
            {
                 
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);


            cnn.Open();
            string PartyQuery = "exec SP_Loan_Disburse " + intCaseID;
            SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
            dsLoanDisburse ds = new dsLoanDisburse();
            daItem.Fill(ds, "LoanDisburseData");

            lblPartyCode.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[1].ToString();
            lblName.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[2].ToString();
            lblFName.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[3].ToString();
            lblVillage.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[5].ToString();
            lblPO.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[6].ToString();
            lblUpazilla.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[7].ToString();
            lblDistrict.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[8].ToString();
            lblLoanID.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[9].ToString();
            lblCaseID.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[10].ToString();
            lblSerialNo.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[11].ToString();
            lblBagNo.Text = ds.Tables["LoanDisburseData"].Rows[0].ItemArray[12].ToString();

            cnn.Close();
             }
        #endregion

       }
}