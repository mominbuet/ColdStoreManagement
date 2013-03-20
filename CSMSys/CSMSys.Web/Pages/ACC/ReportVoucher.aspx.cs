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

namespace CSMSys.Web.Pages.ACC
{
    public partial class ReportVoucher : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                VoucherType = string.IsNullOrEmpty(Request.QueryString["Voucher"]) ? 1 : int.Parse(Request.QueryString["Voucher"]);
                TransactionID = string.IsNullOrEmpty(Request.QueryString["TMID"]) ? 0 : int.Parse(Request.QueryString["TMID"]);
                if (VoucherType == 1)
                {
                    MultiViewVoucher.ActiveViewIndex = 0;
                    lblVoucher.Text = "ডেবিট ভাউচার";

                    if (TransactionID > 0)
                    {
                        loadDrTabAccountGrid(TransactionID);
                    }
                }
                else if (VoucherType == 2)
                {
                    MultiViewVoucher.ActiveViewIndex = 1;
                    lblVoucher.Text = "ক্রেডিট ভাউচার";

                    if (TransactionID > 0)
                    {
                        loadCrTabAccountGrid(TransactionID);
                    }
                }
                else if (VoucherType == 3)
                {
                    MultiViewVoucher.ActiveViewIndex = 2;
                    lblVoucher.Text = "জার্নাল ভাউচার";

                    if (TransactionID > 0)
                    {
                        loadJrTabAccountGrid(TransactionID);
                    }
                }
            }
            Page.Title = lblVoucher.Text;
        }
        
        #region Method for Load
        private void loadCrTabAccountGrid(int TMID)
        {
            formCon = new SqlConnection(connstring);

            try
            {
                objDaTrans = new DaTransaction();
                TransactionMaster objTransM = objDaTrans.getTransMaster(formCon, TMID);

                if (objTransM != null)
                {
                    lblCrVoucherNo.Text = objTransM.VoucherNo;
                    lblCrDate.Text = objTransM.TransactionDate.ToShortDateString();
                    if (objTransM.TransactionMethodID == 1)
                    {
                        lblCrPayMethod.Text = "Cash";
                    }
                    else if (objTransM.TransactionMethodID == 2)
                    {
                        lblCrPayMethod.Text = "Cheque";
                    }
                    else if (objTransM.TransactionMethodID == 3)
                    {
                        lblCrPayMethod.Text = "DD/TT";
                    }
                    else if (objTransM.TransactionMethodID == 4)
                    {
                        lblCrPayMethod.Text = "LC";
                    }
                    lblCrAccount.Text = getAccountTitle(getAccountID(TMID));
                    lblCrAccountNo.Text = getAccountNo(getAccountID(TMID));

                    lblCrRefNo.Text = objTransM.MethodRefNo;
                    lblCrDesc.Text = objTransM.TransactionDescription;

                    lblCrAppvBy.Text = objTransM.ApprovedBy;
                    if (objTransM.ApprovedDate != new DateTime(1900, 1, 1))
                    {
                        lblCrAppvDate.Text = objTransM.ApprovedDate.ToShortDateString();
                    }
                }
                else
                {
                    lblCrVoucherNo.Text = string.Empty;
                    lblCrAccount.Text = string.Empty;
                    lblCrAccountNo.Text = string.Empty;
                    lblCrDesc.Text = string.Empty;
                    lblCrAppvBy.Text = string.Empty;
                    lblCrAppvDate.Text = string.Empty;

                    TransactionID = 0;
                    lblCrPayMethod.Text = "0";
                }
                grvCrAccount.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void loadDrTabAccountGrid(int TMID)
        {
            formCon = new SqlConnection(connstring);

            try
            {
                objDaTrans = new DaTransaction();
                TransactionMaster objTransM = objDaTrans.getTransMaster(formCon, TMID);

                if (objTransM != null)
                {
                    lblDrVoucherNo.Text = objTransM.VoucherNo;
                    lblDrDate.Text = objTransM.TransactionDate.ToShortDateString();
                    if (objTransM.TransactionMethodID == 1)
                    {
                        lblDrPayMethod.Text = "Cash";
                    }
                    else if (objTransM.TransactionMethodID == 2)
                    {
                        lblDrPayMethod.Text = "Cheque";
                    }
                    else if (objTransM.TransactionMethodID == 3)
                    {
                        lblDrPayMethod.Text = "DD/TT";
                    }
                    else if (objTransM.TransactionMethodID == 4)
                    {
                        lblDrPayMethod.Text = "LC";
                    }
                    lblDrAccount.Text = getAccountTitle(getAccountID(TMID));
                    lblDrAccountNo.Text = getAccountNo(getAccountID(TMID));

                    lblDrRefNo.Text = objTransM.MethodRefNo;
                    lblDrDesc.Text = objTransM.TransactionDescription;

                    lblDrAppvBy.Text = objTransM.ApprovedBy;
                    if (objTransM.ApprovedDate != new DateTime(1900, 1, 1))
                    {
                        lblDrAppvDate.Text = objTransM.ApprovedDate.ToShortDateString();
                    }
                }
                else
                {
                    lblDrVoucherNo.Text = string.Empty;
                    lblDrAccount.Text = string.Empty;
                    lblDrAccountNo.Text = string.Empty;
                    lblDrDesc.Text = string.Empty;
                    lblDrAppvBy.Text = string.Empty;
                    lblDrAppvDate.Text = string.Empty;

                    TransactionID = 0;
                    lblDrPayMethod.Text = "0";
                }

                grvDrAccount.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void loadJrTabAccountGrid(int TMID)
        {
            formCon = new SqlConnection(connstring);

            try
            {
                objDaTrans = new DaTransaction();
                TransactionMaster objTransM = objDaTrans.getTransMaster(formCon, TMID);

                if (objTransM != null)
                {
                    lblJrVoucherNo.Text = objTransM.VoucherNo;
                    lblJrDate.Text = objTransM.TransactionDate.ToShortDateString();
                    int trnsMethod = string.IsNullOrEmpty(objTransM.TransactionMethodID.ToString()) ? 0 : objTransM.TransactionMethodID;
                    if (trnsMethod == 1)
                    {
                        lblJrPayMethod.Text = "Cash";
                    }
                    else if (trnsMethod == 2)
                    {
                        lblJrPayMethod.Text = "Cheque";
                    }
                    else if (trnsMethod == 3)
                    {
                        lblJrPayMethod.Text = "DD/TT";
                    }
                    else if (trnsMethod == 4)
                    {
                        lblJrPayMethod.Text = "LC";
                    }
                    else
                    {
                        lblJrPayMethod.Text = "Account";
                    }
                    lblJrAccount.Text = getAccountTitle(getAccountID(TMID));
                    lblJrAccountNo.Text = getAccountNo(getAccountID(TMID));
                    lblJrRefNo.Text = objTransM.MethodRefNo;
                    lblJrDesc.Text = objTransM.TransactionDescription;

                    lblJrAppvBy.Text = objTransM.ApprovedBy;
                    if (objTransM.ApprovedDate != new DateTime(1900, 1, 1))
                    {
                        lblJrAppvDate.Text = objTransM.ApprovedDate.ToShortDateString();
                    }
                }
                else
                {
                    lblJrVoucherNo.Text = string.Empty;
                    lblJrAccount.Text = string.Empty;
                    lblJrAccountNo.Text = string.Empty;
                    lblJrDesc.Text = string.Empty;
                    lblJrAppvBy.Text = string.Empty;
                    lblJrAppvDate.Text = string.Empty;

                    TransactionID = 0;
                    lblJrPayMethod.Text = "0";
                }

                grvJrAccount.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getAccountNo(int accountID)
        {
            string accountNo = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountNo] FROM [T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) accountNo = sqlReader["AccountNo"].ToString();
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return accountNo;
        }

        private string getAccountTitle(int accountID)
        {
            string account = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountTitle] FROM [T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) account = sqlReader["AccountTitle"].ToString();
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return account;
        }

        private int getAccountID(int TMID)
        {
            int accountID = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountID] FROM [T_Transaction_Detail] WHERE [TransMID] = " + TMID + " ORDER BY [TransDID]";
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) accountID = int.Parse(sqlReader["AccountID"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return accountID;
        }
        #endregion

        #region Method for Dr Accounts
        protected void grvDrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NumberToEnglish n2e = new NumberToEnglish();
            NumberToBangla n2b = new NumberToBangla();

            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebitTotal = (Label)e.Row.FindControl("lblDebitTotal");
                lblDebitTotal.Text = String.Format("{0:N}", TotalDebit);

                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                lblDebit.Text = "কথায় : টাকা " + n2b.changeCurrencyToWords(TotalDebit.ToString()).ToString();
            }
        }
        #endregion

        #region Method for Cr Accounts
        protected void grvCrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NumberToEnglish n2e = new NumberToEnglish();
            NumberToBangla n2b = new NumberToBangla();

            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CreditAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblCreditTotal = (Label)e.Row.FindControl("lblCreditTotal");
                lblCreditTotal.Text = String.Format("{0:N}", TotalCredit);

                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                lblCredit.Text = "কথায় : টাকা " + n2b.changeCurrencyToWords(TotalCredit.ToString()).ToString();
            }
        }
        #endregion

        #region Method for Jr Accounts
        protected void grvJrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CreditAmt"));
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblCredit = (Label)e.Row.FindControl("lblCreditTotal");
                lblCredit.Text = String.Format("{0:N}", TotalCredit);

                Label lblDebit = (Label)e.Row.FindControl("lblDebitTotal");
                lblDebit.Text = String.Format("{0:N}", TotalDebit);
            }
        }
        #endregion

        #region SqlDataSource Control Event Handlers
        protected void dsDrAccount_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@TransMID"].Value = TransactionID;
        }

        protected void dsCrAccount_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@TransMID"].Value = TransactionID;
        }

        protected void dsJrAccount_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@TransMID"].Value = TransactionID;
        }
        #endregion
    }
}