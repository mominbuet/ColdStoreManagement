using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;

using CSMSys.Lib.AccountingDataAccess;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.DataAccessLayer.Implementations;

namespace CSMSys.Web.Controls.INV
{
    public partial class Party : System.Web.UI.Page
    {
        #region Private Properties
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

        private int ParentID
        {
            get
            {
                if (ViewState["ParentID"] == null)
                    ViewState["ParentID"] = -1;
                return (int)ViewState["ParentID"];
            }
            set
            {
                ViewState["ParentID"] = value;
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

        private INVParty _Party;
        ComboData _ComboData = new ComboData();
        SqlConnection formConn = null;
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;
        #endregion

        protected void ddltype_changed(object sender, EventArgs e)
        {
            //lblseridtobe.Text = "Next party No "+ ddlType.SelectedItem.Text.ToArray()[0]+ new PartyManager().getNextPartyID();
            //txtCode.Text = (""+ddlType.SelectedItem.Text.ToArray()[0] + new PartyManager().getNextPartyID()).ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //UIUtility.FillDropDownList(ddlGender, _ComboData.GenderList(), "Gender");
                //UIUtility.FillDropDownList(ddlReligion, _ComboData.ReligionList(), "Religion");
                UIUtility.FillDropDownList(ddlType, _ComboData.PartyType(), "Type");
                
                UIUtility.BindDistrictDDL(ddlDistrict, 0, false);
                UIUtility.BindUpazillaPSDDL(ddlUpazila, 0, false);

                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    PartyID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    txtParent.Text = "Loans Receivable";
                    ParentID = getAccountID(txtParent.Text.ToString());

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(PartyID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        txtLedger.Text = getAccountNo();

                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewParty.ActiveViewIndex = 0;
            }
        }

        #region Methods for Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
                {
                    if (!checkValidity()) return;
                    updateStock();
                    SaveLedger();
                    SaveParty();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewParty.ActiveViewIndex = 1;
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewParty.ActiveViewIndex = 2;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDistrict.SelectedValue) > 0)
            {
                int districtID = int.Parse(ddlDistrict.SelectedValue);
                UIUtility.BindUpazillaPSDDL(ddlUpazila, districtID, false);
            }
        }
        #endregion

        #region Methods
        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Code is Required";
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblFailure.Text = "Name is Required";
                txtName.Focus();
                return false;
            }
            if (ddlDistrict.SelectedIndex <= 0)
            {
                lblFailure.Text = "District is Required";
                ddlDistrict.Focus();
                return false;
            }
            if (ddlUpazila.SelectedIndex <= 0)
            {
                lblFailure.Text = "Upazila/PS is Required";
                ddlUpazila.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFather.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            txtContactNo.Text = string.Empty;
            //ddlGender.SelectedIndex = 0;
            //ddlReligion.SelectedIndex = 0;
            txtBLoan.Text = "0";

            txtVillage.Text = string.Empty;
            txtPO.Text = string.Empty;
            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            //txtTel.Text = string.Empty;
            txtParent.Text = string.Empty;
            txtLedger.Text = string.Empty;
        }

        private INVParty FormToObject(int id)
        {
            INVParty _temp_Party = new INVParty();
            if(id>0)
                _temp_Party = new PartyManager().GetPartyByID(id);

            _temp_Party.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
            _temp_Party.CreatedDate = System.DateTime.Now;

            _temp_Party.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _temp_Party.ModifiedDate = System.DateTime.Now;

            _temp_Party.PartyID = id;
            _temp_Party.bagcount = getPartyBagCount(id);
            _temp_Party.PartyCode = txtCode.Text;
            _temp_Party.PartyName = txtName.Text;
            _temp_Party.FatherName = txtFather.Text;
            _temp_Party.PartyType = ddlType.SelectedItem.Text.ToString();
            _temp_Party.ContactNo = txtContactNo.Text;
            _temp_Party.Gender = string.Empty; //ddlGender.SelectedValue.ToString();
            _temp_Party.Religion = string.Empty; //ddlReligion.SelectedValue.ToString();
            _temp_Party.BloanPerc = int.Parse(txtBLoan.Text.ToString());

            _temp_Party.AreaVillageName = txtVillage.Text;
            _temp_Party.AreaPOName = txtPO.Text;
            _temp_Party.UpazilaPSID = ddlUpazila.SelectedIndex > 0 ? Convert.ToInt32(ddlUpazila.SelectedValue.ToString()) : 0;
            _temp_Party.DistrictID = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue.ToString()) : 0;
            //_temp_Party.Tel = txtTel.Text;
            //_temp_Party.Cell = txtCell.Text;
            //_temp_Party.Email = txtEmail.Text;
            _temp_Party.AccountID = AccountID;
            _temp_Party.AccountNo = txtLedger.Text;

            _temp_Party.PartyCodeName = txtCode.Text + " - " + txtName.Text;
            _temp_Party.IsActive = true;

            return _temp_Party;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                _Party = new PartyManager().GetPartyByID(intPartyID);

                txtCode.Text = _Party.PartyCode;
                txtName.Text = _Party.PartyName;
                txtFather.Text = _Party.FatherName;
                ddlType.SelectedValue = _Party.PartyType;
                txtContactNo.Text = _Party.ContactNo;
                //ddlGender.SelectedValue = _Party.Gender;
                //ddlReligion.SelectedValue = _Party.Religion;
                txtBLoan.Text = _Party.BloanPerc.ToString();

                txtVillage.Text = _Party.AreaVillageName;
                txtPO.Text = _Party.AreaPOName;
                ddlDistrict.SelectedValue = _Party.DistrictID.ToString();
                ddlUpazila.SelectedValue = _Party.UpazilaPSID.ToString();
                //txtTel.Text = _Party.Tel;
                //txtCell.Text = _Party.Cell;
                //txtEmail.Text = _Party.Email;
                txtLedger.Text = _Party.AccountNo != null ? _Party.AccountNo : getAccountNo();
                AccountID = _Party.AccountID != null ? (Int32)_Party.AccountID : 0;
            }
        }

        private int getPartyBagCount(int PartyID)
        {
            int intBags = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT SUM(Bags) AS BagCount FROM [INVStockSerial] GROUP BY PartyID HAVING [PartyID] = " + PartyID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) intBags = int.Parse(sqlReader["BagCount"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return intBags;
        }

        private int getAccountID(string strTitle)
        {
            int accountID = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccountID] FROM [dbo].[T_Account] WHERE [AccountTitle] = 'Loans Receivable'";
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

        private string getAccountNo()
        {
            formConn = new SqlConnection(connstring);
            formConn.Open();
            string accountNo = "0";

            if (ParentID > 0)
            {
                accountNo = new DaAccount().GenerateAccountNo(formConn, ParentID);
            }

            return accountNo;
        }

        private int getAccountDepth(int accountID)
        {
            int depth = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [AccDepth] FROM [dbo].[T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) depth = int.Parse(sqlReader["AccDepth"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return depth;
        }

        private int getAccountNature(int accountID)
        {
            int nature = 0;

            using (SqlConnection sqlConn = new SqlConnection(connstring))
            {
                sqlConn.Open();
                string _query = "SELECT [Nature] FROM [dbo].[T_Account] WHERE [AccountID] = " + accountID;
                SqlCommand sqlCmd = new SqlCommand(_query, sqlConn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                if (sqlReader.Read())
                {
                    if (sqlReader.HasRows) nature = int.Parse(sqlReader["Nature"].ToString());
                }

                sqlReader.Close();
                sqlConn.Close();
            }
            return nature;
        }

        private Accounts CreateAccountObject(int LdgrID)
        {
            Accounts objAcc = null;
            try
            {
                objAcc = new Accounts();
                objAcc.AccountID = AccountID;
                objAcc.AccountNo = txtLedger.Text;
                objAcc.AccountTitle = txtName.Text.Trim();
                objAcc.AccountOrGroup = "Account";

                objAcc.AccountCreateDate = DateTime.Today;
                objAcc.AccountStatus = "Active";
                objAcc.OpeningBalance = 0;
                objAcc.IsInventoryRelated = 0;

                objAcc.LedgerTypeID = 2;
                objAcc.ParentID = ParentID;
                objAcc.AccountDepth = getAccountDepth(ParentID) + 1;
                objAcc.AccountNature = getAccountNature(ParentID);


                objAcc.LedgerID = LdgrID;
                if (AccountID <= 0)
                {
                    objAcc.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                    objAcc.CreatedDate = DateTime.Today;
                    objAcc.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    objAcc.ModifiedDate = DateTime.Today;
                }
                else
                {
                    objAcc.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    objAcc.ModifiedDate = DateTime.Today;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objAcc;
        }

        private Ledgers CreateLedgerObject()
        {
            Ledgers objLdgr = null;
            try
            {
                objLdgr = new Ledgers();

                objLdgr.LedgerID = 0;
                objLdgr.LedgerName = txtName.Text.Trim();
                objLdgr.LedgerTypeID = 2;
                objLdgr.Address = "Vill: " + txtVillage.Text.Trim() + ", PO: " + txtPO.Text.Trim();
                objLdgr.CountryID = 0;
                objLdgr.CurrencyID = 1;
                objLdgr.ContactPerson = txtName.Text.Trim();
                objLdgr.BankAccountType = "NULL";
                objLdgr.BusinessType = ddlType.SelectedIndex > 0 ? ddlType.SelectedValue.ToString() : " ";
                objLdgr.Phone = txtContactNo.Text.Trim();
                objLdgr.Fax = string.Empty;
                objLdgr.Email = string.Empty;
                objLdgr.TeamMemberID = -1;
                objLdgr.Remarks = string.Empty;
                objLdgr.AccountID = -1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objLdgr;
        }
        #endregion

        #region Methods For Save
        private void updateStock()
        {
            if (PartyID != 0)
            {
                IList<INVStockSerial> invss = new SerialDAOLinq().SearchSerialByParty(PartyID);
                foreach (INVStockSerial invs in invss)
                {
                    INVStockSerial tempinvss = new INVStockSerial();
                    tempinvss.Serial = invs.Serial;
                    tempinvss.SerialDate = invs.SerialDate;
                    tempinvss.SerialID = invs.SerialID;
                    tempinvss.Bags = invs.Bags;
                    tempinvss.SerialNo = invs.SerialNo;
                    tempinvss.PartyID = invs.PartyID;
                    tempinvss.Remarks = invs.Remarks;
                    tempinvss.CreatedBy = invs.CreatedBy;
                    tempinvss.CreatedDate = invs.CreatedDate;

                    tempinvss.PartyCode = txtCode.Text;
                    tempinvss.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                    tempinvss.ModifiedDate = DateTime.Now;
                    new SerialDAOLinq().Edit(tempinvss);
                }
            }
        }
        private void SaveParty()
        {
            INVParty ip = new INVParty();
            ip = FormToObject(PartyID);
            if (new PartyManager().SaveParty(ip))
            {
                ClearForm();
            }
        }

        private void SaveLedger()
        {
            formConn = new SqlConnection(connstring);
            formConn.Open();
            try
            {
                int LdgrID = -1, AccID = -1;

                if (AccountID <= 0)
                {
                    Ledgers objLedger = CreateLedgerObject();
                    LdgrID = new DaLedger().InsertUpdateLedgers(objLedger, formConn);

                    Accounts objAccount = CreateAccountObject(LdgrID);
                    AccountID = new DaAccount().InsertUpdateAccounts(objAccount, formConn);
                }
                else
                {
                    new DaLedger().UpdateAccountID(formConn, LdgrID, AccID);
                    new DaAccount().UpdateLedgerID(formConn, AccID, LdgrID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}