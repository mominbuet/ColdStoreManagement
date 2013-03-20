using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;

namespace CSMSys.Web.Controls.Item
{
    public partial class ItemDetails : System.Web.UI.Page
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

        private INVParty _Party;
        private INVBagFair _BagFair;

        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            txtRegistrationID.Text = Request.QueryString["RID"];
            txtCode.Text = Request.QueryString["CID"];
            if (!IsPostBack)
            {
                UIUtility.BindTypeDDL(ddlType, 0, false);
                UIUtility.BindBagDDL(ddlWeight, 0, false);
               
                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    PartyID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(PartyID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
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
            //try
            //{
            //    if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
            //    {
            //        if (!checkValidity()) return;

            //        SaveParty();
            //    }
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
            //    MultiViewParty.ActiveViewIndex = 1;
            //}
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
            //    MultiViewParty.ActiveViewIndex = 2;
            //}
        }
        #endregion

        #region Methods
        
        private void ClearForm()
        {
            //txtCode.Text = string.Empty;
            //txtName.Text = string.Empty;
            //txtFather.Text = string.Empty;
            //ddlType.SelectedIndex = 0;
            //txtContactNo.Text = string.Empty;
            //ddlGender.SelectedIndex = 0;
            //ddlReligion.SelectedIndex = 0;

        }

        private INVParty FormToObject(int id)
        {
            if (id <= 0)
            {
                _Party = new INVParty();

                _Party.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                _Party.CreatedDate = System.DateTime.Now;
            }

            _Party.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _Party.ModifiedDate = System.DateTime.Now;

            _Party.PartyID = id;

            //_Party.PartyCode = txtCode.Text;
            //_Party.PartyName = txtName.Text;
            //_Party.FatherName = txtFather.Text;
            //_Party.PartyType = ddlType.SelectedValue.ToString();
            //_Party.ContactNo = txtContactNo.Text;
            //_Party.Gender = ddlGender.SelectedValue.ToString();
            //_Party.Religion = ddlReligion.SelectedValue.ToString();
         //   _Party.PartyCodeName = txtCode.Text + " - " + txtName.Text;
            _Party.IsActive = true;

            return _Party;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                _Party = new PartyManager().GetPartyByID(intPartyID);

                txtCode.Text = _Party.PartyCode;
                //txtName.Text = _Party.PartyName;
                //txtFather.Text = _Party.FatherName;
                //ddlType.SelectedValue = _Party.PartyType;
                //txtContactNo.Text = _Party.ContactNo;
                //ddlGender.SelectedValue = _Party.Gender;
                //ddlReligion.SelectedValue = _Party.Religion;

            }
        }
        #endregion
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdvance.Enabled = true;
            txtBagNo.Enabled = true;
            ddlWeight.Enabled = true;
        }

        protected void ddlWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlWeight.SelectedIndex) > 0)
            {
                int fairID = int.Parse(ddlWeight.SelectedValue);
                BagManager bagManager = new BagManager(true);
                _BagFair = bagManager.GetFairByID(fairID);
                txtBagFair.Text = _BagFair.BagFair.ToString();
                int totalFair = Convert.ToInt32(txtBagNo.Text) * Convert.ToInt32(txtBagFair.Text);
                txtTotalFair.Text = totalFair.ToString();
            }
            txtAdvance.Enabled = true;
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if (new ItemDetailsManager(true).SaveItemDetails(ItemToObject(PartyID)))
            {
                dsItemDetails.SelectCommand = @"SELECT     itd.RegistrationID,itd.ItemDetailID, it.TypeName, itd.BagNo, itd.BagWeight, itd.BagFair, itd.TotalFair, itd.Advance
                                                                            FROM          dbo.INVItemDetail as itd INNER JOIN dbo.INVItemType AS it ON itd.ItemTypeID = it.TypeID
                                                                            WHERE itd.RegistrationID = " + txtRegistrationID.Text + " ORDER BY itd.ItemDetailID DESC";
                grvItemDetails.DataBind();
            }
        }

        private INVItemDetail ItemToObject(int id)
        {
            INVItemDetail item = new INVItemDetail();
            item.RegistrationID = Convert.ToInt32(txtRegistrationID.Text);
            item.BagNo = Convert.ToInt32(txtBagNo.Text);
            item.BagWeight = Convert.ToInt32(ddlWeight.SelectedItem.Text);
            item.BagFair = Convert.ToInt32(txtBagFair.Text);
            item.TotalFair = Convert.ToInt32(txtTotalFair.Text);
            item.Advance = Convert.ToInt32(txtAdvance.Text);
            item.ItemTypeID = Convert.ToInt32(ddlType.SelectedIndex);
            return item;
        }


        #region Methods For Save
        private void SaveParty()
        {
            if (new PartyManager().SaveParty(FormToObject(PartyID)))
            {
                ClearForm();
            }
        }
        #endregion
    }
}