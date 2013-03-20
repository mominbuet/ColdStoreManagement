using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CSMSys.Lib.AccountingDataAccess;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingEntity;
using System.Configuration;
using CSMSys.Lib.Utility;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Pages.ACC
{
    public partial class Ledger : System.Web.UI.Page
    {
        #region Private Properties
        SqlConnection formConn = null;
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

        private Accounts objAccount = null;
        private Ledgers objLedger = null;
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillTree();
                loadParentAccounts();
                loadLedgerTypes();
                loadCountry();
                loadCurrency();
                loadTeam();
            }
        }

        #region Method
        private void FillTree()
        {
            DataSet PrSet = PDataset("select * from T_Account where CompanyID=1 AND AccDepth=0");

            tvLedger.Nodes.Clear();

            foreach (DataRow dr in PrSet.Tables[0].Rows)
            {
                TreeNode tnParent = new TreeNode();

                tnParent.Text = dr["AccountTitle"].ToString();

                tnParent.Value = dr["AccountID"].ToString();

                tnParent.PopulateOnDemand = true;

                tnParent.ToolTip = "Click to get Child";

                tnParent.SelectAction = TreeNodeSelectAction.SelectExpand;

                tnParent.Expand();

                tnParent.Selected = true;

                tvLedger.Nodes.Add(tnParent);

                FillChild(tnParent, tnParent.Value);

            }
        }

        public void FillChild(TreeNode parent, string ParentId)
        {
            DataSet ds = PDataset("select * from T_Account where CompanyID=1 AND ParentID=" + ParentId);

            parent.ChildNodes.Clear();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                TreeNode child = new TreeNode();

                child.Text = dr["AccountTitle"].ToString().Trim();

                child.Value = dr["AccountID"].ToString().Trim();

                if (child.ChildNodes.Count == 0)
                {

                    //child.PopulateOnDemand = true;

                    FillChild(child, child.Value);
                }
                child.ToolTip = "Click to get Child";

                child.SelectAction = TreeNodeSelectAction.SelectExpand;

                child.CollapseAll();

                parent.ChildNodes.Add(child);
            }

        }

        protected DataSet PDataset(string Select_Statement)
        {
            SqlConnection SqlCon = new SqlConnection(connstring);
            SqlDataAdapter da = new SqlDataAdapter(Select_Statement, SqlCon);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        private void loadParentAccounts()
        {
            formConn = new SqlConnection(connstring);
            try
            {
                ddlParentLedger.DataSource = new DaAccount().getAccountHeads(formConn);
                ddlParentLedger.DataTextField = "AccountTitle";
                ddlParentLedger.DataValueField = "AccountID";
                ddlParentLedger.DataBind();
                ddlParentLedger.Items.Insert(0, new ListItem("   <-- Select Parent Account -->", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadLedgerTypes()
        {
            formConn = new SqlConnection(connstring);
            try
            {
                DataTable dt = new DaLedgerType().getLedgerType(formConn);
                ddlLedgerType.DataSource = dt;
                ddlLedgerType.DataValueField = "LedgerTypeID";
                ddlLedgerType.DataTextField = "LedgerType";
                ddlLedgerType.DataBind();
                ddlLedgerType.Items.Insert(0, new ListItem("   <-- Select Ledger Type -->", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadCountry()
        {
            formConn = new SqlConnection(connstring);
            try
            {
                ddlCountry.DataSource = new DaCountry().getCountry(formConn);
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("   <-- Select Country -->", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadCurrency()
        {
            UIUtility.FillDropDownList(ddlCurrency, _ComboData.CurrencyCodeList(), "Currency");

            //formConn = new SqlConnection(connstring);
            //try
            //{
            //    ddlCurrency.DataSource = new DaCurrency().getCurrency(formConn);
            //    ddlCurrency.DataValueField = "CurrencyID";
            //    ddlCurrency.DataTextField = "Name";
            //    ddlCurrency.DataBind();
            //    ddlCurrency.Items.Insert(0, new ListItem("   <-- Select Currency -->", "-1"));
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void loadTeam()
        {
            formConn = new SqlConnection(connstring);
            try
            {
                DataTable dt = new DaTeam().loadMaster(formConn);
                //dt.Rows.Add(-1, "", "Independant");
                ddlTeam.DataSource = dt;
                ddlTeam.DataValueField = "TeamID";
                ddlTeam.DataTextField = "TeamName";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("   <-- Select Team -->", "-1"));
                ddlTeam.Items.Insert(0, new ListItem("Independant", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadTeamMember(int teamID)
        {
            formConn = new SqlConnection(connstring);
            try
            {
                ddlMember.DataSource = new DaTeam().loadDetail(formConn, teamID);
                ddlMember.DataValueField = "MemberID";
                ddlMember.DataTextField = "MemberName";
                ddlTeam.DataBind();
                ddlTeam.Items.Insert(0, new ListItem("   <-- Select Member -->", "-1"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string InputValidation()
        {
            string err = string.Empty;
            try
            {
                if (txtLedgerTitle.Text.Trim() == "")
                    return "Enter a valid Title";
                if (txtLedgerNo.Text.Trim() == "")
                    return "Invalid Ledger No";
                txtOpBal.Text = txtOpBal.Text.Trim();
                if (txtOpBal.Text == "") txtOpBal.Text = "0";
                if (chkInChartOfAcc.Checked == false && chkDetail.Checked == false)
                    return "you must create either an account or ledger";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
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
                //DataRowView drv = (DataRowView)ddlParentLedger.SelectedItem;
                objAcc = new Accounts();
                objAcc.AccountID = AccountID;
                objAcc.AccountNo = txtLedgerNo.Text;
                objAcc.AccountTitle = txtLedgerTitle.Text.Trim();
                objAcc.AccountOrGroup = ddlAccOrGroup.SelectedItem.ToString() == "Account" ? "Account" : "Group";

                if (ddlAccOrGroup.SelectedItem.ToString() == "Account")
                {
                    objAcc.AccountCreateDate = DateTime.Parse(txtCreateDate.Text);
                    objAcc.AccountStatus = ddlAccStatus.SelectedItem.ToString();
                    objAcc.OpeningBalance = double.Parse(txtOpBal.Text.Trim());
                    objAcc.IsInventoryRelated = chkInvRel.Checked ? 1 : 0;
                }
                else
                {
                    objAcc.AccountCreateDate = DateTime.Now.Date;
                    objAcc.AccountStatus = "Active";
                    objAcc.OpeningBalance = 0;
                    objAcc.IsInventoryRelated = 0;
                }
                objAcc.LedgerTypeID = int.Parse(ddlLedgerType.SelectedValue);
                objAcc.ParentID = int.Parse(ddlParentLedger.SelectedValue);
                objAcc.AccountDepth = getAccountDepth(objAcc.ParentID) + 1;
                objAcc.AccountNature = getAccountNature(objAcc.ParentID);


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

                objLdgr.LedgerID = (hdnLedgerID.Text=="")?0:int.Parse(hdnLedgerID.Text);
                objLdgr.LedgerName = txtLedgerTitle.Text.Trim();
                objLdgr.LedgerTypeID = int.Parse(ddlLedgerType.SelectedValue);
                objLdgr.Address = txtAddress.Text.Trim();
                objLdgr.CountryID = int.Parse(ddlCountry.SelectedValue);
                objLdgr.CurrencyID = 1;
                objLdgr.ContactPerson = txtContactPerson.Text.Trim();
                if (ddlAcType.Enabled == false)
                    objLdgr.BankAccountType = "NULL";
                else
                    objLdgr.BankAccountType = ddlAcType.SelectedItem.ToString();
                objLdgr.BusinessType = txtBusinessType.Text.Trim();
                objLdgr.Phone = txtPhone.Text.Trim();
                objLdgr.Fax = txtFax.Text.Trim();
                objLdgr.Email = txtEmail.Text.Trim();
                if (ddlTeam.Enabled == false)
                    objLdgr.TeamMemberID = -1;
                else if (int.Parse(ddlTeam.SelectedValue) == -1)
                    objLdgr.TeamMemberID = -1;
                else
                    objLdgr.TeamMemberID = int.Parse(ddlMember.SelectedValue);
                objLdgr.Remarks = txtRemarks.Text.Trim();
                objLdgr.AccountID = -1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objLdgr;
        }
        #endregion

        #region Method for Button
        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                hdnAccountID.Value = "0";
                hdnLedgerID.Text = "0";
                txtLedgerTitle.Text = string.Empty;
                txtOpBal.Text = string.Empty;
                hdnCurBal.Value = "0";
                chkInvRel.Checked = false;
                ddlAccStatus.SelectedItem.Text = "Active";

                txtContactPerson.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtBusinessType.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtFax.Text = string.Empty;
                txtLedgerNo.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtRemarks.Text = string.Empty;

                AccountID = 0;

                loadParentAccounts();
                FillTree();
                txtLedgerTitle.Focus();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        protected void tvLedger_SelectedNodeChanged(object sender, EventArgs e)
        {
            formConn = new SqlConnection(connstring);
            try
            {
                if (tvLedger.SelectedNode.Value != null)
                {
                    AccountID = int.Parse(tvLedger.SelectedNode.Value);
                    objAccount = new DaAccount().GetAccount(formConn, AccountID);

                    txtLedgerTitle.Text = objAccount.AccountTitle;
                    ddlLedgerType.SelectedValue = objAccount.LedgerTypeID == 0 ? "-1" : objAccount.LedgerTypeID.ToString();
                    chkInChartOfAcc.Checked = true;
                    ddlParentLedger.SelectedValue = objAccount.ParentID == 0 ? "-1" : objAccount.ParentID.ToString();

                    if (objAccount.AccountOrGroup == "Account")
                        ddlAccOrGroup.SelectedItem.Text = "Account";
                    else
                        ddlAccOrGroup.SelectedItem.Text = "Sub Ledger";
                    txtOpBal.Text = objAccount.OpeningBalance.ToString("0.00");
                    hdnCurBal.Value = objAccount.CurrentBalance.ToString("0.00");
                    txtLedgerNo.Text = objAccount.AccountNo;
                    chkInvRel.Checked = objAccount.IsInventoryRelated == 1 ? true : false;
                    txtCreateDate.Text = objAccount.AccountCreateDate == null ? DateTime.Today.ToShortDateString() : objAccount.AccountCreateDate.ToShortDateString();

                    if (objAccount.LedgerID == -1)
                    {
                        chkDetail.Checked = false;
                        hdnLedgerID.Text = "0";
                    }
                    else
                    {
                        chkDetail.Checked = true;
                        objLedger = new DaLedger().GetLedger(formConn, objAccount.LedgerID);
                        if (objLedger == null) return;
                        hdnLedgerID.Text = objLedger.LedgerID.ToString();
                        txtContactPerson.Text = objLedger.ContactPerson;
                        ddlAcType.SelectedItem.Text = objLedger.BankAccountType;
                        txtAddress.Text = objLedger.Address;
                        txtBusinessType.Text = objLedger.BusinessType;
                        txtPhone.Text = objLedger.Phone;
                        txtFax.Text = objLedger.Fax;
                        txtEmail.Text = objLedger.Email;
                        ddlCountry.SelectedValue = objLedger.CountryID.ToString();
                        ddlCurrency.SelectedValue = objLedger.CurrencyID.ToString();
                        if (objLedger.TeamMemberID == -1) ddlTeam.SelectedValue = "-1";
                        else
                        {
                            TeamDetail objSalesTeam = new DaTeam().getTeamMember(formConn, objLedger.TeamMemberID);
                            if (objSalesTeam != null)
                            {
                                ddlTeam.SelectedValue = objSalesTeam.intTeamID.ToString();
                                ddlMember.SelectedValue = objLedger.TeamMemberID.ToString();
                            }
                        }

                    }
                }
                //else
                //{
                //    LdgrID = (int)ctlDgvAccountLedger.Rows[rowID].Cells["LedgerID"].Value;
                //    chkDetail.Checked = true;
                //    objLedger = new DaLedger().GetLedger(formConnection, LdgrID);
                //    if (objLedger == null) return;
                //    lblLedgerID.Text = objLedger.LedgerID.ToString();
                //    txtContactPerson.Text = objLedger.ContactPerson;
                //    cboAcType.SelectedItem = objLedger.BankAccountType;
                //    txtAddress.Text = objLedger.Address;
                //    txtPhone.Text = objLedger.Phone;
                //    txtFax.Text = objLedger.Fax;
                //    txtEmail.Text = objLedger.Email;
                //    txtBusinessType.Text = objLedger.BusinessType;
                //    cboCountry.SelectedValue = objLedger.CountryID;
                //    cboCurrency.SelectedValue = objLedger.CurrencyID;
                //    if (objLedger.TeamMemberID == -1) cboTeam.SelectedValue = -1;
                //    else
                //    {
                //        TeamDetail objSalesTeam = new DaTeam().getTeamMember(formConnection, objLedger.TeamMemberID);
                //        if (objSalesTeam != null)
                //        {
                //            cboTeam.SelectedValue = objSalesTeam.intTeamID;
                //            cboMember.SelectedValue = objLedger.TeamMemberID;
                //        }
                //    }
                //    if (objLedger.AccountID == -1)
                //    {
                //        chkInChartOfAcc.Checked = false;
                //        txtLedgerTitle.Text = objLedger.LedgerName;
                //        cboLedgerType.SelectedValue = objLedger.LedgerTypeID;
                //        lblAccountID.Text = "0";
                //    }

                //    else
                //    {
                //        chkInChartOfAcc.Checked = true;


                //        objAccount = new DaAccount().GetAccount(formConnection, objLedger.AccountID);

                //        lblAccountID.Text = objAccount.AccountID.ToString();
                //        txtLedgerTitle.Text = objAccount.AccountTitle;
                //        cboLedgerType.SelectedValue = objAccount.LedgerTypeID;

                //        cboParentLedger.SelectedValue = objAccount.ParentID;

                //        if (objAccount.AccountOrGroup == "Account")
                //            cboAccOrGroup.SelectedItem = "Account";
                //        else
                //            cboAccOrGroup.SelectedItem = "Sub Ledger";
                //        txtOpBal.Text = objAccount.OpeningBalance.ToString("0.00");
                //        txtCurBal.Text = objAccount.CurrentBalance.ToString("0.00");
                //        txtLedgerNo.Text = objAccount.AccountNo;
                //        chkInvRel.Checked = objAccount.IsInventoryRelated == 1 ? true : false;
                //        dtpCreateDate.Value = objAccount.AccountCreateDate.Date;

                //    }

                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            formConn = new SqlConnection(connstring);
            formConn.Open();
            //SqlTransaction trans = null;
            try
            {
                string err = InputValidation();

                if (err != string.Empty)
                {
                    lblFailure.Text = err;
                    return;
                }

                bool msgFlag = false;
                int LdgrID=-1,AccID=-1;
                //trans = formConn.BeginTransaction();
                if (chkDetail.Checked)
                {
                    Ledgers objLedger = CreateLedgerObject();
                    LdgrID = new DaLedger().InsertUpdateLedgers(objLedger, formConn);
                    msgFlag = true;
                }
                else
                {
                    int lID = int.Parse(string.IsNullOrEmpty(hdnLedgerID.Text) ? "0" : hdnLedgerID.Text);
                    if (lID > 0)
                        new DaLedger().DeleteLedger(lID, formConn);
                }

                if (chkInChartOfAcc.Checked)
                {
                    Accounts objAccount = CreateAccountObject(LdgrID);
                    AccID = new DaAccount().InsertUpdateAccounts(objAccount, formConn);
                    msgFlag = true;
                }
                else
                {
                    int aID = AccountID;
                    if (aID > 0)
                        new DaAccount().DeleteAccount(aID, formConn);
                }
                new DaLedger().UpdateAccountID(formConn, LdgrID, AccID);
                new DaAccount().UpdateLedgerID(formConn, AccID, LdgrID);

                //trans.Commit();
                if (msgFlag == true)
                {
                    btnNew_Click(null, null);
                    //btnSearch_Click(null, null);
                    //MessageBox.Show("Saved successfully");
                }
            }
            catch (Exception ex)
            {
                //if (trans != null) trans.Rollback();
                //if (ex.Message.Contains("duplicate"))
                //    MessageBox.Show("This same type ledger or account already exists" + Environment.NewLine + "Please choose a different one.", "Chart Of Account");
                //else
                //    MessageBox.Show(ex.Message);
            }
        }
        #endregion

        protected void ddlLedgerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(ddlLedgerType.SelectedValue) == 1)  //General 
            {
                txtContactPerson.Enabled = true;
                ddlAcType.Enabled = false;
                txtAddress.Enabled = true;
                txtPhone.Enabled = true;
                txtFax.Enabled = true;
                txtEmail.Enabled = true;
                ddlCountry.Enabled = true;
                ddlCurrency.Enabled = true;
                txtBusinessType.Enabled = true;
                txtRemarks.Enabled = true;
                ddlTeam.Enabled = false;
                lblContact.Text = "Contact Person : ";
                lblBusiness.Text = "Business Type : ";
            }
            else if (int.Parse(ddlLedgerType.SelectedValue) == 2)  //customer
            {
                txtContactPerson.Enabled = true;
                ddlAcType.Enabled = false;
                txtAddress.Enabled = true;
                txtPhone.Enabled = true;
                txtFax.Enabled = true;
                txtEmail.Enabled = true;
                ddlCountry.Enabled = true;
                ddlCurrency.Enabled = true;
                txtBusinessType.Enabled = true;
                txtRemarks.Enabled = true;
                ddlTeam.Enabled = true;
                lblContact.Text = "Contact Person : ";
                lblBusiness.Text = "Business Type : ";

            }
            else if (int.Parse(ddlLedgerType.SelectedValue) == 3)  // Supplier
            {
                txtContactPerson.Enabled = true;
                ddlAcType.Enabled = false;
                txtAddress.Enabled = true;
                txtPhone.Enabled = true;
                txtFax.Enabled = true;
                txtEmail.Enabled = true;
                ddlCountry.Enabled = true;
                ddlCurrency.Enabled = true;
                txtBusinessType.Enabled = true;
                txtRemarks.Enabled = true;
                ddlTeam.Enabled = false;
                lblContact.Text = "Contact Person : ";
                lblBusiness.Text = "Business Type : ";

            }
            else if (int.Parse(ddlLedgerType.SelectedValue) == 4)  // Bank
            {
                txtContactPerson.Enabled = true;
                ddlAcType.Enabled = true;
                txtAddress.Enabled = true;
                txtPhone.Enabled = true;
                txtFax.Enabled = true;
                txtEmail.Enabled = true;
                ddlCountry.Enabled = true;
                ddlCurrency.Enabled = true;
                txtBusinessType.Enabled = true;
                txtRemarks.Enabled = true;
                ddlTeam.Enabled = false;
                lblContact.Text = "A/C Holder : ";
                lblBusiness.Text = "Branch : ";

            }
        }

        protected void ddlParentLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            formConn = new SqlConnection(connstring);
            formConn.Open();

            if (ddlParentLedger.SelectedValue == null) return;
            txtLedgerNo.Text = new DaAccount().GenerateAccountNo(formConn, int.Parse(ddlParentLedger.SelectedValue));
        }

        protected void ddlTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTeam.SelectedValue == null) return;
            if (int.Parse(ddlTeam.SelectedValue) == -1) ddlMember.Enabled = false; else ddlMember.Enabled = true;
            loadTeamMember(int.Parse(ddlTeam.SelectedValue));
        }
    }
}