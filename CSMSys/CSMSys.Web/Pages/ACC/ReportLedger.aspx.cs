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
    public partial class ReportLedger : System.Web.UI.Page
    {
        #region Private Properties
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;
        private decimal TotalDebit = (decimal)0.0;
        private decimal TotalCredit = (decimal)0.0;
        private decimal TotalBalance = (decimal)0.0;

        SqlConnection formCon = null;

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

        private int AccountID
        {
            get
            {
                if (ViewState["AccountID"] == null)
                    ViewState["AccountID"] = -1;
                return (int)ViewState["AccountID"];
            }
            set
            {
                ViewState["AccountID"] = value;
            }
        }

        DaTransaction objDaTrans = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateFrom = DateTime.Today;
                txtDateFrom.Text = DateFrom.ToShortDateString();
                DateTo = DateTime.Today;
                txtDateTo.Text = DateTo.ToShortDateString();
                loadParentAccounts();
            }
        }

        #region Method for Load
        private void loadParentAccounts()
        {
            formCon = new SqlConnection(connstring);
            try
            {
                ddlParentLedger.DataSource = new DaAccount().GetAccounts(formCon);
                ddlParentLedger.DataTextField = "AccountTitle";
                ddlParentLedger.DataValueField = "AccountID";
                ddlParentLedger.DataBind();
                ddlParentLedger.Items.Insert(0, new ListItem("   <-- Select Account -->", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadLedgerInfo()
        {
            formCon = new SqlConnection(connstring);
            formCon.Open();
            try
            {
                using (SqlCommand com = new SqlCommand("spRptOpeningBalanceOfAccount", formCon))
                {
                    DataTable dt = new DataTable();

                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@UpToDate", SqlDbType.DateTime).Value = DateTo;
                    com.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountID;

                    SqlDataAdapter _dap = new SqlDataAdapter(com);

                    _dap.Fill(dt);

                    //lblOpBalance.Text = (string)com.ExecuteScalar();
                }

                grvLedger.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void getAccountInfo(int accountID)
        {
            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountNo], [AccountTitle], [Nature], [OpeningBalance] FROM [T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows)
                    {
                        lblAccountTitle.Text = sqlReader["AccountTitle"].ToString();
                        lblAccountNo.Text = sqlReader["AccountNo"].ToString();
                        int nature = int.Parse(sqlReader["Nature"].ToString());
                        double opBalance = double.Parse(sqlReader["OpeningBalance"].ToString());
                        opBalance = opBalance * (float)nature;
                        lblOpBalance.Text = opBalance.ToString();
                        if (nature <= 0)
                        {
                            lblType.Text = "Cr";
                        }
                        else
                        {
                            lblType.Text = "Dr";
                        }
                    }
                }

                sqlReader.Close();
                sqlConn.Close();
            }
        }

        private void getClosingBalance(int accountID)
        {
            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT AccountID, SUM(DebitAmt - CreditAmt) AS ClosingBalance FROM dbo.T_Transaction_Detail GROUP BY AccountID HAVING ([AccountID] = " + accountID + ")";
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows)
                    {
                        double opBalance = double.Parse(lblOpBalance.Text.ToString());
                        double clBalance = double.Parse(sqlReader["ClosingBalance"].ToString());
                        lblClBalance.Text = (opBalance + clBalance).ToString();
                    }
                }

                sqlReader.Close();
                sqlConn.Close();
            }
        }
        #endregion

        #region Method for Dr Accounts
        protected void grvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            NumberToEnglish n2e = new NumberToEnglish();
            NumberToBangla n2b = new NumberToBangla();

            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmt"));
                TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CreditAmt"));
                TotalBalance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "LineTotal"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebitTotal = (Label)e.Row.FindControl("lblDebitTotal");
                //lblDebitTotal.Text = String.Format("{0:N}", TotalDebit);

                Label lblCreditTotal = (Label)e.Row.FindControl("lblCreditTotal");
                //lblCreditTotal.Text = String.Format("{0:N}", TotalCredit);

                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = String.Format("{0:N}", TotalBalance);

                //Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                //lblDebit.Text = "কথায় : টাকা " + n2b.changeCurrencyToWords(TotalDebit.ToString()).ToString();
            }
        }
        #endregion

        #region SqlDataSource Control Event Handlers
        protected void dsLedger_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@AccountID"].Value = AccountID;
            e.Command.Parameters["@DateFrom"].Value = DateFrom;
            e.Command.Parameters["@DateTo"].Value = DateTo;
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
            DateTo = DateTime.Parse(txtDateTo.Text.ToString());
            AccountID = ddlParentLedger.SelectedIndex >= 0 ? int.Parse(ddlParentLedger.SelectedValue.ToString()) : 0;

            getAccountInfo(AccountID);
            getClosingBalance(AccountID);

            lblDateFrom.Text = DateFrom.ToShortDateString();
            lblDateTo.Text = DateTo.ToShortDateString();
            grvLedger.DataBind();
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            //txtSearch.Text = string.Empty;
            //grvAttendance.DataBind();
        }
        #endregion
    }
}