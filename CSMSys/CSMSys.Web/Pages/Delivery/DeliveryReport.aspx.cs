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
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace CSMSys.Web.Pages.Delivery
{
    public partial class DeliveryReport : System.Web.UI.Page
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
        ComboData _ComboData = new ComboData();
        #endregion

        String strSearch = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
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
        private bool checkValidity()
        {
            return true;
        }

        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtFather.Text = string.Empty;
            txtPartyType.Text = string.Empty;
            txtCellNo.Text = string.Empty;
            txtVillage.Text = string.Empty;
            txtPO.Text = string.Empty;
            txtDistrict.Text = string.Empty;
            txtUpazilla.Text = string.Empty;
            txtBagloans.Text = string.Empty;
            txtCarryingCost.Text = string.Empty;
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

            _Party.PartyCode = txtCode.Text;
            _Party.PartyName = txtName.Text;
            _Party.FatherName = txtFather.Text;
            _Party.PartyType = txtPartyType.Text;
            _Party.Cell = txtCellNo.Text;
            
            _Party.AreaVillageName = txtVillage.Text;
            _Party.AreaPOName = txtPO.Text;
            //_Party.UpazilaPSID = ddlUpazila.SelectedIndex > 0 ? Convert.ToInt32(ddlUpazila.SelectedValue.ToString()) : 0;
            //_Party.DistrictID = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue.ToString()) : 0;
            //_Party.Tel = txtTel.Text;
            //_Party.Cell = txtCell.Text;
            //_Party.Email = txtEmail.Text;

            _Party.PartyCodeName = txtCode.Text + " - " + txtName.Text;
            _Party.IsActive = true;

            return _Party;
        }

        private void LoadToAllControlValue(int intPartyID)
        {
            if (intPartyID > 0)
            {
                ReportManager rptManager = new ReportManager();
                //_Party = new PartyManager.GetPartyByID(intPartyID);
                _Party = rptManager.GetPartyByID(intPartyID);

              //  txtCode.Text = _Party.PartyID;
                txtCode.Text = _Party.PartyCode;
                txtpartycode.Text = _Party.PartyID.ToString();
                txtName.Text = _Party.PartyName;
                txtFather.Text = _Party.FatherName;
                txtPartyType.Text = _Party.PartyType;
                txtVillage.Text = _Party.AreaVillageName;
                txtPO.Text = _Party.AreaPOName;
                txtCellNo.Text = _Party.Cell;
                txtDistrict.Text = rptManager.getdist(Convert.ToInt32(_Party.DistrictID)).DistrictName;
                txtUpazilla.Text = rptManager.getupzilla(Convert.ToInt32(_Party.UpazilaPSID)).UpazilaPSName;
                
            }
        }
        #endregion
        #region Methods
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

        //private bool checkValidity()
        //{
        //    if (string.IsNullOrEmpty(txtSerial.Text))
        //    {
        //        lblFailure.Text = "Serial No is Required";
        //        txtSerial.Focus();
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(txtBags.Text))
        //    {
        //        lblFailure.Text = "No of Bags is Required";
        //        txtBags.Focus();
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(txtCode.Text))
        //    {
        //        lblFailure.Text = "Customer Code is Required";
        //        txtCode.Focus();
        //        return false;
        //    }
        //    //if (ddlCustomer.SelectedIndex <= 0)
        //    //{
        //    //    lblFailure.Text = "Customer Code is Required";
        //    //    ddlCustomer.Focus();
        //    //    return false;
        //    //}
        //    return true;
        //}

        //private void ClearForm()
        //{
        //    txtDate.Text = System.DateTime.Today.ToShortDateString();
        //    txtSerialNo.Text = string.Empty;
        //    txtSerial.Text = string.Empty;
        //    //ddlCustomer.SelectedIndex = 0;
        //    txtCode.Text = string.Empty;
        //    txtBags.Text = string.Empty;
        //    txtRemarks.Text = string.Empty;
        //}

        
        #endregion
        #region Methods For Save
        private void SaveParty()
        {
            if (new PartyManager().SaveParty(FormToObject(PartyID)))
            {
                ClearForm();
            }
        }
        #endregion

        protected void btnReport_Click(object sender, EventArgs e)
        {

            Response.Redirect("../CollectionReport/CollectionReportViewer.aspx?code=" + txtpartycode.Text + "&bagLoans=" + txtBagloans.Text + "&carryingCost=" + txtCarryingCost.Text
                    + "&remarks=" + txtName.Text);

           // Response.Redirect("../LoanDisburseReport/LoanReportViwer.aspx?code=" + txtpartycode.Text + "&bagLoans=" + txtBagloans.Text + "&carryingCost=" + txtCarryingCost.Text
           //         + "&remarks=" + txtName.Text);

            //Response.Redirect("../Delivery/DeliveryReportViewer.aspx?code=" + txtpartycode.Text + "&bagLoans=" + txtBagloans.Text + "&carryingCost=" + txtCarryingCost.Text
            //        + "&remarks=" + txtName.Text);
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

                //int intPartyID = Convert.ToInt32(strID);

                if (text!=null)
                {
                    //ddlCustomer.SelectedValue = strID;
                    //hdnPartyID.Value = strID;
                    txtAgreementNo.Text = strID;

                    int partyID = Convert.ToInt32(strCode);
                    try
                    {
                        LoadToAllControlValue(partyID);
                        //txtCode.Text = strCode;
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
                btnEdit.CommandArgument = ((Label)e.Row.FindControl("lblSerialNo")).Text + "@" + ((Label)e.Row.FindControl("lblpid")).Text;

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

    }
}