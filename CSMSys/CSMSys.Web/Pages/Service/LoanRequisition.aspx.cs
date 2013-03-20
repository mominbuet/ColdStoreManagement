using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer;
using CSMSys.Web.Utility;

namespace CSMSys.Web.Pages.Service
{
    public partial class LoanRequisition : System.Web.UI.Page
    {
        private string strSearch = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                MultiViewSL.ActiveViewIndex = 0;

        }
        protected void change_query(object sender, EventArgs e)
        {
            
            dsParty.SelectCommand = @"select sr.RegistrationID,sr.BagLoan,sr.CarryingLoan,sr.PartyID,sr.SerialNo,ip.PartyName,ip.AreaVillageName,ip.fathername,ip.PartyCode,ip.ContactNo,sr.serialID,convert(varchar(10),sr.createddate,10) as cdate
                                                            from SRVRegistration as sr
                                                            inner JOIN INVParty as ip on sr.PartyID=ip.PartyID
                                                            where sr.Requisitioned='" + RadioButtonList1.SelectedItem.Text + "' order by SerialNo asc;";
                    grvParty.DataBind();
        }
        protected void save_requisition(object sender, EventArgs e)
        {
            bool res = false;
            foreach (ListItem serialid in lstserials.Items)
            {
                SRVRegistration temp=new SRVRegistration();
                SRVRegistration sr = new RegistrationDAOLinq().SearchRegistrationByNo(serialid.Value);
                temp.Requisitioned = "Applied For Loan";
                temp.SerialID = sr.SerialID;
                temp.SerialNo = sr.SerialNo;
                temp.Remarks = sr.Remarks;
                temp.BagLoan = sr.BagLoan;
                temp.CarryingLoan = sr.CarryingLoan;
                temp.PartyID = sr.PartyID;
                temp.LoanDisbursed = 0;
                temp.Bags = 0;
                temp.RegistrationID = sr.RegistrationID;
                temp.CreatedBy = sr.CreatedBy;
                temp.CreatedDate = sr.CreatedDate;
                temp.ModifiedBy = WebCommonUtility.GetCSMSysUserKey();
                temp.ModifiedDate = DateTime.Now;
                res = (new RegistrationManager().SaveRegistration(temp));
                if (!res)
                {
                    //MultiViewSL.ActiveViewIndex = 2;
                    //System.Threading.Thread.Sleep(1200);
                    //MultiViewSL.ActiveViewIndex = 0;
                    lblFailure.Text = "Error Occured";
                }
            }
            if (res)
            {
                ClearForm();
                Response.Redirect("~/pages/service/LoanRequisition.aspx");
                //MultiViewSL.ActiveViewIndex = 1;
                //System.Threading.Thread.Sleep(1200);
                //MultiViewSL.ActiveViewIndex = 0;
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
        public string removespan(string input)
        {
            input = input.Replace("<span class=highlight>", "");
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
            lblpartyid.Text = string.Empty;
            //txtSerialNo.Text = System.DateTime.Today.ToShortDateString();
            lblpartyname.Text = string.Empty;
            lblpartycode.Text = string.Empty;
            lstserials.Items.Clear();
            lbltotalbags.Text = string.Empty;
            //txtfloor.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            //txtline.Text = string.Empty;
            //txtSerialNo.Text = string.Empty;
            grvParty.DataBind();
            btnSave.Enabled = false;
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
                string[] word = e.CommandArgument.ToString().Split('@');
                INVParty invParty = new PartyManager().GetPartyByID(int.Parse(word[1]));
                if (!lblpartycode.Text.Equals(invParty.PartyCode))
                {    
                    lstserials.Items.Clear();
                    lbltotalbags.Text = "0";

                }
                lblpartycode.Text = invParty.PartyCode;
                lblpartyname.Text = invParty.PartyName;
                int serial = new StockSerialNo().getSerialID(word[0]);
                int bag = new StockSerialNo().getbagcount(word[0]);
                lbltotalbags.Text = (int.Parse(lbltotalbags.Text) + bag).ToString();
                 lstserials.Items.Add(new ListItem(word[0],word[2]));

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
                if(btnselect!=null)
                    btnselect.CommandArgument = (DataBinder.Eval(e.Row.DataItem, "serialNo")).ToString() + "@" +
                                            (DataBinder.Eval(e.Row.DataItem, "partyID")).ToString() + "@" +
                                            (DataBinder.Eval(e.Row.DataItem, "serialID")).ToString();
                ;

            }
        }
        #endregion
    }
}