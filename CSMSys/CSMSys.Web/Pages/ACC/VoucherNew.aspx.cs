using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Pages.ACC
{
    public partial class VoucherNew : System.Web.UI.Page
    {
        #region Private Properties
        private string strSearch = string.Empty;
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;

        SqlConnection formCon = null;
        bool _isEditable = true;
        private decimal TotalDebit = (decimal)0.0;
        private decimal TotalCredit = (decimal)0.0;

        private enum UIMODE
        {
            NEW,
            EDIT
        }

        private UIMODE UIMode
        {
            get
            {
                if (ViewState["UIMODE"] == null)
                    ViewState["UIMODE"] = new UIMODE();
                return (UIMODE)ViewState["UIMODE"];
            }
            set
            {
                ViewState["UIMODE"] = value;
            }
        }

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
        //ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string voucher = string.Empty;
                VoucherType = string.IsNullOrEmpty(Request.QueryString["Voucher"]) ? 1 : int.Parse(Request.QueryString["Voucher"]);
                TransactionID = string.IsNullOrEmpty(Request.QueryString["TMID"]) ? 0 : int.Parse(Request.QueryString["TMID"]);
                if (VoucherType == 1)
                {
                    MultiViewVoucher.ActiveViewIndex = 0;
                    lblVoucher.Text = "Debit Voucher";

                    if (TransactionID <= 0)
                    {
                        txtDrDate.Text = DateTime.Today.ToShortDateString();

                        txtDrVoucherNo.Text = getVoucherNo(1);
                    }
                    else
                    {
                        loadDrTabAccountGrid(TransactionID);
                    }
                }
                else if (VoucherType == 2)
                {
                    MultiViewVoucher.ActiveViewIndex = 1;
                    lblVoucher.Text = "Credit Voucher";

                    if (TransactionID <= 0)
                    {
                        txtCrDate.Text = DateTime.Today.ToShortDateString();

                        txtCrVoucherNo.Text = getVoucherNo(2);
                    }
                    else
                    {
                        loadCrTabAccountGrid(TransactionID);
                    }
                }
                else if (VoucherType == 3)
                {
                    MultiViewVoucher.ActiveViewIndex = 2;
                    lblVoucher.Text = "Journal Voucher";
                    txtJrDate.Text = DateTime.Today.ToShortDateString();

                    txtJrVoucherNo.Text = getVoucherNo(3);
                }
                else
                {
                    MultiViewVoucher.ActiveViewIndex = 0;
                    lblVoucher.Text = "Debit Voucher";
                    txtDrDate.Text = DateTime.Today.ToShortDateString();

                    txtDrVoucherNo.Text = getVoucherNo(1);
                }

                hdnTMID.Value = "0";
            }
            Page.Title = lblVoucher.Text;
        }

        #region Methods
        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtSearchDrPayment.Text;
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

        private string getAccountTitle(int accountID)
        {
            string accountTitle = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountTitle] FROM [T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) accountTitle = sqlReader["AccountTitle"].ToString();
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return accountTitle;
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

        private int getTransDetailID(int TMID, int accountID)
        {
            int intTDID = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [TransDID] FROM [T_Transaction_Detail] WHERE [AccountID] = " + accountID + " AND [TransMID] = " + TMID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) intTDID = int.Parse(sqlReader["TransDID"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return intTDID;
        }

        private string getVoucherNo(int voucherType)
        {
            string voucherNo = string.Empty;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [VoucherNo] FROM [T_Transaction_Master] WHERE [VoucherType] = " + voucherType + " AND [TransMID] = (SELECT MAX([TransMID]) FROM [T_Transaction_Master] WHERE [VoucherType] = " + voucherType + ")";
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) voucherNo = sqlReader["VoucherNo"].ToString();

                    if (!string.IsNullOrEmpty(voucherNo))
                    {
                        int voucher = int.Parse(voucherNo.Substring(voucherNo.Length - 5)) + 1;
                        if (voucherType == 1) voucherNo = "D" + voucher.ToString("00000");
                        if (voucherType == 2) voucherNo = "C" + voucher.ToString("00000");
                        if (voucherType == 3) voucherNo = "J" + voucher.ToString("00000");
                    }
                    else
                    {
                        if (voucherType == 1) voucherNo = "D00001";
                        if (voucherType == 2) voucherNo = "C00001";
                        if (voucherType == 3) voucherNo = "J00001";
                    }
                }
                else
                {
                    if (voucherType == 1) voucherNo = "D00001";
                    if (voucherType == 2) voucherNo = "C00001";
                    if (voucherType == 3) voucherNo = "J00001";
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return voucherNo;
        }

        private float getTransDebitTotal(int TMID)
        {
            float drTotal = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT SUM([DebitAmt]) AS [DebitAmt] FROM [T_Transaction_Detail] WHERE [TransMID] = " + TMID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) drTotal = float.Parse(sqlReader["DebitAmt"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return drTotal;
        }
        #endregion

        #region Method for Load
        private void loadCrTabAccountGrid(int TMID)
        {
            formCon = new SqlConnection(connstring);

            try
            {
                objDaTrans = new DaTransaction();


                //////////////////////
                TransactionMaster objTransM = objDaTrans.getTransMaster(formCon, TMID);

                if (objTransM != null)
                {
                    hdnTMID.Value = objTransM.TransactionMasterID.ToString();
                    txtCrVoucherNo.Text = objTransM.VoucherNo;
                    txtCrDate.Text = objTransM.TransactionDate.ToShortDateString();
                    ddlCollMethod.SelectedValue = objTransM.TransactionMethodID.ToString();
                    txtCrRefNo.Text = objTransM.MethodRefNo;
                    txtCrDesc.Text = objTransM.TransactionDescription;

                    txtCrAppvBy.Text = objTransM.ApprovedBy;
                    if (objTransM.ApprovedDate == new DateTime(1900, 1, 1))
                    {
                        chkCrAppvBy.Checked = false;
                    }
                    else
                    {
                        chkCrAppvBy.Checked = true;
                        txtCrAppvDate.Text = objTransM.ApprovedDate.ToShortDateString();
                    }
                }
                else
                {
                    txtCrVoucherNo.Text = string.Empty;
                    txtReceiveFrom.Text = string.Empty;
                    txtReceiveAmount.Text = string.Empty;
                    txtCrAccount.Text = string.Empty;
                    txtCrAccountNo.Text = string.Empty;
                    txtCrDesc.Text = string.Empty;
                    txtCrAppvBy.Text = string.Empty;
                    txtCrAppvDate.Text = string.Empty;

                    hdnTMID.Value = "0";
                    TransactionID = 0;
                    hdnCrAccID.Value = "0";
                    hdnCrAccNo.Value = "0";
                    hdnDrAccID.Value = "0";
                    hdnDrAccNo.Value = "0";

                    ddlCollMethod.SelectedIndex = 0;
                    grvCrAccount.DataBind();

                    txtCrDate.Text = DateTime.Today.ToShortDateString();
                    txtCrVoucherNo.Text = getVoucherNo(2);

                }

                /////////////////////
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


                //////////////////////
                TransactionMaster objTransM = objDaTrans.getTransMaster(formCon, TMID);

                if (objTransM != null)
                {
                    hdnTMID.Value = objTransM.TransactionMasterID.ToString();
                    txtDrVoucherNo.Text = objTransM.VoucherNo;
                    txtDrDate.Text = objTransM.TransactionDate.ToShortDateString();
                    ddlPayMethod.SelectedValue = objTransM.TransactionMethodID.ToString();
                    txtDrRefNo.Text = objTransM.MethodRefNo;
                    txtDrDesc.Text = objTransM.TransactionDescription;

                    txtDrAppvBy.Text = objTransM.ApprovedBy;
                    if (objTransM.ApprovedDate == new DateTime(1900, 1, 1))
                    {
                        chkDrAppvBy.Checked = false;
                    }
                    else
                    {
                        chkDrAppvBy.Checked = true;
                        txtDrAppvDate.Text = objTransM.ApprovedDate.ToShortDateString();
                    }
                }
                else
                {
                    txtDrVoucherNo.Text = string.Empty;
                    txtReceiveFrom.Text = string.Empty;
                    txtReceiveAmount.Text = string.Empty;
                    txtDrAccount.Text = string.Empty;
                    txtDrAccountNo.Text = string.Empty;
                    txtDrDesc.Text = string.Empty;
                    txtDrAppvBy.Text = string.Empty;
                    txtDrAppvDate.Text = string.Empty;

                    hdnTMID.Value = "0";
                    TransactionID = 0;
                    hdnCrAccID.Value = "0";
                    hdnCrAccNo.Value = "0";
                    hdnDrAccID.Value = "0";
                    hdnDrAccNo.Value = "0";

                    ddlPayMethod.SelectedIndex = 0;
                    grvDrAccount.DataBind();

                    txtDrDate.Text = DateTime.Today.ToShortDateString();
                    txtDrVoucherNo.Text = getVoucherNo(2);

                }

                /////////////////////
                grvDrAccount.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Methods For Payment Grid
        protected void grvDrPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    hdnDrAccID.Value = intAccountID.ToString();
                    txtDrAccount.Text = getAccountTitle(intAccountID);
                    txtDrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
        }

        protected void grvDrPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvDrPayment.PageIndex = e.NewPageIndex;
            grvDrPayment.DataBind();
        }

        protected void grvDrPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("imgSelectDrPayment");
                btnSelect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Methods For VoucherFor Grid
        protected void grvVoucherFor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    hdnCrAccID.Value = intAccountID.ToString();
                    txtDrVoucherFor.Text = getAccountTitle(intAccountID);
                    hdnCrAccNo.Value = getAccountNo(intAccountID);
                }
            }
        }

        protected void grvVoucherFor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvVoucherFor.PageIndex = e.NewPageIndex;
            grvVoucherFor.DataBind();
        }

        protected void grvVoucherFor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("imgSelectVoucherFor");
                btnSelect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
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

        #region Method for Dr Accounts
        protected void grvDrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;
            }

            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalDebit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DebitAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebitTotal");
                lblDebit.Text = String.Format("{0:N}", TotalDebit);
            }
        }

        protected void grvDrAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    hdnDrAccID.Value = intAccountID.ToString();
                    txtDrAccount.Text = getAccountTitle(intAccountID);
                    txtDrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
            else if (e.CommandName.Equals("Delete"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    objDaTrans = new DaTransaction();
                    objDaTrans.DeleteTransDetail(formCon, intDID);
                    grvDrAccount.DataBind();
                }
            }
        }

        #endregion

        #region Methods For CrPayment Grid
        protected void grvCrPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    hdnCrAccID.Value = intAccountID.ToString();
                    txtCrAccount.Text = getAccountTitle(intAccountID);
                    txtCrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
        }

        protected void grvCrPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvCrPayment.PageIndex = e.NewPageIndex;
            grvCrPayment.DataBind();
        }

        protected void grvCrPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("imgSelectCrPayment");
                btnSelect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Methods For Receive Grid
        protected void grvReceive_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    hdnDrAccID.Value = intAccountID.ToString();
                    txtReceiveFrom.Text = getAccountTitle(intAccountID);
                    hdnDrAccNo.Value = getAccountNo(intAccountID);
                }
            }
        }

        protected void grvReceive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvReceive.PageIndex = e.NewPageIndex;
            grvReceive.DataBind();
        }

        protected void grvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("imgSelectReceive");
                btnSelect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Method for Cr Accounts
        protected void grvCrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;
            }

            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[6].Controls.Count > 0))
            {
                TotalCredit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CreditAmt"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblCredit = (Label)e.Row.FindControl("lblCreditTotal");
                lblCredit.Text = String.Format("{0:N}", TotalCredit);
            }
        }

        protected void grvCrAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    hdnDrAccID.Value = intAccountID.ToString();
                    txtDrAccount.Text = getAccountTitle(intAccountID);
                    txtDrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
            else if (e.CommandName.Equals("Delete"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    objDaTrans = new DaTransaction();
                    objDaTrans.DeleteTransDetail(formCon, intDID);
                    grvDrAccount.DataBind();
                }
            }
        }
        #endregion

        #region Methods For JrAccountNo Grid
        protected void grvJrAccountNo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                int intAccountID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAccountID > 0)
                {
                    hdnCrAccID.Value = intAccountID.ToString();
                    txtJrAccount.Text = getAccountTitle(intAccountID);
                    txtJrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
        }

        protected void grvJrAccountNo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvJrAccountNo.PageIndex = e.NewPageIndex;
            grvJrAccountNo.DataBind();
        }

        protected void grvJrAccountNo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnSelect = (ImageButton)e.Row.FindControl("imgSelectJrAccount");
                btnSelect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion

        #region Method for Jr Accounts
        protected void grvJrAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + "@" + ((Label)e.Row.Cells[3].Controls[1]).Text;
            }

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

                hdnCrTotal.Value = Convert.ToDecimal(lblCredit.Text).ToString();
                hdnDrTotal.Value = Convert.ToDecimal(lblDebit.Text).ToString();
            }
        }

        protected void grvJrAccount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    hdnDrAccID.Value = intAccountID.ToString();
                    txtDrAccount.Text = getAccountTitle(intAccountID);
                    txtDrAccountNo.Text = getAccountNo(intAccountID);
                }
            }
            else if (e.CommandName.Equals("Delete"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strDID = words[0].Trim().ToString();
                string strAccID = words[1].Trim().ToString();

                int intDID = Convert.ToInt32(strDID.ToString());
                int intAccountID = Convert.ToInt32(strAccID.ToString());

                if ((intAccountID > 0) && (intDID > 0))
                {
                    objDaTrans = new DaTransaction();
                    objDaTrans.DeleteTransDetail(formCon, intDID);
                    grvJrAccount.DataBind();
                }
            }
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearchDrPayment.Text;
        }

        protected void btnDrAdd_Click(object sender, EventArgs e)
        {
            if ((txtDrVoucherFor.Text == null) || (float.Parse(txtDrVoucherAmount.Text) <= 0)) return;

            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                TransactionMaster objTM = new TransactionMaster();
                TransactionDetail objTD = new TransactionDetail();

                objTM = CreateTransMasterObject("Debit");

                int intAccountID = string.IsNullOrEmpty(hdnCrAccID.Value) ? 0 : Convert.ToInt32(hdnCrAccID.Value);
                float fltAmount = string.IsNullOrEmpty(txtDrVoucherAmount.Text) ? 0 : Convert.ToInt32(txtDrVoucherAmount.Text);
                int intTDID = getTransDetailID(TransactionID, intAccountID);

                objTD = CreateTransDetailObject(intTDID, TransactionID, intAccountID, 0, Convert.ToDouble(fltAmount), txtDrAccount.Text);

                objDaTrans = new DaTransaction();
                //TMID = objDaTrans.SaveEditTransactionMaster(objTM, formCon, trans);

                TransactionID = objDaTrans.InsertUpdateTransactionMaster(objTM, formCon, objTD);
                hdnTMID.Value = TransactionID.ToString();

                txtDrVoucherFor.Text = string.Empty;
                txtDrVoucherAmount.Text = string.Empty;
                hdnCrAccNo.Value = "0";
                txtDrVoucherFor.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                formCon.Close();
                grvDrAccount.DataBind();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvDrAccount.DataBind();
            grvCrAccount.DataBind();
        }

        protected void btnDrPrint_Click(object sender, EventArgs e)
        {
            int intTMID = hdnTMID.Value != "0" ? int.Parse(hdnTMID.Value) : TransactionID;
            if (string.IsNullOrEmpty(hdnTMID.Value)) return;

            Response.Redirect("~/Pages/ACC/ReportVoucher.aspx?Voucher=1&TMID=" + intTMID);
        }

        protected void btnDrPost_Click(object sender, EventArgs e)
        {
            txtDrVoucherNo.Text = string.Empty;
            txtDrVoucherFor.Text = string.Empty;
            txtDrVoucherAmount.Text = string.Empty;
            txtDrAccount.Text = string.Empty;
            txtDrAccountNo.Text = string.Empty;
            txtDrDesc.Text = string.Empty;
            txtDrAppvBy.Text = string.Empty;
            txtDrAppvDate.Text = string.Empty;

            hdnTMID.Value = TransactionID.ToString();
            TransactionID = 0;
            hdnCrAccID.Value = "0";
            hdnCrAccNo.Value = "0";
            hdnDrAccID.Value = "0";
            hdnDrAccNo.Value = "0";

            ddlPayMethod.SelectedIndex = 0;
            grvDrAccount.DataBind();

            txtDrDate.Text = DateTime.Today.ToShortDateString();
            txtDrVoucherNo.Text = getVoucherNo(1);

            lblDrFailure.Text = "Posted Successfully";
        }

        protected void btnCrAdd_Click(object sender, EventArgs e)
        {
            if ((txtReceiveFrom.Text == null) || (float.Parse(txtReceiveAmount.Text) <= 0)) return;

            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                TransactionMaster objTM = new TransactionMaster();
                TransactionDetail objTD = new TransactionDetail();

                objTM = CreateTransMasterObject("Credit");

                int intAccountID = string.IsNullOrEmpty(hdnDrAccID.Value) ? 0 : Convert.ToInt32(hdnDrAccID.Value);
                float fltAmount = string.IsNullOrEmpty(txtReceiveAmount.Text) ? 0 : Convert.ToInt32(txtReceiveAmount.Text);
                int intTDID = getTransDetailID(TransactionID, intAccountID);

                objTD = CreateTransDetailObject(intTDID, TransactionID, intAccountID, Convert.ToDouble(fltAmount), 0, txtCrAccount.Text);


                objDaTrans = new DaTransaction();
                //TMID = objDaTrans.SaveEditTransactionMaster(objTM, formCon, trans);

                TransactionID = objDaTrans.InsertUpdateTransactionMaster(objTM, formCon, objTD);
                hdnTMID.Value = TransactionID.ToString();

                txtReceiveFrom.Text = string.Empty;
                txtReceiveAmount.Text = string.Empty;
                hdnDrAccNo.Value = "0";
                txtReceiveFrom.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                formCon.Close();
                grvCrAccount.DataBind();
            }
        }

        protected void btnCrPost_Click(object sender, EventArgs e)
        {
            txtCrVoucherNo.Text = string.Empty;
            txtReceiveFrom.Text = string.Empty;
            txtReceiveAmount.Text = string.Empty;
            txtCrAccount.Text = string.Empty;
            txtCrAccountNo.Text = string.Empty;
            txtCrDesc.Text = string.Empty;
            txtCrAppvBy.Text = string.Empty;
            txtCrAppvDate.Text = string.Empty;

            hdnTMID.Value = TransactionID.ToString();
            TransactionID = 0;
            hdnCrAccID.Value = "0";
            hdnCrAccNo.Value = "0";
            hdnDrAccID.Value = "0";
            hdnDrAccNo.Value = "0";

            ddlCollMethod.SelectedIndex = 0;
            grvCrAccount.DataBind();

            txtCrDate.Text = DateTime.Today.ToShortDateString();
            txtCrVoucherNo.Text = getVoucherNo(2);

            lblCrFailure.Text = "Posted Successfully";
        }

        protected void btnCrPrint_Click(object sender, EventArgs e)
        {
            int intTMID = hdnTMID.Value != "0" ? int.Parse(hdnTMID.Value) : TransactionID;
            if (string.IsNullOrEmpty(hdnTMID.Value)) return;

            Response.Redirect("~/Pages/ACC/ReportVoucher.aspx?Voucher=2&TMID=" + intTMID);
        }

        protected void btnJrAdd_Click(object sender, EventArgs e)
        {
            if (txtJrAccount.Text == null)
            {
                float crAmt = string.IsNullOrEmpty(txtCrAmount.Text) ? 0 : float.Parse(txtCrAmount.Text);
                float drAmt = string.IsNullOrEmpty(txtDrAmount.Text) ? 0 : float.Parse(txtDrAmount.Text);
                if ((crAmt <= 0) || (drAmt <= 0)) return;
            }

            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                TransactionMaster objTM = new TransactionMaster();
                TransactionDetail objTD = new TransactionDetail();

                objTM = CreateTransMasterObject("Journal");

                int intAccountID = string.IsNullOrEmpty(hdnCrAccID.Value) ? 0 : Convert.ToInt32(hdnCrAccID.Value);
                float fltCrAmount = string.IsNullOrEmpty(txtCrAmount.Text) ? 0 : Convert.ToInt32(txtCrAmount.Text);
                float fltDrAmount = string.IsNullOrEmpty(txtDrAmount.Text) ? 0 : Convert.ToInt32(txtDrAmount.Text);
                int intTDID = getTransDetailID(TransactionID, intAccountID);

                objTD = CreateTransDetailObject(intTDID, TransactionID, intAccountID, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), txtJrAccount.Text);

                objDaTrans = new DaTransaction();
                //TMID = objDaTrans.SaveEditTransactionMaster(objTM, formCon, trans);

                TransactionID = objDaTrans.InsertUpdateTransactionMaster(objTM, formCon, objTD);
                hdnTMID.Value = TransactionID.ToString();

                txtJrAccount.Text = string.Empty;
                txtJrAccountNo.Text = string.Empty;
                txtCrAmount.Text = string.Empty;
                txtDrAmount.Text = string.Empty;
                hdnDrAccNo.Value = "0";
                txtJrAccount.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                formCon.Close();
                grvJrAccount.DataBind();
            }
        }

        protected void btnJrPost_Click(object sender, EventArgs e)
        {
            if (float.Parse(hdnCrTotal.Value) != float.Parse(hdnDrTotal.Value))
            {
                lblJrFailure.Text = "Debit Amt and Credit Amt should be Equal";
                return;
            }

            txtJrVoucherNo.Text = string.Empty;
            txtCrAmount.Text = string.Empty;
            txtDrAmount.Text = string.Empty;
            txtJrAccount.Text = string.Empty;
            txtJrAccountNo.Text = string.Empty;
            txtJrDesc.Text = string.Empty;
            txtJrAppvBy.Text = string.Empty;
            txtJrAppvDate.Text = string.Empty;

            hdnTMID.Value = TransactionID.ToString();
            TransactionID = 0;
            hdnCrAccID.Value = "0";
            hdnCrAccNo.Value = "0";
            hdnDrAccID.Value = "0";
            hdnDrAccNo.Value = "0";

            grvJrAccount.DataBind();

            txtJrDate.Text = DateTime.Today.ToShortDateString();
            txtJrVoucherNo.Text = getVoucherNo(3);

            lblJrFailure.Text = "Posted Successfully";
        }

        protected void btnJrPrint_Click(object sender, EventArgs e)
        {
            int intTMID = hdnTMID.Value != "0" ? int.Parse(hdnTMID.Value) : TransactionID;
            if (string.IsNullOrEmpty(hdnTMID.Value)) return;

            Response.Redirect("~/Pages/ACC/ReportVoucher.aspx?Voucher=3&TMID=" + intTMID);
        }

        #region create objects
        private TransactionMaster CreateTransMasterObject(string TabName)
        {
            TransactionMaster objTM = null;
            try
            {
                objTM = new TransactionMaster();

                if (TabName == "Debit")
                {
                    objTM.TransactionMasterID = TransactionID;
                    objTM.TransactionDate = DateTime.Parse(txtDrDate.Text);
                    objTM.VoucherNo = txtDrVoucherNo.Text.Trim();
                    objTM.VoucherPayee = "";
                    objTM.VoucherType = VoucherType;
                    objTM.TransactionMethodID = Convert.ToInt32(ddlPayMethod.SelectedValue);
                    objTM.MethodRefID = -1;
                    objTM.MethodRefNo = txtDrRefNo.Text.Trim();
                    objTM.TransactionDescription = txtDrDesc.Text;
                    objTM.Module = "Voucher";
                    if (chkDrAppvBy.Checked)
                    {
                        objTM.ApprovedBy = txtDrAppvBy.Text;
                        objTM.ApprovedDate = DateTime.Parse(txtDrAppvDate.Text);
                    }
                    else
                    {
                        objTM.ApprovedBy = string.Empty;
                        objTM.ApprovedDate = new DateTime(1900, 1, 1);
                    }

                    if (objTM.TransactionMasterID <= 0)
                    {
                        objTM.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.CreatedDate = DateTime.Today;
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }
                    else
                    {
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }

                }
                else if (TabName == "Credit")
                {
                    objTM.TransactionMasterID = TransactionID;
                    objTM.TransactionDate = DateTime.Parse(txtCrDate.Text);
                    objTM.VoucherNo = txtCrVoucherNo.Text.Trim();
                    objTM.VoucherPayee = "";
                    objTM.VoucherType = VoucherType;
                    objTM.TransactionMethodID = Convert.ToInt32(ddlCollMethod.SelectedValue);
                    objTM.MethodRefID = -1;
                    objTM.MethodRefNo = txtCrRefNo.Text.Trim();
                    objTM.TransactionDescription = txtCrDesc.Text;
                    objTM.Module = "Voucher";
                    if (chkCrAppvBy.Checked)
                    {
                        objTM.ApprovedBy = txtCrAppvBy.Text;
                        objTM.ApprovedDate = DateTime.Parse(txtCrAppvDate.Text);
                    }
                    else
                    {
                        objTM.ApprovedBy = string.Empty;
                        objTM.ApprovedDate = new DateTime(1900, 1, 1);
                    }

                    if (objTM.TransactionMasterID <= 0)
                    {
                        objTM.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.CreatedDate = DateTime.Today;
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }
                    else
                    {
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }
                }

                else if (TabName == "Journal")
                {
                    objTM.TransactionMasterID = TransactionID;
                    objTM.TransactionDate = DateTime.Parse(txtJrDate.Text);
                    objTM.VoucherNo = txtJrVoucherNo.Text.Trim();
                    objTM.VoucherPayee = "";
                    objTM.VoucherType = VoucherType;
                    objTM.TransactionMethodID = -1;
                    objTM.MethodRefID = -1;
                    objTM.MethodRefNo = string.Empty;
                    objTM.TransactionDescription = txtJrDesc.Text;
                    objTM.Module = "Voucher";
                    if (chkJrAppvBy.Checked)
                    {
                        objTM.ApprovedBy = txtJrAppvBy.Text;
                        objTM.ApprovedDate = DateTime.Parse(txtJrAppvDate.Text);
                    }
                    else
                    {
                        objTM.ApprovedBy = string.Empty;
                        objTM.ApprovedDate = new DateTime(1900, 1, 1);
                    }

                    if (objTM.TransactionMasterID <= 0)
                    {
                        objTM.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.CreatedDate = DateTime.Today;
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }
                    else
                    {
                        objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                        objTM.ModifiedDate = DateTime.Today;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objTM;
        }

        private TransactionDetail CreateTransDetailObject(int TDID, int TMID, int AccountID, double CrAmt, double DrAmt, string cmnt)
        {
            TransactionDetail objTD = null;
            try
            {
                objTD = new TransactionDetail();
                objTD.TransactionDetailID = TDID;
                objTD.TransactionMasterID = TMID;
                objTD.TransactionAccountID = AccountID;
                objTD.CreditAmount = CrAmt;
                objTD.DebitAmount = DrAmt;
                objTD.Comments = cmnt; // string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objTD;
        }

        #endregion

        protected void btnDrDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdnTMID.Value)) return;

            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                objDaTrans = new DaTransaction();
                objDaTrans.DeleteTransaction(int.Parse(hdnTMID.Value), formCon);

                lblDrFailure.Text = "Posted Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        protected void txtSearchDrPayment_TextChanged(object sender, EventArgs e)
        {
            strSearch = txtSearchDrPayment.Text;
        }
    }
}