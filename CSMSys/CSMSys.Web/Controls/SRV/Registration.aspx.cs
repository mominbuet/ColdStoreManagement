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
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.DataAccessLayer.Implementations;

namespace CSMSys.Web.Controls.SRV
{
    public partial class Registration : System.Web.UI.Page
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

        private int RegistrationID
        {
            get
            {
                if (ViewState["RegistrationID"] == null)
                    ViewState["RegistrationID"] = -1;
                return (int)ViewState["RegistrationID"];
            }
            set
            {
                ViewState["RegistrationID"] = value;
            }
        }

        private SRVRegistration _Registration = new SRVRegistration();
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    RegistrationID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(RegistrationID);

                        pnlNew.Visible = true;
                        btnSave.Text = "Update";
                    }
                    else if (UIMode == UIMODE.NEW)
                    {
                        pnlNew.Visible = true;
                        btnSave.Text = "Save";
                    }
                }
                MultiViewRegistration.ActiveViewIndex = 0;
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

                    SaveRegistration();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewRegistration.ActiveViewIndex = 1;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewRegistration.ActiveViewIndex = 2;
            }
        }

        #endregion
        #region Methods For Grid
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                //string text = e.CommandArgument.ToString();
                //string[] words = text.Split('@');
                //string strID = words[0].Trim().ToString();
                //string strCode = words[1].Trim().ToString();

                //string serid = words[2].Trim().ToString();
                ////int intPartyID = Convert.ToInt32(strID);

                //if (text != null)
                //{
                //    //ddlCustomer.SelectedValue = strID;
                //    //hdnPartyID.Value = strID;
                //    txtAgreementNo.Text = strID;
                //    txtserid.Text = serid;
                //    int partyID = Convert.ToInt32(strCode);
                //    try
                //    {
                //        LoadToAllControlValue(partyID);
                //        //txtCode.Text = strCode;
                //        //btnSave.Enabled = true;
                //    }
                //    catch (InvalidCastException err)
                //    {
                //        throw (err);    // Rethrowing exception e
                //    }
                    btnSave.Enabled = true;

                //}
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
                //ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                //btnEdit.CommandArgument = ((Label)e.Row.FindControl("lblSerialNo")).Text + "@" + ((Label)e.Row.FindControl("lblpid")).Text
                //    + "@" + ((Label)e.Row.FindControl(("lblSerialID"))).Text;

            }
        }
        #endregion
        #region Methods
        
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
            SRVRegistration msrv = new RegistrationDAOLinq().PickByID(id);
            SRVRegistration tsrv = new SRVRegistration();
            tsrv.SerialID = Convert.ToInt32(txtserid.Text);
            tsrv.PartyID = Convert.ToInt32(txtpartycode.Text);
            //tsrv.BagLoan = float.Parse(txtBagloans.Text);
            tsrv.CarryingLoan = string.IsNullOrEmpty(txtCarryingCost.Text) ? 0 : float.Parse(txtCarryingCost.Text);
            tsrv.Remarks = txtRemarks.Text;
            tsrv.SerialNo = txtAgreementNo.Text;
            tsrv.Requisitioned = msrv.Requisitioned;
            tsrv.ModifiedDate = msrv.ModifiedDate;
            tsrv.BagLoan = string.IsNullOrEmpty(txtEmptyBag.Text) ? 0 : Convert.ToInt32(txtEmptyBag.Text);
            tsrv.LoanDisbursed = 0;
            tsrv.Bags = 0;
            tsrv.BagWeight = ddlWeight.SelectedIndex <= 0 ? 85 : Convert.ToInt32(ddlWeight.SelectedItem.Text);
            tsrv.CreatedDate = DateTime.Now;
            tsrv.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
            tsrv.RegistrationID = id;


            return tsrv;
        }

        private void LoadToAllControlValue(int intRegistrationID)
        {
            if (intRegistrationID > 0)
            {
                //UIUtility.BindTypeDDL(ddlType, 0, false);
                UIUtility.BindBagDDL(ddlWeight, 0, false);
                ddlWeight.SelectedIndex = 1;
                //txtRegistrationID.Text = ((new RegistrationManager().GetRegistrationID()) + 1).ToString();
                

                _Registration = new RegistrationDAOLinq().SearchRegistrationByNo(intRegistrationID.ToString());
                txtserid.Text = _Registration.SerialID.ToString();
                txtRegistrationID.Text = _Registration.RegistrationID.ToString();
                txtRemarks.Text = _Registration.Remarks;
                txtCarryingCost.Text = _Registration.CarryingLoan.ToString();
                txtEmptyBag.Text = _Registration.BagLoan.ToString();
                txtAgreementNo.Text = _Registration.SerialNo;
                INVParty party = new PartyDAOLinq().PickByID(Convert.ToInt32(new SerialDAOLinq().GetSumByParty("select partyid as smbags from srvregistration where serialid=" + intRegistrationID)));
                txtpartycode.Text = party.PartyID.ToString();
                txtName.Text = party.PartyName;
                txtCode.Text = party.PartyCode;
                txtPartyType.Text = party.PartyType;

            }
        }
        #endregion
        #region Methods For Button
        string strSearch = string.Empty;
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
            return true;
        }
        #region Methods For Save
        private void SaveRegistration()
        {
            if (new RegistrationManager().SaveRegistration(FormToObject(int.Parse(txtRegistrationID.Text))))
            {
                ClearForm();
            }
        }
        #endregion
    }
}