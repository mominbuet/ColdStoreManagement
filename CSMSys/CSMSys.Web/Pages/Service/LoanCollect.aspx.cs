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
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Manager.INV;
namespace CSMSys.Web.Pages.Service
{
    public partial class LoanCollect : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        private IList<INVStockSerial> iss;
        //private INVStockSerial _Serial;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                iss = new SerialManager().GetAllSerial();
                grvloan.DataBind();
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
        #endregion
        #region Methods For Grid
        protected void grvloan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Delete"))
            //{
            //    int intPartyID = Convert.ToInt32(e.CommandArgument.ToString());

            //    if (intPartyID > 0)
            //    {
            //        DeleteParty(intPartyID);
            //    }
            //}
        }

        protected void grvloan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvloan.PageIndex = e.NewPageIndex;
            grvloan.DataBind();
        }

        protected void grvloan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                Label serials = (Label)e.Row.FindControl("lblserials");
                        //INVStockSerial isto = iss.Where(t => t.SerialID.Equals(long.Parse(serid))).First();
                IList<INVStockSerial> iss =
                    new StockSerialNo().getSerialFromSerialIDS(DataBinder.Eval(e.Row.DataItem, "serialIDs").ToString());
                foreach (INVStockSerial invStockSerial in iss)
                {
                    Label bags = (Label)e.Row.FindControl("lbltotalbagi");
                    bags.Text =( Convert.ToInt32(bags.Text) + new StockSerialNo().getbagcount(invStockSerial.SerialNo)).ToString();
                    serials.Text += invStockSerial.SerialNo+",";    
                }
                

                
                //string[] serids = DataBinder.Eval(e.Row.DataItem, "serialIDs").ToString().Split(',');

                //int bagcount = 0;
                //foreach (string serid in serids)
                //{
                //    if (serid != "")
                //    {
                        
                //        Label lbltotalbag = (Label)e.Row.FindControl("lbltotalbag");
                //        lbltotalbag.Text = (float.Parse(lbltotalbag.Text) + isto.Bags).ToString();
                //        bagcount += new StockSerialNo().getbagcount(isto.SerialNo);

                //    }


                //}
                //((Label)e.Row.FindControl("lbltotalbagi")).Text = bagcount.ToString();
                //((Label)e.Row.FindControl("lbltotalLoan")).Text =
                //            (bagcount *
                //             float.Parse(DataBinder.Eval(e.Row.DataItem, "LoanAmount").ToString())).ToString();
            }
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvloan.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvloan.DataBind();
        }
        #endregion
        //#region Methods For Delete
        //private void DeleteLoan(int id)
        //{
        //    SRVLoanDisburse _tempSerial = new SRVLoanDisburse();
        //    //_Serial.SerialID = id;
        //    if (id > 0)
        //    {
        //        _tempSerial = new LoanManager().GetLoanByID(id);
        //        if (new LoanManager().DeleteLoan(_tempSerial))
        //        {
        //            grvloan.DataBind();
        //        }
        //    }
        //}
        //#endregion
    }
}