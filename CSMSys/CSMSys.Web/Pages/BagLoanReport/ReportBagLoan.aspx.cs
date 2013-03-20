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
using System.Globalization;

namespace CSMSys.Web.Pages.BagLoanReport
{
    public partial class ReportBagLoan : System.Web.UI.Page
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
                //lblDate.Text = DateTime.Today.ToString("dd/MM/yyyy"); 
                int intPartyID = Convert.ToInt32(Request.QueryString["PID"]);
                int bagLoanID = Convert.ToInt32(Request.QueryString["BID"]);
                //VoucherType = string.IsNullOrEmpty(Request.QueryString["Voucher"]) ? 1 : int.Parse(Request.QueryString["Voucher"]);
                //TransactionID = string.IsNullOrEmpty(Request.QueryString["TMID"]) ? 0 : int.Parse(Request.QueryString["TMID"]);
                //LoadValue()
                LoadAllValue(intPartyID, bagLoanID);
            }
        }

        #region Method for Load
        public void LoadAllValue(int intPartyID, int bagLoanID)
        {

            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);


            cnn.Open();
            string PartyQuery = "exec SP_Party_Information " + intPartyID;
            SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
            dsBagLoan ds = new dsBagLoan();
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


            cnn.Open();
            string query = "exec SP_Bag_LoanReport " + bagLoanID;
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            //dsPartyReport ds = new dsPartyReport();
            dscmd.Fill(ds, "SRVBagLoan");
            lblBagLoanID.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[0].ToString();
            lblDate.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[10].ToString();
                // lblTotalLoan.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[9].ToString();
                //  lblLoanPerBag.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[10].ToString();
            lblBagNo.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[1].ToString();
            lblBag.Text = ds.Tables["SRVBagLoan"].Rows[0].ItemArray[1].ToString();
            cnn.Close();

        }
        #endregion

    }
}