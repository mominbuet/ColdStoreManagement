using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.Model;
using CSMSys.Lib.Manager.INV;
namespace CSMSys.Web.Pages.Service
{
    public partial class Delivery : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            MultiViewSL.ActiveViewIndex = 0;
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
        public string removespan(string input)
        {
            input= input.Replace("<span class=highlight>", "");
            input = input.Replace("</span", "");
            return input;
        }
        public string ReplaceKeyWords(Match m)
        {
            return ("<span class=highlight>" + m.Value + "</span>");
        }

        private bool checkValidity()
        {
            //if (string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    lblFailure.Text = "Serial No is Required";
            //    txtSerialNo.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtbags.Text))
            //{
            //    lblFailure.Text = "No of Bags is Required";
            //    txtbags.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtchamber.Text))
            //{
            //    lblFailure.Text = "Chamber Number is Required";
            //    txtchamber.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtfloor.Text))
            //{
            //    lblFailure.Text = "Floor Number is Required";
            //    txtfloor.Focus();
            //    return false;
            //}
            //if (string.IsNullOrEmpty(txtpocket.Text))
            //{
            //    lblFailure.Text = "Pocket Number is Required";
            //    txtpocket.Focus();
            //    return false;
            //}
            //INVStockLoading tsl = new LoadManager().checkloc(Convert.ToInt32(txtchamber.Text), Convert.ToInt32(txtfloor.Text), Convert.ToInt32(txtpocket.Text));
            //if(tsl!=null)
            //{
            //    lblFailure.Text="That Location is already been given to "+tsl.SerialNo+"/"+tsl.Bags+".";
            //    //txtpocket.Focus();
            //    //txtfloor.Focus();
            //    //txtchamber.Focus();
            //    return false;
            //}
            return true;
        }

        private void ClearForm()
        {
            //txtparty.Text = string.Empty;
            ////txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            //txtbags.Text = string.Empty;
            //txtchamber.Text = string.Empty;
            ////ddlCustomer.SelectedIndex = 0;
            //txtpocket.Text = string.Empty;
            //txtfloor.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            //txtline.Text = string.Empty;
            //txtSerialNo.Text = string.Empty;
        }
        #endregion

        protected void txtchanged(object sender, EventArgs e)
        {
            //txtcarrycost.Visible = false;
            //txtemptybag.Visible = false;
            txtcarrycost.Text =( float.Parse(txthidcarrycost.Text)/float.Parse(txthidbag.Text)*float.Parse(txtbags.Text)).ToString();
            txtemptybag.Text = (float.Parse(txthidemptybag.Text) / float.Parse(txthidbag.Text) * float.Parse(txtbags.Text)).ToString();
            txtkeeping.Text = (float.Parse(txthidkeeping.Text) / float.Parse(txthidbag.Text) * float.Parse(txtbags.Text)).ToString();
            txtloandisbursed.Text = (float.Parse(txthidloandisbursed.Text) / float.Parse(txthidbag.Text) * float.Parse(txtbags.Text)).ToString();
            //txtcarrycost.Visible = true;
            //txtemptybag.Visible = true;
            lbltotalamount.Text = (float.Parse(txtcarrycost.Text) + float.Parse(txtkeeping.Text) +float.Parse(txtloandisbursed.Text)+
                                   float.Parse(txtemptybag.Text)).ToString();
            if (float.Parse(txtloandisbursed.Text) > 0)
                btnSave.Visible = true;


        }

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
                //string text = e.CommandArgument.ToString();
                string[] words = e.CommandArgument.ToString().Split('@');
                Int32 serialid = Convert.ToInt32(words[3]);
                txtpartyname.Text = new PartyManager().GetPartyByID(Convert.ToInt32(words[0])).PartyName;
                INVStockSerial invss = new SerialManager().GetSerialByID(serialid);
                txtSerialNo.Text = invss.SerialNo;
                txtbags.Text = words[5];
                txtcarrycost.Text = words[1];
                txtemptybag.Text = words[2];
                txthidcarrycost.Text = words[1];
                txthidemptybag.Text = words[2];
                txthidbag.Text = words[5];
                
                //DManager dm = new DManager();
                //SRVRegistration srvreg = dm.getRegBySerialNo(serialid);
                //IList<INVItemDetail> titemdetail = dm.getItemDetailByRegID(srvreg.RegistrationID);
                //double keepcharge = 0;
                //foreach (INVItemDetail invItemDetail in titemdetail)
                //{
                //    keepcharge += (Convert.ToDouble(invItemDetail.TotalFair) - Convert.ToDouble(invItemDetail.Advance));
                //}
                txtkeeping.Text = words[4].ToString();
                txthidkeeping.Text = words[4].ToString();
                
                float loanamount = new DManager().retValidDelivery(serialid);
                if (Math.Round(loanamount,2) < 0)
                {
                    btnSave.Visible = false;
                    lblFailure.Text = "This user have some("+Math.Round(loanamount,2)*(-1)+") Loans to clear from his account.";
                    txtloandisbursed.Text = (Math.Round(loanamount, 2)*(-1)).ToString();
                    
                }
                else
                {
                    txtloandisbursed.Text = "0";
                    lblFailure.Text = "";
                    btnSave.Visible = true;
                }
                txthidloandisbursed.Text = txtloandisbursed.Text;
                lbltotalamount.Text = (float.Parse(txtcarrycost.Text) + float.Parse(txtkeeping.Text) + float.Parse(txtloandisbursed.Text)+
                                       float.Parse(txtemptybag.Text)).ToString();
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
                Label lblkeepcharge = (Label)e.Row.FindControl("lblkeepcharge");
                lblkeepcharge.Text = (double.Parse(DataBinder.Eval(e.Row.DataItem, "TotalFair").ToString()) - double.Parse(DataBinder.Eval(e.Row.DataItem, "Advance").ToString())).ToString();
                
                ImageButton btnselect = (ImageButton)e.Row.FindControl("imgselect");
                btnselect.CommandArgument = (((Label)e.Row.FindControl("lblPartyID")).Text) + "@" +
                    ((Label)e.Row.FindControl("lblcarryingcost")).Text + "@" +
                     ((Label)e.Row.FindControl("lblemptybagloan")).Text + "@"
                     + ((Label)e.Row.FindControl("lblSl")).Text+"@"+
                     lblkeepcharge.Text + "@" + ((Label)e.Row.FindControl("lblbags")).Text;


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