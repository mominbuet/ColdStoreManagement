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

namespace CSMSys.Web.Controls.Administration.Application
{
    public partial class AreaPO : System.Web.UI.Page
    {
        private enum UIMODE
        {
            NEW,
            EDIT,
            VIEW
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

        private int AreaPOID
        {
            get
            {
                if (ViewState["AreaPOID"] == null)
                    ViewState["AreaPOID"] = -1;
                return (int)ViewState["AreaPOID"];
            }
            set
            {
                ViewState["AreaPOID"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qsUIMODE = Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    AreaPOID = Convert.ToInt32(Request.QueryString["DID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(AreaPOID);

                        pnlNew.Visible = true;
                        pnlView.Visible = false;

                        btnNew.Visible = true;
                        btnSave.Text = "Update";
                        btnNew.Text = "New";
                    }
                    else if (UIMode == UIMODE.VIEW)
                    {
                        LoadToAllControlValue(AreaPOID);

                        pnlNew.Visible = false;
                        pnlView.Visible = true;

                        btnNew.Visible = true;
                        btnSave.Text = "Edit";
                        btnNew.Text = "New";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        pnlView.Visible = false;

                        btnSave.Text = "Save";
                        btnNew.Visible = false;
                    }
                }
                LoadDistrict();
                MultiViewAreaPO.ActiveViewIndex = 0;
            }
        }

        #region Load
        private void LoadDistrict()
        {
            try
            {
                //CSMSysDataContext db = new CSMSysDataContext();
                //var district = (from d in db.ADMDistricts select d).ToList();
                //ddlDistrict.DataSource = district;
                //ddlDistrict.DataTextField = "DistrictName";
                //ddlDistrict.DataValueField = "DistrictID";
                //ddlDistrict.DataBind();

            }
            catch (Exception ex)
            {

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

                    SaveAreaPO();
                    MultiViewAreaPO.ActiveViewIndex = 1;
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "onSuccess();", true);
                }
                else if (UIMode == UIMODE.EDIT)
                {
                    if (!checkValidity()) return;

                    SaveAreaPO();
                    MultiViewAreaPO.ActiveViewIndex = 1;
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "onSuccess();", true);
                }
                else if (UIMode == UIMODE.VIEW)
                {
                    UIMode = UIMODE.EDIT;
                    pnlNew.Visible = true;
                    pnlView.Visible = false;

                    btnNew.Visible = true;
                    btnSave.Text = "Update";
                    btnNew.Text = "New";
                    hdnWindowUIMODE.Value = UIMode.ToString();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "onError();", true);
                MultiViewAreaPO.ActiveViewIndex = 2;
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            UIMode = UIMODE.NEW;
            ClearForm();

            pnlNew.Visible = true;
            pnlView.Visible = false;

            btnNew.Visible = false;
            btnSave.Text = "Save";
            hdnWindowUIMODE.Value = UIMode.ToString();
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
            if (ddlDistrict.SelectedIndex == 0)
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
        private void SaveAreaPO()
        {
            //obj.AreaPOCode = txtCode.Text;
            //obj.AreaPOType = ddlType.SelectedValue.ToString();
            //obj.AreaPOName = txtName.Text;
            //obj.AreaPOFather = txtFather.Text;
            //obj.AreaPOMother = txtMother.Text;
            //obj.AreaPOAddress = txtAddress.Text;
            //obj.AreaPOArea = txtArea.Text;
            //obj.AreaPOEmail = txtEmail.Text;
            //obj.AreaPOCell = txtCell.Text;
            //obj.ReferenceName = txtRefName.Text;
            //obj.ReferenceAddress = txtRefAddress.Text;
            //obj.ReferenceTel = txtRefTel.Text;
            //obj.ReferenceCell = txtRefCell.Text;
            //obj.ReferenceEmail = txtRefEmail.Text;
            //obj.AreaPODOB = DateTime.Parse(txtDOB.Text);
            //obj.JoiningDate = DateTime.Parse(txtJoining.Text);
            //obj.Gender = ddlGender.SelectedValue.ToString();
            //obj.Religion = ddlReligion.SelectedValue.ToString();
            //obj.Active = chkActive.Checked == true ? 1 : 0;
            //obj.PhotoFileName = imgView.ImageUrl.ToString();

            //int intAreaPOID = cBLL.SaveAreaPO(obj);
        }
        #endregion

        #region Control Load Event
        private void LoadToAllControlValue(int intAreaPOID)
        {
            //DataTable dt = new DataTable();
            //dt = new bll_AreaPO().RetrieveAreaPOByID(intAreaPOID);
            //if (dt.Rows.Count > 0)
            //{
            //    txtCode.Text = dt.Rows[0]["AreaPOCode"].ToString();
            //    ddlType.SelectedValue = dt.Rows[0]["AreaPOType"].ToString();
            //    txtName.Text = dt.Rows[0]["AreaPOName"].ToString();
            //    txtFather.Text = dt.Rows[0]["AreaPOFather"].ToString();
            //    txtMother.Text = dt.Rows[0]["AreaPOMother"].ToString();
            //    txtAddress.Text = dt.Rows[0]["AreaPOAddress"].ToString();
            //    txtArea.Text = dt.Rows[0]["AreaPOArea"].ToString();
            //    txtCell.Text = dt.Rows[0]["AreaPOCell"].ToString();
            //    txtEmail.Text = dt.Rows[0]["AreaPOEmail"].ToString();

            //    txtRefName.Text = dt.Rows[0]["ReferenceName"].ToString();
            //    txtRefAddress.Text = dt.Rows[0]["ReferenceAddress"].ToString();
            //    txtRefTel.Text = dt.Rows[0]["ReferenceTel"].ToString();
            //    txtRefCell.Text = dt.Rows[0]["ReferenceCell"].ToString();
            //    txtRefEmail.Text = dt.Rows[0]["ReferenceEmail"].ToString();

            //    ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            //    ddlReligion.SelectedValue = dt.Rows[0]["Religion"].ToString();

            //    chkActive.Checked = dt.Rows[0]["Active"].ToString() == "True" ? true : false;
            //    chkViewActive.Checked = dt.Rows[0]["Active"].ToString() == "True" ? true : false;

            //    lblCode.Text = dt.Rows[0]["AreaPOCode"].ToString();
            //    lblType.Text = dt.Rows[0]["AreaPOType"].ToString();
            //    lblName.Text = dt.Rows[0]["AreaPOName"].ToString();
            //    lblFather.Text = dt.Rows[0]["AreaPOFather"].ToString();
            //    lblMother.Text = dt.Rows[0]["AreaPOMother"].ToString();
            //    lblAddress.Text = dt.Rows[0]["AreaPOAddress"].ToString();
            //    lblArea.Text = dt.Rows[0]["AreaPOArea"].ToString();
            //    lblCell.Text = dt.Rows[0]["AreaPOCell"].ToString();
            //    lblEmail.Text = dt.Rows[0]["AreaPOEmail"].ToString();
            //    lblRefName.Text = dt.Rows[0]["ReferenceName"].ToString();
            //    lblRefAddress.Text = dt.Rows[0]["ReferenceAddress"].ToString();
            //    lblRefTel.Text = dt.Rows[0]["ReferenceTel"].ToString();
            //    lblRefCell.Text = dt.Rows[0]["ReferenceCell"].ToString();
            //    lblRefEmail.Text = dt.Rows[0]["ReferenceEmail"].ToString();
            //    DateTime dtJoining = DateTime.Parse(dt.Rows[0]["JoiningDate"].ToString());
            //    lblJoining.Text = dtJoining.ToShortDateString();
            //    DateTime dtDOB = DateTime.Parse(dt.Rows[0]["AreaPODOB"].ToString());
            //    lblDOB.Text = dtDOB.ToShortDateString();
            //    lblGender.Text = dt.Rows[0]["Gender"].ToString();
            //    lblReligion.Text = dt.Rows[0]["Religion"].ToString();

            //    txtDOB.Text = dtDOB.ToShortDateString();
            //    txtJoining.Text = dtJoining.ToShortDateString();

            //    string strPhoto = dt.Rows[0]["PhotoFileName"].ToString();
            //    imgEdit.ImageUrl = string.IsNullOrEmpty(strPhoto) ? "~/App_Themes/Default/Images/no_preview.jpeg" : strPhoto;
            //    imgView.ImageUrl = string.IsNullOrEmpty(strPhoto) ? "~/App_Themes/Default/Images/no_preview.jpeg" : strPhoto;

            //    Session.Add(ClassConstant.DMMS_AreaPO_ID, intAreaPOID.ToString());
            //}
            //else
            //{
            //    txtCode.Text = string.Empty;
            //    ddlType.SelectedValue = "--Select--";
            //    txtName.Text = string.Empty;
            //    txtFather.Text = string.Empty;
            //    txtMother.Text = string.Empty;
            //    txtAddress.Text = string.Empty;
            //    txtArea.Text = string.Empty;
            //    txtCell.Text = string.Empty;
            //    txtEmail.Text = string.Empty;

            //    txtRefName.Text = string.Empty;
            //    txtRefAddress.Text = string.Empty;
            //    txtRefTel.Text = string.Empty;
            //    txtRefCell.Text = string.Empty;
            //    txtRefEmail.Text = string.Empty;

            //    ddlGender.SelectedValue = "--Select--";
            //    ddlReligion.SelectedValue = "--Select--";

            //    chkActive.Checked = false;
            //    chkViewActive.Checked = false;

            //    lblCode.Text = string.Empty;
            //    lblType.Text = string.Empty;
            //    lblName.Text = string.Empty;
            //    lblFather.Text = string.Empty;
            //    lblMother.Text = string.Empty;
            //    lblAddress.Text = string.Empty;
            //    lblArea.Text = string.Empty;
            //    lblCell.Text = string.Empty;
            //    lblEmail.Text = string.Empty;
            //    lblRefName.Text = string.Empty;
            //    lblRefAddress.Text = string.Empty;
            //    lblRefTel.Text = string.Empty;
            //    lblRefCell.Text = string.Empty;
            //    lblRefEmail.Text = string.Empty;
            //    lblJoining.Text = string.Empty;
            //    lblDOB.Text = string.Empty;

            //    txtDOB.Text = string.Empty;
            //    txtJoining.Text = string.Empty;
            //}
        }
        #endregion
    }
}