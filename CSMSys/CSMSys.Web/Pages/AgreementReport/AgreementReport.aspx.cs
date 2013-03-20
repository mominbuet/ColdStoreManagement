using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;

namespace CSMSys.Web.Pages.AgreementReport
{
    public partial class AgreementReport : System.Web.UI.Page
    {
        #region Private Properties
        SqlConnection formCon = null;
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;

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
        
        private int PartyID
        {
            get
            {
                if (ViewState["PartyID"] == null)
                    ViewState["PartyID"] = -1;
                return (int)ViewState["PartyID"];
            }
            set
            {
                ViewState["PartyID"] = value;
            }
        }

        private INVParty _Party;
        private SRVBagLoan _BagLoan;
        private INVBagFair _BagFair;

        ComboData _ComboData = new ComboData();
        String strSearch = string.Empty;
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //UIUtility.BindTypeDDL(ddlType, 0, false);
                UIUtility.BindBagDDL(ddlWeight, 0, false);
                ddlWeight.SelectedIndex = 1;
                //txtRegistrationID.Text = ((new RegistrationManager().GetRegistrationID()) + 1).ToString();
                txtRegistrationID.Text = ((new RegistrationManager().getNextRegistrationID()) ).ToString();

            }
        }

        protected void btnCode_Click(object sender, EventArgs e)
        {
            int partyID = Convert.ToInt32(txtCode.Text);
            try
            {
                
                LoadToAllControlValue(partyID);
            }
            catch (InvalidCastException err)
            {
                throw (err);    // Rethrowing exception e
            }
        }

        #region Methods
        //private bool checkValidity()
        //{
        //    return true;
        //}

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAgreementNo.Text = string.Empty;
            txtPartyType.Text = string.Empty;
            txtCarryingCost.Text = "0";
            txtRegistrationID.Text = ((new RegistrationManager().getNextRegistrationID())).ToString();

            grvStockSerial.DataBind();

            btnSave.Enabled = false;
            txtCarryingCost.Enabled = false;
            txtRemarks.Enabled = false;
        }

        private SRVRegistration FormToObject(int id)
        {
            SRVRegistration tsrv = new SRVRegistration();
            tsrv.SerialID = Convert.ToInt32(txtserid.Text);
            tsrv.PartyID = Convert.ToInt32(txtpartycode.Text);
            //tsrv.BagLoan = float.Parse(txtBagloans.Text);
            tsrv.CarryingLoan = string.IsNullOrEmpty(txtCarryingCost.Text) ? 0 : float.Parse(txtCarryingCost.Text);
            tsrv.Remarks = txtRemarks.Text;
            tsrv.SerialNo = txtAgreementNo.Text;
            tsrv.Requisitioned = "Not Applied";
            tsrv.BagLoan = string.IsNullOrEmpty(txtEmptyBag.Text) ? 0 : Convert.ToInt32(txtEmptyBag.Text);
            tsrv.LoanDisbursed = 0;
            tsrv.Bags = 0;
            tsrv.BagWeight = ddlWeight.SelectedIndex <= 0 ? 85 : Convert.ToInt32(ddlWeight.SelectedItem.Text);
            tsrv.CreatedDate = DateTime.Now;
            tsrv.CreatedBy = WebCommonUtility.GetCSMSysUserKey();



            return tsrv;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                ReportManager rptManager = new ReportManager();
                //_Party = new PartyManager.GetPartyByID(intPartyID);
                _Party = rptManager.GetPartyByID(intPartyID);

                //  txtCode.Text = _Party.PartyID;
                //txtserid.Text=_Party
                txtpartycode.Text = _Party.PartyID.ToString();
                txtCode.Text = _Party.PartyCode;
                txtName.Text = _Party.PartyName;
                //txtFather.Text = _Party.FatherName;
                txtPartyType.Text = _Party.PartyType;
                AccountID = (Int32)_Party.AccountID;
                AccountNo = _Party.AccountNo;

                //txtVillage.Text = _Party.AreaVillageName;
                //txtPO.Text = _Party.AreaPOName;
                //txtCellNo.Text = _Party.Cell;
                //txtDistrict.Text = rptManager.getdist(Convert.ToInt32(_Party.DistrictID)).DistrictName;
                //txtUpazilla.Text = rptManager.getupzilla(Convert.ToInt32(_Party.UpazilaPSID)).UpazilaPSName;


                string partyType = txtPartyType.Text;

                if (partyType.Contains("Company"))
                {
                   // txtBagloans.Enabled = true;
                    txtCarryingCost.Enabled = false;
                }
                else
                {
                    //txtBagloans.Enabled = false;
                    txtCarryingCost.Enabled = true;
                }


                BagLoanManager bagLoanManager = new BagLoanManager();
                string bagLoan = Convert.ToString(bagLoanManager.getBagLoanByID(intPartyID));
                if (bagLoan == "")
                {
                    txtEmptyBag.Text = ("0");
                }
                else
                {
                    int bagRemain = Convert.ToInt32(bagLoan) - Convert.ToInt32(_Party.bagcount);
                    txtEmptyBag.Text = (bagRemain<0)?bagRemain.ToString():bagRemain.ToString();
                }
            }
        }

        public string HighlightText(string InputTxt)
        {
            string SearchStr = txtsearch.Text;
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

        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtpartycode.Text))
            {
                lblFailure.Text = "Please Select a Customer";
                //txtSerial.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCarryingCost.Text))
            {
                if (txtCarryingCost.Enabled == true)
                {
                    lblFailure.Text = "Carrying Loan is Required";
                    txtCarryingCost.Focus();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Customer Code is Required";
                txtCode.Focus();
                return false;
            }

            if (ddlWeight.SelectedIndex == 0)
            {
                lblFailure.Text = "Weight is Required";
                txtCode.Focus();
                return false;
            }
            //if (ddlCustomer.SelectedIndex <= 0)
            //{
            //    lblFailure.Text = "Customer Code is Required";
            //    ddlCustomer.Focus();
            //    return false;
            //}
            return true;
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

                objTM = CreateTransMasterObject("Journal");

                float fltDrAmount = string.IsNullOrEmpty(txtCarryingCost.Text) ? 0 : Convert.ToInt32(txtCarryingCost.Text);
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

                objTM = CreateTransMasterObject("Journal");

                float fltDrAmount = 0;
                float fltCrAmount = string.IsNullOrEmpty(txtCarryingCost.Text) ? 0 : Convert.ToInt32(txtCarryingCost.Text);
                int intTDID = getTransDetailID(TransactionID, 37);

                objTD = CreateTransDetailObject(intTDID, TransactionID, 37, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), txtName.Text);

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
        private TransactionMaster CreateTransMasterObject(string TabName)
        {
            TransactionMaster objTM = null;
            try
            {
                objTM = new TransactionMaster();

                if (TabName == "Debit")
                {
                    //objTM.TransactionMasterID = TransactionID;
                    //objTM.TransactionDate = DateTime.Today;
                    //objTM.VoucherNo = getVoucherNo(1);
                    //objTM.VoucherPayee = "";
                    //objTM.VoucherType = 1;
                    //objTM.TransactionMethodID = -1;
                    //objTM.MethodRefID = -1;
                    //objTM.MethodRefNo = string.Empty;
                    //objTM.TransactionDescription = "Carrying Loan to " + txtCode.Text + " - " + txtName.Text;
                    //objTM.Module = "Voucher";
                    //objTM.ApprovedBy = string.Empty;
                    //objTM.ApprovedDate = new DateTime(1900, 1, 1);

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
                    objTM.TransactionDescription = "Carrying Loan to " + txtCode.Text + " - " + txtName.Text;
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
        #endregion
        
        #region Methods For Save
        private void saveSRVRegistration()
        {
            if (new ReportManager().SaveReport(FormToObject(PartyID)))
            {
                lblFailure.Text = string.Empty;
                grvStockSerial.DataBind();
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgreementReportViewer.aspx?RID=" + txtRegistrationID.Text);

        }

        #region from shad
        #region Methods For Grid
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strID = words[0].Trim().ToString();
                string strCode = words[1].Trim().ToString();

                string serid = words[2].Trim().ToString();
                //int intPartyID = Convert.ToInt32(strID);

                if (text != null)
                {
                    //ddlCustomer.SelectedValue = strID;
                    //hdnPartyID.Value = strID;
                    txtAgreementNo.Text = strID;
                    txtserid.Text = serid;
                    int partyID = Convert.ToInt32(strCode);
                    try
                    {
                        LoadToAllControlValue(partyID);
                        //txtCode.Text = strCode;
                        btnSave.Enabled = true;
                    }
                    catch (InvalidCastException err)
                    {
                        throw (err);    // Rethrowing exception e
                    }
                }
            }
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }

        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.CommandArgument = ((Label)e.Row.FindControl("lblSerialNo")).Text + "@" + ((Label)e.Row.FindControl("lblpid")).Text
                    + "@" + ((Label)e.Row.FindControl(("lblSerialID"))).Text;

            }
        }
        #endregion
        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtsearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtsearch.Text = string.Empty;
            grvStockSerial.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        #endregion

     #endregion


     
      

        protected void btnSave_Click(object sender, EventArgs e)
        {
         
       
            try
            {
                if (!checkValidity()) return;
                saveSRVRegistration();

                if (float.Parse(txtCarryingCost.Text) > 0)
                {
                    SaveLoanCrAccount();
                    SaveLoanDrAccount();
                }

                ClearForm();
                //Response.Redirect("AgreementReportViewer.aspx?RID=" + txtRegistrationID.Text);
            }
            catch (InvalidCastException err)
            {
               // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "checkit('" + txtRegistrationID.Text + "');", true);
                throw (err);    // Rethrowing exception e
            }
                     
        }

    }
}