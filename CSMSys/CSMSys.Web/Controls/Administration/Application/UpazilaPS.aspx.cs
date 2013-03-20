using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.Administration.Application;

namespace CSMSys.Web.Controls.Administration.Application
{
    public partial class UpazilaPS : System.Web.UI.Page
    {
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

        private int UpazilaPSID
        {
            get
            {
                if (ViewState["UpazilaPSID"] == null)
                    ViewState["UpazilaPSID"] = -1;
                return (int)ViewState["UpazilaPSID"];
            }
            set
            {
                ViewState["UpazilaPSID"] = value;
            }
        }

        private int DistrictID
        {
            get
            {
                if (ViewState["DistrictID"] == null)
                    ViewState["DistrictID"] = -1;
                return (int)ViewState["DistrictID"];
            }
            set
            {
                ViewState["DistrictID"] = value;
            }
        }

        private int DivisionID
        {
            get
            {
                if (ViewState["DivisionID"] == null)
                    ViewState["DivisionID"] = -1;
                return (int)ViewState["DivisionID"];
            }
            set
            {
                ViewState["DivisionID"] = value;
            }
        }

        #region Private Properties
        private ADMUpazilaPS _UpazilaPS;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qsUIMODE = Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    UpazilaPSID = Convert.ToInt32(Request.QueryString["UID"]);
                    DistrictID = string.IsNullOrEmpty(Request.QueryString["DID"]) ? 0 : Convert.ToInt32(Request.QueryString["DID"]);
                    DivisionID = string.IsNullOrEmpty(Request.QueryString["VID"]) ? 0 : Convert.ToInt32(Request.QueryString["VID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(UpazilaPSID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                LoadDivision();
                MultiViewUpazilaPS.ActiveViewIndex = 0;
            }
        }

        #region Methods For Dropdown Load
        private void LoadDivision()
        {
            try
            {
                IList<ADMDivision> division = new DivisionManager(true).GetAllDivision();

                ddlDivision.DataSource = division;
                ddlDivision.DataTextField = "DivisionName";
                ddlDivision.DataValueField = "DivisionID";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("<<<....Select Division....>>>", "-1"));
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadDistrict(int DivisionID)
        {
            try
            {
                IList<ADMDistrict> district = new DistrictManager(true).GetAllDistrictByDivision(DivisionID);

                ddlDistrict.DataSource = district;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("<<<....Select District....>>>", "-1"));
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDivision.SelectedValue.ToString()) > 0)
            {
                LoadDistrict(int.Parse(ddlDivision.SelectedValue));
            }
        }
        #endregion

        #region Methods for Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (UIMode == UIMODE.NEW)
                {
                    if (!checkValidity()) return;

                    SaveUpazilaPS();
                    MultiViewUpazilaPS.ActiveViewIndex = 1;
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "onSuccess();", true);
                }
                else if (UIMode == UIMODE.EDIT)
                {
                    if (!checkValidity()) return;

                    SaveUpazilaPS();
                    MultiViewUpazilaPS.ActiveViewIndex = 1;
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "onSuccess();", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "onError();", true);
                MultiViewUpazilaPS.ActiveViewIndex = 2;
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
            if (ddlDistrict.SelectedIndex == -1)
            {
                lblFailure.Text = "District is Required";
                ddlDistrict.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblFailure.Text = "Name is Required";
                txtName.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            ddlDistrict.SelectedIndex = 0;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            lblCode.Text = string.Empty;
            lblDistrict.Text = string.Empty;
            lblName.Text = string.Empty;
            lblDescription.Text = string.Empty;
        }
        #endregion

        #region Methods For Save
        private void SaveUpazilaPS()
        {
            new UpazilaPSManager(false).SaveUpazilaPS(FormToObject(UpazilaPSID));
        }

        private ADMUpazilaPS FormToObject(int id)
        {
            if (id <= 0)
            {
                _UpazilaPS = new ADMUpazilaPS();

                _UpazilaPS.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
            }

            _UpazilaPS.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();

            _UpazilaPS.DistrictID = int.Parse(ddlDistrict.SelectedItem.Value);
            _UpazilaPS.UpazilaPSID = id;

            _UpazilaPS.UpazilaPSCode = txtCode.Text;
            _UpazilaPS.UpazilaPSName = txtName.Text;
            _UpazilaPS.Description = txtDescription.Text;

            return _UpazilaPS;
        }
        #endregion

        #region Control Load Event
        private void LoadToAllControlValue(int upazilaPSID)
        {
            _UpazilaPS = new UpazilaPSManager(true).GetUpazilaPSByID(upazilaPSID);

            if (_UpazilaPS != null)
            {
                LoadDistrict(DivisionID);
                ddlDivision.SelectedValue = DivisionID.ToString();
                ddlDistrict.SelectedValue = DistrictID.ToString();
                txtCode.Text = _UpazilaPS.UpazilaPSCode;
                txtName.Text = _UpazilaPS.UpazilaPSName;
                txtDescription.Text = _UpazilaPS.Description;
            }
            else
            {
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
            }
        }
        #endregion
    }
}