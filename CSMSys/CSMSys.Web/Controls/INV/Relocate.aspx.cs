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
using System.Threading;
namespace CSMSys.Web.Controls.INV
{
    public partial class Relocate : System.Web.UI.Page
    {
        #region Private Properties
        private INVStockLoading _Loading;
        //private enum UIMODE
        //{
        //    NEW,
        //    EDIT
        //}

        //private UIMODE UIMode
        //{
        //    get
        //    {
        //        if (ViewState["UIMODE"] == null)
        //            ViewState["UIMODE"] = new UIMODE();
        //        return (UIMODE)ViewState["UIMODE"];
        //    }
        //    set
        //    {
        //        ViewState["UIMODE"] = value;
        //    }
        //}

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
            LocationID = Convert.ToInt32(Request.QueryString["LID"]);
            hdnWindowUIMODE.Value = "EDIT";

            if (LocationID > 0 & !IsPostBack)
            {
                LoadToAllControlValue(LocationID);
                //Response.Write(LocationID);
                pnlNew.Visible = true;
                btnSave.Text = "Update";
            }
            MultiViewParty.ActiveViewIndex = 0;

        }
        #region Methods
        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtchamber.Text))
            {
                lblFailure.Text = "Chamber no is Required";
                txtchamber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtfloor.Text))
            {
                lblFailure.Text = "Floor no is Required";
                txtfloor.Focus();
                return false;
            }
           
            if (string.IsNullOrEmpty(txtpocket.Text))
            {
                lblFailure.Text = "Pocket no is Required";
                txtpocket.Focus();
                return false;
            }
            INVStockLoading tsl = new LoadManager().checkloc(Convert.ToInt32(txtchamber.Text), Convert.ToInt32(txtfloor.Text), Convert.ToInt32(txtpocket.Text));
            if (tsl!=null)
            {
                lblFailure.Text = "That Location is already been given to " + tsl.SerialNo + ".";
                //txtpocket.Focus();
                //txtfloor.Focus();
                //txtchamber.Focus();
                //return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtchamber.Text = string.Empty;
            txtfloor.Text = string.Empty;
            
            txtpocket.Text = string.Empty;
        }
        private void LoadToAllControlValue(int lid)
        {
            if (lid > 0)
            {
                _Loading = new LoadManager().GetLoadByID(lid);
                lblSerialNo.Text = _Loading.SerialNo + "/" + _Loading.Bags;
                lblparty.Text = _Loading.PartyCode;
                txtchamber.Text = _Loading.ChamberNo.ToString();
                txtfloor.Text = _Loading.Floor.ToString();
                txtpocket.Text = _Loading.Pocket.ToString();
                txtremarks.Text = _Loading.Remarks;

            }
        }
        #endregion
        #region Methods for SAVE
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if ((UIMode == UIMODE.NEW) || (UIMode == UIMODE.EDIT))
                //{
                if (!checkValidity()) return;

                saveloc();
                //Thread.Sleep(2000);
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewParty.ActiveViewIndex = 1;
                
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewParty.ActiveViewIndex = 2;
            }
        }
        private void saveloc()
        {
            INVStockLoading invStockLoading = new LoadManager().GetLoadByID(LocationID);
            if (new LoadManager().SaveOrEditLocation(FormToObject(LocationID)))
            {
                INVRelocate invRelocate = new INVRelocate();
                invRelocate.RelocationCount = invStockLoading.RelocatedCount;
                invRelocate.CreatedDate = DateTime.Now;
                invRelocate.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                invRelocate.Chamber = invStockLoading.ChamberNo;
                invRelocate.Floor = invStockLoading.Floor;
                invRelocate.Pocket = invStockLoading.Pocket;
                invRelocate.SerialNo = invStockLoading.SerialNo;
                invRelocate.SerialID = invStockLoading.SerialID;
                invRelocate.PartyCode = invStockLoading.PartyCode;
                invRelocate.Remarks = invStockLoading.Remarks;
                new RelocateManager().SaveRelocate(invRelocate);
                ClearForm();
            }
        }
        private INVStockLoading FormToObject(int id)
        {
            INVStockLoading _tempLoading = new LoadManager().GetLoadByID(id);

           
            _tempLoading.Relocated = System.DateTime.Now;
            _tempLoading.RelocatedCount += 1;
            Int32 chamber = Int32.Parse(txtchamber.Text.ToString());
            _tempLoading.ChamberNo = chamber;
            _tempLoading.Floor = Int32.Parse(txtfloor.Text);
            _tempLoading.Pocket = Int32.Parse(txtpocket.Text);
            _tempLoading.Remarks = txtremarks.Text.ToString();

            return _tempLoading;
        }

        #endregion
    }
}