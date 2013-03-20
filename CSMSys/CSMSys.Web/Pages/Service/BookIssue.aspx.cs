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
using CSMSys.Lib.Manager.SRV;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace CSMSys.Web.Pages.Service
{
    public partial class BookIssue : System.Web.UI.Page
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
            if (!IsPostBack)
            {

            }
            
        }

        #region Methods


        private void ClearForm()
        {
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            //txtFather.Text = string.Empty;
            txtPartyType.Text = string.Empty;
            //txtCellNo.Text = string.Empty;
            //txtVillage.Text = string.Empty;
            //txtPO.Text = string.Empty;
            //txtDistrict.Text = string.Empty;
            //txtUpazilla.Text = string.Empty;
            txtBookNo.Text = string.Empty;
            txtPageNo.Text = string.Empty;
            txtRemarks.Text = string.Empty;
     
            //txtBagloans.Text = string.Empty;
            //txtCarryingCost.Text = string.Empty;
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
                //txtFather.Text = _Party.FatherName;
                txtPartyType.Text = _Party.PartyType;
                //txtVillage.Text = _Party.AreaVillageName;
                //txtPO.Text = _Party.AreaPOName;
                //txtCellNo.Text = _Party.Cell;
                //txtDistrict.Text = rptManager.getdist(Convert.ToInt32(_Party.DistrictID)).DistrictName;
                //txtUpazilla.Text = rptManager.getupzilla(Convert.ToInt32(_Party.UpazilaPSID)).UpazilaPSName;
                
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

        
        #endregion
       
        #region from shad
        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("Select"))
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
                    //txtAgreementNo.Text = strID;

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
                btnEdit.CommandArgument = " "+ "@" +
                    ((Label)e.Row.FindControl("lblPartyID")).Text;

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
            grvParty.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvParty.DataBind();
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValidity()) return;
                SaveSRVBookIssue();
            }
            catch (InvalidCastException err)
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "checkit('" + txtRegistrationID.Text + "');", true);
                throw (err);    // Rethrowing exception e
            }
        }
        #endregion

        #region Methods For Save
        private void SaveSRVBookIssue()
        {
            if (new BookIssueManager().SaveBookIssueDetails(FormToObject()))
            {
              ClearForm();
              lblFailure.Text = "Information saved successfully"  ;
              lblFailure.Text = string.Empty;
            }
        }

        private SRVBookIssue FormToObject()
        {
            SRVBookIssue srvBookIssue = new SRVBookIssue();
                                                                     
            srvBookIssue.PartyID = Convert.ToInt32(txtpartycode.Text);
            //tsrv.BagLoan = float.Parse(txtBagloans.Text);
            srvBookIssue.BookNumber = Convert.ToInt32(txtBookNo.Text);
            srvBookIssue.PageNo = txtPageNo.Text;
            srvBookIssue.Remarks = txtRemarks.Text;
            srvBookIssue.CreatedDate = DateTime.ParseExact(txtdatedisbursed.Text, "yyyy/MM/dd", new CultureInfo("en-US"));
            srvBookIssue.CreatedBy = Convert.ToString(WebCommonUtility.GetCSMSysUserKey());
            return srvBookIssue;
        }

        private bool checkValidity()
        {
            if (string.IsNullOrEmpty(txtpartycode.Text))
            {
                lblFailure.Text = "Please Select a Customer";
                return false;
            }
            if (string.IsNullOrEmpty(txtBookNo.Text))
            {
                lblFailure.Text = "Book Number is Required";
                txtBookNo.Focus();
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

        #endregion

    }
}