using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Manager.SRV;

using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;

namespace CSMSys.Web.Controls.SRV
{
    public partial class DisburseUser : System.Web.UI.Page
    {     
        
        #region Private Properties
        private SRVLoanDisburse _disburse;
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
        
        private int LocationID
        {
            get
            {
                if (ViewState["LocationID"] == null)
                    ViewState["LocationID"] = -1;
                return (int)ViewState["LocationID"];
            }
            set
            {
                ViewState["LocationID"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiViewParty.ActiveViewIndex = 0;
                LocationID = Convert.ToInt32(Request.QueryString["LID"]);
                hdnWindowUIMODE.Value = "EDIT";

                if (LocationID > 0 )
                {
                    LoadToAllControlValue(LocationID);
                    //Response.Write(LocationID);
                    btnSave.Text = "Update";
                }
            }
        }
        private void LoadToAllControlValue(int lid)
        {
            if (lid > 0)
            {
                _disburse = new LoanDAOLinq().PickByID(lid);
                string[] serids=_disburse.serialIDs.Split(',');
                foreach(string id in serids)
                {
                    if(id!="")
                        lstserialids.Items.Add(id);

                }
                INVParty ip = new PartyDAOLinq().PickByID(Convert.ToInt32( _disburse.PartyID));
                lblPartyName.Text = ip.PartyName;
                lblpartyCode.Text = ip.PartyCode;
                lblloanid.Text = _disburse.LoanID.ToString();
                lblbags.Text = _disburse.Bags.ToString();
                lblAmount.Text = _disburse.LoanAmount.ToString();
                lblTotal.Text = (float.Parse(lblbags.Text) * float.Parse(lblAmount.Text)).ToString();
                TransactionID = (Int32)_disburse.TMID;
                AccountID = (Int32)ip.AccountID;
                AccountNo = ip.AccountNo;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int loanid = int.Parse(lblloanid.Text);

                if (loanid > 0)
                {
                    if (float.Parse(lblTotal.Text) > 0)
                    {
                        SaveLoanCrAccount();
                        SaveLoanDrAccount();
                    }

                    DisburseLoan(loanid);

                    MultiViewParty.ActiveViewIndex = 1;
                    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                }
            }
            catch (Exception ex)
            {
                MultiViewParty.ActiveViewIndex = 2;
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
            }
        }

        private void DisburseLoan(int id)
        {
            SRVLoanDisburse srv = new LoanDAOLinq().PickByID(id);
            srv.TMID = TransactionID;
            new LoanDAOLinq().Edit(srv);
            string[] serialids = srv.serialIDs.Split(',');
            foreach (string serialid in serialids)
            {
                if (serialid != "")
                {
                    //SRVLoanDisburse srvld = new LoanDAOLinq().PickByID(
                    INVStockSerial invStockSerial = new SerialManager().GetSerialByID(int.Parse(serialid));
                    new LoanManager().updateSRVRegistration(invStockSerial.SerialNo, int.Parse(srv.PartyID.ToString()),
                        int.Parse(srv.caseID.ToString()));
                }

            }
            //print that report here//
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

                float fltDrAmount = string.IsNullOrEmpty(lblTotal.Text) ? 0 : Convert.ToInt32(lblTotal.Text);
                float fltCrAmount = 0;
                int intTDID = getTransDetailID(TransactionID, AccountID);

                objTD = CreateTransDetailObject(intTDID, TransactionID, AccountID, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), lblPartyName.Text);

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
                float fltCrAmount = string.IsNullOrEmpty(lblTotal.Text) ? 0 : Convert.ToInt32(lblTotal.Text);
                int intTDID = getTransDetailID(TransactionID, 35);

                objTD = CreateTransDetailObject(intTDID, TransactionID, 35, Convert.ToDouble(fltCrAmount), Convert.ToDouble(fltDrAmount), lblPartyName.Text);

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
                    //objTM.TransactionDescription = "Carrying Loan to " + lblpartyCode.Text + " - " + lblPartyName.Text;
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
                    objTM.TransactionDescription = "Potato Loan to " + lblpartyCode.Text + " - " + lblPartyName.Text;
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

    }
}