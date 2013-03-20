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

namespace CSMSys.Web.Controls.INV
{
    public partial class Serial : System.Web.UI.Page
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

        private int SerialID
        {
            get
            {
                if (ViewState["SerialID"] == null)
                    ViewState["SerialID"] = -1;
                return (int)ViewState["SerialID"];
            }
            set
            {
                ViewState["SerialID"] = value;
            }
        }

        private string strSearch = string.Empty;
        private INVStockSerial _Serial;
        ComboData _ComboData = new ComboData();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //UIUtility.BindCustomerDDL(ddlCustomer, false);
                txtDate.Text = System.DateTime.Today.ToShortDateString();
                string qsUIMODE = string.IsNullOrEmpty(Request.QueryString["UIMODE"]) ? "NEW" : Request.QueryString["UIMODE"];
                if (string.IsNullOrEmpty(qsUIMODE) == false)
                {
                    UIMode = (UIMODE)Enum.Parse(typeof(UIMODE), qsUIMODE);
                    SerialID = Convert.ToInt32(Request.QueryString["PID"]);
                    hdnWindowUIMODE.Value = UIMode.ToString();

                    if (UIMode == UIMODE.EDIT)
                    {
                        LoadToAllControlValue(SerialID);

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

                    string serial = txtSerial.Text;
                    SaveSerial();
                    ClearForm();
                    //Response.Redirect("~/Pages/StockSerialReport/StockReportViewer.aspx?SID=" + serial);
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

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
            grvParty.DataBind();
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvParty.DataBind();
        }
        #endregion
        protected void txtbag_changed(object sender, EventArgs e)
        {
            //txtSerialNo.Text = txtSerial.Text + "/" + txtBags.Text;
        }

        #region Methods
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

        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtSerial.Text))
            {
                lblFailure.Text = "Serial No is Required";
                txtSerial.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtBags.Text))
            {
                lblFailure.Text = "No of Bags is Required";
                txtBags.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblFailure.Text = "Customer Code is Required";
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

        private void ClearForm()
        {
            txtDate.Text = System.DateTime.Today.ToShortDateString();
            //txtSerialNo.Text = string.Empty;
            txtSerial.Text = string.Empty;
            //ddlCustomer.SelectedIndex = 0;
            txtCode.Text = string.Empty;
            txtBags.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }

        private INVStockSerial FormToObject(int id)
        {

            INVStockSerial _temp_serial = new INVStockSerial();
            
            if (id > 0)
            {
                _temp_serial = new SerialManager().GetSerialByID(id);
                
            }
            else
            {
                _temp_serial.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
                _temp_serial.CreatedDate = System.DateTime.Now;
                _temp_serial.SerialDate = DateTime.Now;
            }
                

            _temp_serial.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            _temp_serial.ModifiedDate = System.DateTime.Now;

            //_temp_serial.SerialID = id;
            
            _temp_serial.Serial = Convert.ToInt32(txtSerial.Text);
            _temp_serial.Bags = float.Parse(txtBags.Text);
            _temp_serial.SerialNo = (txtSerial.Text+"/"+txtBags.Text).ToString();
            _temp_serial.PartyCode = txtCode.Text.ToString();
            _temp_serial.PartyID = Convert.ToInt32(hdnPartyID.Value.ToString());
            //_temp_serial.SerialDate =_Serial.SerialDate;
            _temp_serial.Remarks = txtRemarks.Text;

            return _temp_serial;
        }

        private void LoadToAllControlValue(int intSerialID)
        {
            if (intSerialID > 0)
            {
                _Serial = new SerialManager().GetSerialByID(intSerialID);
                hdnPartyID.Value = _Serial.PartyID.ToString();
                txtSerial.Text = _Serial.Serial.ToString();
                txtBags.Text = _Serial.Bags.ToString();
                txtSerialNo.Text = _Serial.SerialNo;
                //ddlCustomer.SelectedValue = _Serial.PartyID.ToString();
                txtCode.Text = _Serial.PartyCode;
                txtDate.Text = DateTime.Parse(_Serial.SerialDate.ToString()).ToShortDateString();
                txtRemarks.Text = _Serial.Remarks;
            }
        }
        #endregion

        #region Methods For Save
        private void SaveSerial()
        {
            try
            {
                INVStockSerial iss = new INVStockSerial();
                iss = FormToObject(SerialID);
                if (new SerialManager().SaveSerial(iss))
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewSerial.ActiveViewIndex = 2;
            }
        }
        #endregion

        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('/');
                string strID = words[0].Trim().ToString();
                string strCode = words[1].Trim().ToString();
                if (UIMode != UIMODE.EDIT)
                {
                    lblseridtobe.Text = "Next serial should be " +
                                        (new SerialManager().getnextserial() ).ToString();
                    txtSerial.Text = "" + (new SerialManager().getnextserial() ).ToString();
                }
                int intPartyID = Convert.ToInt32(strID);

                if (intPartyID > 0)
                {
                    //ddlCustomer.SelectedValue = strID;
                    hdnPartyID.Value = strID;

                    ReportManager rptManager = new ReportManager();
                    INVParty _Party = rptManager.GetPartyByID(intPartyID);

                    //  txtCode.Text = _Party.PartyID;
                    txtCode.Text = _Party.PartyCode;
                    //txtCode.Text = strCode;
                    
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
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text + " / " + ((Label)e.Row.Cells[3].Controls[1]).Text;
            }
        }
        #endregion
    }
}