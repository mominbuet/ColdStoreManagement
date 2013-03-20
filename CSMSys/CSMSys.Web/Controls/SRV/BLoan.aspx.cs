using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;
using System.Globalization;

namespace CSMSys.Web.Controls.SRV
{
    public partial class BLoan : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private SRVBagLoan _bagLoan;
        private INVParty _Party;

        SqlConnection formCon = null;
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;

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

        private int LoanID
        {
            get
            {
                if (ViewState["LoanID"] == null)
                    ViewState["LoanID"] = -1;
                return (int)ViewState["LoanID"];
            }
            set
            {
                ViewState["LoanID"] = value;
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

        private string AccountNo
        {
            get
            {
                if (ViewState["AccountNo"] == null)
                    ViewState["AccountNo"] = -1;
                return (string)ViewState["AccountNo"];
            }
            set
            {
                ViewState["AccountNo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtAmtPerBag.Text = "80";
                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                LoanID = string.IsNullOrEmpty(Request.QueryString["LID"]) ? 0 : int.Parse(Request.QueryString["LID"]);
                TransactionID = string.IsNullOrEmpty(Request.QueryString["TMID"]) ? 0 : int.Parse(Request.QueryString["TMID"]);
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllLoanlValue(LoanID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewSerial.ActiveViewIndex = 0;

                ////txtRegistrationID.Text = ((new RegistrationManager().GetRegistrationID()) + 1).ToString();
                //txtBagLoanID.Text = ((new BagLoanManager().getNextRegistrationID()) + 1).ToString();

            }
        }

        private void LoadToAllLoanlValue(int intLoanID)
        {
            if (intLoanID > 0)
            {
                BagLoanManager _Manager = new BagLoanManager();
                _bagLoan = _Manager.GetBagLoanByID(intLoanID);

                int intPartyID = (Int32)_bagLoan.PartyID;
                LoadToAllControlValue(intPartyID);

                txtBag.Text = _bagLoan.BagNumber.ToString();
                txtAmtPerBag.Text = _bagLoan.AmountPerBag.ToString();
                //txtAmnt.Text = _bagLoan.Amount.ToString();
            }
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                ReportManager rptManager = new ReportManager();
                _Party = rptManager.GetPartyByID(intPartyID);

                txtCode.Text = _Party.PartyCode;
                txtpartycode.Text = _Party.PartyID.ToString();
                txtName.Text = _Party.PartyName;
                txtperc.Text = _Party.BloanPerc.ToString();
                txtbstock.Text = _Party.bagcount.ToString();
                txtbloaned.Text = new BagLoanManager().GetAllBagLoansByparty(_Party.PartyID).ToString();
                AccountID = (Int32)_Party.AccountID;
                AccountNo = _Party.AccountNo;
                txtAmtPerBag.Text = "80";
            }
        }

        #region Methods
        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            //txtFather.Text = string.Empty;
            //txtPartyType.Text = string.Empty;
            //txtCellNo.Text = string.Empty;
            //txtVillage.Text = string.Empty;
            //txtPO.Text = string.Empty;
            //txtDistrict.Text = string.Empty;
            //txtUpazilla.Text = string.Empty;
            txtBag.Text = string.Empty;
            txtAmtPerBag.Text = string.Empty;
            //txtBank.Text = string.Empty;
            //txtBranch.Text = string.Empty;
            txtCode.Text = string.Empty;
            //txtChq.Text = string.Empty;
            //txtCDate.Text = string.Empty;
            lblFailure.Text = string.Empty;
            //txtBagloans.Text = string.Empty;
            //txtCarryingCost.Text = string.Empty;
            LoanID = 0;
            TransactionID = 0;
            AccountID = 0;
            AccountNo = string.Empty;
        }

        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtSearch.Text;
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
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvParty.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvParty.DataBind();
        }
        #endregion

        #region methods for save
        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtpartycode.Text))
            {
                lblFailure.Text = "Please Select a Customer";
                return false;
            }
            if (!string.IsNullOrEmpty(txtBag.Text))
            {
                if (int.Parse(txtBag.Text) <= 0)
                {
                    lblFailure.Text = "No of Bags is Required";
                    txtBag.Focus();
                    return false;
                }
            }
            else
            {
                lblFailure.Text = "No of Bags is Required";
                txtBag.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAmtPerBag.Text))
            {
                lblFailure.Text = "Amount/Bag is Required";
                txtAmtPerBag.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Customer Code is Required";
                txtCode.Focus();
                return false;
            }
            int bagloaned = new BagLoanManager().GetAllBagLoansByparty(int.Parse(txtpartycode.Text));
            if (bagloaned != 0)
            {
                double bags = (int.Parse(txtperc.Text)) * bagloaned / 100;
                if ((int.Parse(txtbstock.Text)) < bags)
                {
                    lblFailure.Text = "Not enough Bags on Store.";
                    //txtCode.Focus();
                    btnSave.Visible = false;
                    return false;
                }
            }
            //if (getAccountBalance(36) <= 0)
            //{
            //    lblFailure.Text = "Insufficient Balance";
            //    txtBag.Focus();
            //    return false;
            //}
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
                {
                    if (!checkValidity()) return;
                    //SaveLoanCrAccount();
                    //SaveLoanDrAccount();

                    SaveSRVBagLoan();
                    lblFailure.Text = "Bag Loan Entry Saved.";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewSerial.ActiveViewIndex = 1;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewSerial.ActiveViewIndex = 2;
            }
        }

        private void SaveSRVBagLoan()
        {
            if (new BagLoanManager().SaveBagLoanDetails(FormToObject(LoanID)))
            {
                ClearForm();
            }
        }

        private void SaveLoanCrAccount()
        {
            if (AccountID <= 0) return;

            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                TransactionMaster objTM = new TransactionMaster();
                TransactionDetail objTD = new TransactionDetail();

                objTM = CreateTransMasterObject("Journal", "Cr");

                float fltDrAmount = string.IsNullOrEmpty(txtBag.Text) ? 0 : Convert.ToInt32(int.Parse(txtBag.Text) * int.Parse(txtAmtPerBag.Text));
                float fltCrAmount = 0;
                int intTDID = getTransDetailID(TransactionID, AccountID);

                objTD = CreateTransDetailObject(intTDID, TransactionID, AccountID, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), txtName.Text);

                DaTransaction objDaTrans = new DaTransaction();
                //TMID = objDaTrans.SaveEditTransactionMaster(objTM, formCon, trans);

                TransactionID = objDaTrans.InsertUpdateTransactionMaster(objTM, formCon, objTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                formCon.Close();
            }
        }

        private void SaveLoanDrAccount()
        {
            formCon = new SqlConnection(connstring);
            formCon.Open();

            try
            {
                TransactionMaster objTM = new TransactionMaster();
                TransactionDetail objTD = new TransactionDetail();

                objTM = CreateTransMasterObject("Journal", "Dr");

                float fltDrAmount = 0;
                float fltCrAmount = string.IsNullOrEmpty(txtBag.Text) ? 0 : Convert.ToInt32(int.Parse(txtBag.Text) * int.Parse(txtAmtPerBag.Text));
                int intTDID = getTransDetailID(TransactionID, 36);

                objTD = CreateTransDetailObject(intTDID, TransactionID, 36, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), txtName.Text);

                DaTransaction objDaTrans = new DaTransaction();
                //TMID = objDaTrans.SaveEditTransactionMaster(objTM, formCon, trans);

                TransactionID = objDaTrans.InsertUpdateTransactionMaster(objTM, formCon, objTD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                formCon.Close();
            }
        }

        private float getAccountBalance(int accountID)
        {
            float balance = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [CurrentBalance] FROM [T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) balance = float.Parse(sqlReader["CurrentBalance"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return balance;
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

        #region create objects
        private TransactionMaster CreateTransMasterObject(string TabName, string accType)
        {
            TransactionMaster objTM = null;
            try
            {
                objTM = new TransactionMaster();

                if (TabName == "Debit")
                {
                    //objTM.TransactionMasterID = TransactionID;
                    //objTM.TransactionDate = DateTime.Parse(txtDrDate.Text);
                    //objTM.VoucherNo = txtDrVoucherNo.Text.Trim();
                    //objTM.VoucherPayee = "";
                    //objTM.VoucherType = VoucherType;
                    //objTM.TransactionMethodID = Convert.ToInt32(ddlPayMethod.SelectedValue);
                    //objTM.MethodRefID = -1;
                    //objTM.MethodRefNo = txtDrRefNo.Text.Trim();
                    //objTM.TransactionDescription = txtDrDesc.Text;
                    //objTM.Module = "Voucher";
                    //if (chkDrAppvBy.Checked)
                    //{
                    //    objTM.ApprovedBy = txtDrAppvBy.Text;
                    //    objTM.ApprovedDate = DateTime.Parse(txtDrAppvDate.Text);
                    //}
                    //else
                    //{
                    //    objTM.ApprovedBy = string.Empty;
                    //    objTM.ApprovedDate = new DateTime(1900, 1, 1);
                    //}

                    //if (objTM.TransactionMasterID <= 0)
                    //{
                    //    objTM.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.CreatedDate = DateTime.Today;
                    //    objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.ModifiedDate = DateTime.Today;
                    //}
                    //else
                    //{
                    //    objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.ModifiedDate = DateTime.Today;
                    //}

                }
                else if (TabName == "Credit")
                {
                    //objTM.TransactionMasterID = TransactionID;
                    //objTM.TransactionDate = DateTime.Parse(txtCrDate.Text);
                    //objTM.VoucherNo = txtCrVoucherNo.Text.Trim();
                    //objTM.VoucherPayee = "";
                    //objTM.VoucherType = VoucherType;
                    //objTM.TransactionMethodID = Convert.ToInt32(ddlCollMethod.SelectedValue);
                    //objTM.MethodRefID = -1;
                    //objTM.MethodRefNo = txtCrRefNo.Text.Trim();
                    //objTM.TransactionDescription = txtCrDesc.Text;
                    //objTM.Module = "Voucher";
                    //if (chkCrAppvBy.Checked)
                    //{
                    //    objTM.ApprovedBy = txtCrAppvBy.Text;
                    //    objTM.ApprovedDate = DateTime.Parse(txtCrAppvDate.Text);
                    //}
                    //else
                    //{
                    //    objTM.ApprovedBy = string.Empty;
                    //    objTM.ApprovedDate = new DateTime(1900, 1, 1);
                    //}

                    //if (objTM.TransactionMasterID <= 0)
                    //{
                    //    objTM.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.CreatedDate = DateTime.Today;
                    //    objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.ModifiedDate = DateTime.Today;
                    //}
                    //else
                    //{
                    //    objTM.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    //    objTM.ModifiedDate = DateTime.Today;
                    //}
                }
                else if (TabName == "Journal")
                {
                    objTM.TransactionMasterID = TransactionID;
                    objTM.TransactionDate = DateTime.Today;
                    objTM.VoucherNo = getVoucherNo(3);
                    objTM.VoucherPayee = "";
                    objTM.VoucherType = 3;
                    objTM.TransactionMethodID = -1;
                    objTM.MethodRefID = -1;
                    objTM.MethodRefNo = string.Empty;
                    if (accType == "Cr")
                    {
                        objTM.TransactionDescription = "Bags Amount Loan to " + txtCode.Text + " - " + txtName.Text;
                    }
                    else if (accType == "Dr") { objTM.TransactionDescription = "Bags Amount from Gunny Bags"; }
                    objTM.Module = "Voucher";
                    objTM.ApprovedBy = string.Empty;
                    objTM.ApprovedDate = new DateTime(1900, 1, 1);

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

        private SRVBagLoan FormToObject(int loanID)
        {
            SRVBagLoan srvBagLoan = new SRVBagLoan();

            srvBagLoan.BagLoanID = loanID;
            srvBagLoan.PartyID = Convert.ToInt32(txtpartycode.Text);
            srvBagLoan.TMID = TransactionID;
            srvBagLoan.BagNumber = Convert.ToInt32(txtBag.Text);
            srvBagLoan.ChequeNo = string.Empty;
            srvBagLoan.ChequeDate = DateTime.Now;
            srvBagLoan.Bank = string.Empty;
            srvBagLoan.Branch = string.Empty;
            srvBagLoan.Amount = (txtBag.Text != "") ? Convert.ToInt32(int.Parse(txtBag.Text) * int.Parse(txtAmtPerBag.Text)) : 0;
            srvBagLoan.AmountPerBag = (txtAmtPerBag.Text != "") ? float.Parse(txtAmtPerBag.Text) : 0;
            srvBagLoan.LoanAmount = (txtBag.Text != "") ? Convert.ToInt32(int.Parse(txtBag.Text) * int.Parse(txtAmtPerBag.Text)) : 0;
            srvBagLoan.CreatedDate = DateTime.ParseExact(txtdatedisbursed.Text, "yyyy/MM/dd", new CultureInfo("en-US"));
            srvBagLoan.CreatedBy = Convert.ToString(WebCommonUtility.GetCSMSysUserKey());
            return srvBagLoan;
        }
        #endregion

        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("Select"))
            {
                //ClearForm();
                btnSave.Visible = true;
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                //string strID = words[0].Trim().ToString();
                string strCode = words[1].Trim().ToString();

                //int intPartyID = Convert.ToInt32(strID);

                if (text != null)
                {
                    //ddlCustomer.SelectedValue = strID;
                    //hdnPartyID.Value = strID;
                    //txtAgreementNo.Text = strID;

                    int partyID = Convert.ToInt32(strCode);
                    try
                    {
                        //ClearForm();
                        LoadToAllControlValue(partyID);
                        //txtCode.Text = strCode;
                    }
                    catch (InvalidCastException err)
                    {
                        throw (err);    // Rethrowing exception e
                    }
                }
            }
        }

        protected void grvParty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvParty.PageIndex = e.NewPageIndex;
            grvParty.DataBind();
        }

        protected void grvParty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = " " + "@" +
                    (DataBinder.Eval(e.Row.DataItem, "PartyID")).ToString();

            }
        }
        #endregion
    }
}