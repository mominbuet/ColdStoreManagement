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
    public partial class StockLoading : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private INVStockLoading _Loading;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvParty.DataBind();

                MultiViewSL.ActiveViewIndex = 0;
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
            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                lblFailure.Text = "Serial No is Required";
                txtSerialNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtbags.Text))
            {
                lblFailure.Text = "No of Bags is Required";
                txtbags.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtchamber.Text))
            {
                lblFailure.Text = "Chamber Number is Required";
                txtchamber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtfloor.Text))
            {
                lblFailure.Text = "Floor Number is Required";
                txtfloor.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpocket.Text))
            {
                lblFailure.Text = "Pocket Number is Required";
                txtpocket.Focus();
                return false;
            }
            INVStockLoading tsl = new LoadManager().checkloc(Convert.ToInt32(txtchamber.Text), Convert.ToInt32(txtfloor.Text), Convert.ToInt32(txtpocket.Text));
            if(tsl!=null)
            {
                lblFailure.Text="That Location is already been given to "+tsl.SerialNo+".";
                //txtpocket.Focus();
                //txtfloor.Focus();
                //txtchamber.Focus();
                //return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtparty.Text = string.Empty;
            //txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            txtbags.Text = string.Empty;
            txtchamber.Text = string.Empty;
            //ddlCustomer.SelectedIndex = 0;
            txtpocket.Text = string.Empty;
            txtfloor.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
        }
        #endregion
        #region Methods for Save
        protected void save_loading(object sender, EventArgs e)
        {
            try
            {

                if (!checkValidity()) return;

                SaveSerial();

                //ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                //MultiViewSL.ActiveViewIndex = 1;
                //System.Threading.Thread.Sleep(3000);
                //MultiViewSL.ActiveViewIndex = 0;
                lblFailure.Text = (lblFailure.Text.Contains("already")) ? lblFailure.Text += " Loading information saved." : " Loading information saved.";
                grvParty.DataBind();
                //imgRefresh_Click(sender,(ImageClickEventArgs) e);

            }
            catch (Exception ex)
            {
                lblFailure.Text = "Error occured. Loading information not saved."+ex.Message;
            }
        }
        private void SaveSerial()
        {
            if (new LoadManager().SaveOrEditLocation(FormToObject(0)))
            {
                ClearForm();
            }
        }
        private INVStockLoading FormToObject(int id)
        {
            if (id <= 0)
            {
                _Loading = new INVStockLoading();

                _Loading.RelocatedCount = 0;
            }

            //_Loading.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            //_Loading.ModifiedDate = System.DateTime.Now;

            _Loading.SerialID = Int64.Parse(lblslid.Text);
            _Loading.PartyCode = txtparty.Text;
            _Loading.Bags = float.Parse(txtbags.Text);
            _Loading.SerialNo = txtSerialNo.Text;
            Int32 chamber = Int32.Parse(txtchamber.Text.ToString());
            _Loading.ChamberNo = chamber;
            _Loading.Floor = Int32.Parse(txtfloor.Text);
            _Loading.Pocket = Int32.Parse(txtpocket.Text);
            _Loading.Remarks = txtRemarks.Text.ToString();
            _Loading.LoadedDate = DateTime.Now;
            return _Loading;
        }
        #endregion
        #region Methods for Search   & refresh
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
        #region Methods For Grid
        protected void grvParty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Select"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');
                
                if (words[0] != string.Empty && words[1] != string.Empty && words[2] != string.Empty)
                {
                    lblslid.Text = words[0].Trim();
                    txtSerialNo.Text = words[3].Trim();
                    txtbags.Text = words[1].Trim();
                    txtparty.Text = words[2].Trim();
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
                ImageButton btnselect = (ImageButton)e.Row.FindControl("imgselect");
                btnselect.CommandArgument = (DataBinder.Eval(e.Row.DataItem,"SlNo")).ToString() + "@" +
                    (DataBinder.Eval(e.Row.DataItem, "Bags")).ToString() + "@" +
                      (DataBinder.Eval(e.Row.DataItem, "PartyCode")).ToString() + "@" +
                    (DataBinder.Eval(e.Row.DataItem, "SerialNo")).ToString();


                Label lblserial = (Label)e.Row.FindControl("lblSl");
                Label lblloaded = (Label)e.Row.FindControl("lblloaded");
                LoadManager lm = new LoadManager();
                INVStockLoading sl = new INVStockLoading();
                sl = lm.GetLoadBySerial(lblserial.Text);
                lblloaded.Text = (sl==null) ? "No" : "Yes";
                          
                //btnselect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion
    }
}