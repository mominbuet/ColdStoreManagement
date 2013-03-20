using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using System.Globalization;
namespace CSMSys.Web.Controls.SRV
{
    public partial class LDisburse : System.Web.UI.Page
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

        //private INVStockSerial _Serial;
        #endregion
        private string strSearch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvParty.DataBind();
                txtbags_selected.Text = "0";
                //lblcurdate.Text = DateTime.Now.ToString();
                MultiViewSerial.ActiveViewIndex = 0;
                lblcase.Text = ((new LoanManager().GetNextCaseNo()) + 200).ToString();
            }
        }

        protected void txtloanchanged(object sender, EventArgs e)
        {
            imageload.Visible = true;
            int bagcount = 0;
            //System.Threading.Thread.Sleep(500);
            foreach (ListItem li in lstSerial.Items)
            {
                if (li.Selected == true)
                    bagcount += new StockSerialNo().getbagcount(li.Text);
            }
            lbltotloan.Text = (float.Parse(txtloanamount.Text) * float.Parse(txtbags_selected.Text)).ToString();
            imageload.Visible = false;
        }
        protected void txtbags_changed(object sender, EventArgs e)
        {
            int bag_given = int.Parse(txtbags_selected.Text);
            int temp = 0;
            foreach (ListItem li in lstSerial.Items)
            {
                int bagc = new StockSerialNo().getbagcount(li.Text);
                li.Selected = (temp + bagc <= bag_given) ? true : false;
                temp += bagc;
            }
            lblserials.Text = string.Empty;
            foreach (ListItem serialid in lstSerial.Items)
            {
                if (serialid.Selected)
                {
                    lblserials.Text += serialid.Text + ",";

                }
            }
        }
        protected void lst_indchanged(object sender, EventArgs e)
        {
            //string sel = lstSerial.SelectedItem.Text;
            lblserials.Text = string.Empty;
            txtbags_selected.Text = "0";
            foreach (ListItem serialid in lstSerial.Items)
            {
                if (serialid.Selected)
                {
                    lblserials.Text += serialid.Text + ",";
                    string[] wor = serialid.Text.Split('/');
                    txtbags_selected.Text = (Convert.ToInt32(txtbags_selected.Text) + Convert.ToInt32(wor[1])).ToString();
                }
            }


        }
        #region Methods for Save
        protected void save_loan(object sender, EventArgs e)
        {
            try
            {

                if (!checkValidity()) return;

                UpdateSrvRegistration();

                SaveLoanDisburse();
                //ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);
                //MultiViewSerial.ActiveViewIndex = 1;
                //System.Threading.Thread.Sleep(3000);
                //MultiViewSerial.ActiveViewIndex = 0;
                lblFailure.Text = "Loan information saved.";
                grvParty.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onSuccess();", true);

                //imgRefresh_Click(sender,(ImageClickEventArgs) e);

            }
            catch
            {
                lblFailure.Text = "Error occured. Loan information not saved.";
                ScriptManager.RegisterStartupScript(this, GetType(), "onload", "onError();", true);
            }
        }
        private void SaveLoanDisburse()
        {
            if (new LoanManager().SaveOrEditLoanDisburse(FormToObject(Convert.ToInt32(lblcase.Text))))
            {
                ClearForm();
            }
            else
            {
                lblFailure.Text = "data not saved.";
            }
        }
        private void UpdateSrvRegistration()
        {
            int bags_temp = 0;
            int bags = int.Parse(txtbags_selected.Text);
            foreach (ListItem li in lstSerial.Items)
            {
                if (li.Selected == true)
                {
                    if (new LoanManager().updateSRVRegistration(li.Text, int.Parse(txtpartyID.Text),
                                                                float.Parse(txtloanamount.Text), new StockSerialNo().getbagcount(li.Text),int.Parse(lblcase.Text )))
                    bags_temp+= new StockSerialNo().getbagcount(li.Text);
                    else
                        lblFailure.Text = "Error occured, Data not saved.";
                }
                else
                {
                    bags_temp = (bags-bags_temp );
                    if (new LoanManager().updateSRVRegistration(li.Text, int.Parse(txtpartyID.Text),
                                                                float.Parse(txtloanamount.Text), bags_temp,int.Parse(lblcase.Text )))
                        bags = bags_temp;
                    else
                        lblFailure.Text = "Error occured, Data not saved.";
                    
                }
                //bags = (bags >= new StockSerialNo().getbagcount(li.Text))
                //    ? new StockSerialNo().getbagcount(li.Text) - bags
                //    : bags;
            }

        }
        private SRVLoanDisburse FormToObject(int caseid)
        {

            SRVLoanDisburse _loanDisburse = new SRVLoanDisburse();

            _loanDisburse.caseID = caseid;


            //_loanDisburse.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
            //_loanDisburse.ModifiedDate = System.DateTime.Now;

            _loanDisburse.PartyID = Int64.Parse(txtpartyID.Text);
            _loanDisburse.LoanAmount = float.Parse(txtloanamount.Text);
            _loanDisburse.CreatedBy = WebCommonUtility.GetCSMSysUserKey();
            _loanDisburse.CreatedDate = DateTime.ParseExact( txtdatedisbursed.Text,"yyyy/MM/dd",new CultureInfo("en-US"));

            string serialids = string.Empty;
            int bag = 0;
            foreach (ListItem serialid in lstSerial.Items)
            {
                //if (serialid.Selected)
                //{
                    serialids += serialid.Value + ",";
                    bag += new StockSerialNo().getbagcount(serialid.Text);
                //}
            }
            _loanDisburse.Bags = float.Parse(txtbags_selected.Text);
            _loanDisburse.serialIDs = serialids;
            _loanDisburse.Remarks = txtremarks.Text.ToString();

            return _loanDisburse;
        }
        #endregion
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
            if (string.IsNullOrEmpty(txtpartycode.Text))
            {
                lblFailure.Text = "Party Code is Required";
                //txtSerialNo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtloanamount.Text))
            {
                lblFailure.Text = "Per Bag Loan is Required";
                //txtchamber.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtlastdate.Text))
            {
                lblFailure.Text = "Please insert the last date to repays";
                //txtfloor.Focus();
                return false;
            }
            //if (lstSerial.SelectedIndex <= 0)
            //{
            //    lblFailure.Text = "Please select a serial from the list";
            //    //txtfloor.Focus();
            //    return false;
            //}

            return true;
        }

        private void ClearForm()
        {
            txtlastdate.Text = string.Empty;
            //txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            txtbags_selected.Text = string.Empty;
            //lblcurdate.Text = DateTime.Now.ToString();
            //ddlCustomer.SelectedIndex = 0;
            txtloanamount.Text = string.Empty;
            txtpartyID.Text = string.Empty;
            txtpartycode.Text = string.Empty;
            txtremarks.Text = string.Empty;
            lstSerial.Items.Clear();
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
                //string strID = words[0].Trim().ToString();
                //string strCode = words[1].Trim().ToString();

                //txtparty.Text = text;
                if (words[0] != string.Empty && words[1] != string.Empty)
                {
                    IList<SRVRegistration> srv = new LoanManager().GetAllSerialByPartyForRequisition(int.Parse(words[0]), "Applied For Loan");

                    txtpartyID.Text = words[0].Trim();
                    txtpartycode.Text = words[1].Trim();
                    //lblbags.Text = words[3].Trim();

                    lstSerial.DataTextField = "SerialNo";
                    lstSerial.DataValueField = "SerialID";

                    lstSerial.DataSource = srv;
                    lstSerial.DataBind();
                    float bags = 0;
                    foreach (SRVRegistration srvRegistration in srv)
                    {
                         //INVStockSerial ss = new ser
                        bags += new StockSerialNo().getbagcount(srvRegistration.SerialNo);
                    }
                    lblbagsApplied.Text = bags.ToString();
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
                btnselect.CommandArgument = (DataBinder.Eval(e.Row.DataItem, "partyid")).ToString() + "@" +
                                            (DataBinder.Eval(e.Row.DataItem, "partycode")).ToString();

                IList<SRVLoanDisburse> srv =
                    new LoanDAOLinq().getAllLoansByParty(
                        Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "partyid")));
                double loandisbursed = 0;
                foreach (SRVLoanDisburse srvloan in srv)
                {
                    loandisbursed += (Convert.ToDouble(srvloan.Bags * srvloan.LoanAmount));

                }
                ((Label) e.Row.FindControl("lbltotbags")).Text =
                    new PartyManager().GetPartyByID(int.Parse((DataBinder.Eval(e.Row.DataItem, "partyid")).ToString()))
                                      .bagcount.ToString();
                ((Label) e.Row.FindControl("lbltotloandisbursed")).Text = loandisbursed.ToString();
                
                //Label lblserial = (Label)e.Row.FindControl("lblSl");
                //Label lblloaded = (Label)e.Row.FindControl("lblloaded");
                //LoadManager lm = new LoadManager();
                //INVStockLoading sl = new INVStockLoading();
                //sl = lm.GetLoadBySerial(lblserial.Text);
                //lblloaded.Text = (sl == null) ? "No" : "Yes";

                //btnselect.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }
        #endregion
    }
}