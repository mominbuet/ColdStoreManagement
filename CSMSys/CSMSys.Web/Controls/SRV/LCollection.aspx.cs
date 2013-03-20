using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.SRV;

using CSMSys.Lib.Manager.INV;
namespace CSMSys.Web.Controls.SRV
{
    public partial class LCollection : System.Web.UI.Page
    {
        #region Private Properties
        private string strSearch = string.Empty;
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

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                MultiViewSerial.ActiveViewIndex = 0;
                grvParty.DataBind();            
            }

        }
        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvParty.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvParty.DataBind();
        }
        #endregion
        #region save
        protected void btnsave_click(object sender, EventArgs e)
        {
            try
            {

                if (!checkValidity()) return;

                SaveLoanCollection();

                //ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                //MultiViewSerial.ActiveViewIndex = 1;
                //System.Threading.Thread.Sleep(3000);
                //MultiViewSerial.ActiveViewIndex = 0;
                lblFailure.Text = "Loan Collection information saved.";
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                MultiViewSerial.ActiveViewIndex = 1;

            }
            catch
            {
                lblFailure.Text = "Error occured. Loan Collection information not saved.";
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
                MultiViewSerial.ActiveViewIndex = 2;
            }
        }
        private void SaveLoanCollection()
        {
            if (new LoanManager().SaveOrEditLoanCollection((FormToObject())))
            {
                ClearForm();
            }
            else
            {
                lblFailure.Text = "data not saved.";
            }
        }
        private SRVLoanCollection FormToObject()
        {

            SRVLoanCollection _temp_lc = new SRVLoanCollection();

            _temp_lc.LoanID = long.Parse(lblloanid.Text);
            _temp_lc.SerialIDs = lblserialids.Text;
            _temp_lc.days = int.Parse(lbldays.Text);
            _temp_lc.PartyID = long.Parse(lblpartyID.Text);
            _temp_lc.TotalLoan = double.Parse(lbltotalamount.Text);
            _temp_lc.InterestAmount = double.Parse(lblinterest.Text);
            _temp_lc.Remarks = txtRemarks.Text;
            _temp_lc.CreatedDate = DateTime.Now;
            _temp_lc.CreatedBy = WebCommonUtility.GetCSMSysUserKey();

            return _temp_lc;
        }
        #endregion
        private void LoadToAllControlValue(int intloanID)
        {
            if (intloanID > 0)
            {
                SRVLoanDisburse srvLoanDisburse = new LoanManager().GetLoanDByID(intloanID);
                lblcaseID.Text = srvLoanDisburse.caseID.ToString();
                lblloanperbag.Text = (Math.Round(double.Parse( srvLoanDisburse.LoanAmount.ToString()),2)).ToString();
                //lbldays.Text = srvLoanDisburse.CreatedDate.ToString();
                DateTime cdate = DateTime.Parse(srvLoanDisburse.CreatedDate.ToString());
                lbldays.Text = Math.Round((DateTime.Now - cdate).TotalDays).ToString() ;
                lstSerial.DataTextField = "SerialNo";
                lstSerial.DataValueField = "SerialID";
                lstSerial.DataSource = new LoanManager().GetAllSerialForCollection(Convert.ToInt32(srvLoanDisburse.PartyID), new StockSerialNo().getSerialFromSerialIDS(srvLoanDisburse.serialIDs));
                lstSerial.DataBind();
            }
        }

        
        protected void lst_indchanged(object sender, EventArgs e)
        {
            //string sel = lstSerial.SelectedItem.Text;
            
            lblbagc.Text = "0";
            lblserialids.Text = String.Empty;

            foreach (ListItem serialid in lstSerial.Items)
            {
                if (serialid.Selected)
                {
                    string[] wor = serialid.Text.Split('/');
                    lblserialids.Text =lblserialids.Text+ serialid.Value+",";
                    lblbagc.Text = (Convert.ToInt32(lblbagc.Text) + Convert.ToInt32(wor[1])).ToString();
                    lblloandesc.Text ="("+ lblbagc.Text + " * " + lblloanperbag.Text + ")";
                    float loans = float.Parse(lblbagc.Text)*float.Parse(lblloanperbag.Text);
                    lblloan.Text = (loans).ToString();


                                   ////sysvariables should be introduced here.
                    float interest = 22*loans*float.Parse(lbldays.Text)/100/365;
                    lblinterest.Text = interest.ToString();
                    lbltotalamount.Text = (interest + loans).ToString();
                }
            }


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
            if (string.IsNullOrEmpty(txtPartyName.Text))
            {
                lblFailure.Text = "Party Name is Required";
                //txtSerial.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lblinterest.Text))
            {
                lblFailure.Text = "Interest is Required";
                //txtBags.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpartyCode.Text))
            {
                lblFailure.Text = "Customer Code is Required";
                //txtCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(lblbagc.Text))
            {
                lblFailure.Text = "Please select a Serial From Serial List";
                //txtCode.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            //txtDate.Text = System.DateTime.Today.ToShortDateString();
            txtPartyName.Text = string.Empty;
            lblinterest.Text = string.Empty;
            lblloan.Text = string.Empty;
            lblbagc.Text = string.Empty; 
            lblcaseID.Text = string.Empty;
            txtpartyCode.Text = string.Empty;

            txtpartyCode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }

        
        #endregion
        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                string strID = words[0].Trim().ToString();
                //string strname = words[1].Trim().ToString();

                int intloanid = Convert.ToInt32(strID);

                if (intloanid > 0)
                {
                    //ddlCustomer.SelectedValue = strID;
                    hdnPartyID.Value = strID;
                    lblloanid.Text = strID;
                    lblpartyID.Text = words[3].Trim();
                    txtPartyName.Text = words[1].Trim();
                    txtpartyCode.Text = words[2].Trim();
                    LoadToAllControlValue(intloanid);
                    
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
                btnEdit.CommandArgument = ((Label)e.Row.FindControl("lblSl")).Text + "@" + ((Label)e.Row.FindControl("lblPartyName")).Text +
                     "@" + ((Label)e.Row.FindControl("lblPartyCode")).Text + "@" + ((Label)e.Row.FindControl("lblPartyID")).Text;
            }
        }
        #endregion
    }
}