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


namespace CSMSys.Web.Pages.INV
{
    public partial class StockUnloading : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        public INVStockLoading _Loading=new INVStockLoading();

        public INVStockSerial _Serial =new INVStockSerial();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvStockSerial.DataBind();
            }
        }
        private void LoadStockLoading(int lid)
        {
            if (lid > 0)
            {
                _Loading = new LoadManager().GetLoadByID(lid);

            }
        }

        private void LoadStockSerial(int intSerialID)
        {
            if (intSerialID > 0)
            {
                _Serial = new SerialManager().GetSerialByID(intSerialID);
            }
        }

        #region Methods
            public
            string HighlightText(string InputTxt)
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
        protected void grvStockSerial_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("unload"))
            {
                string[] commands = e.CommandArgument.ToString().Split('@');
                int serid = Convert.ToInt32(commands[1]);
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                if (row != null)
                {
                    float bags = 0;
                    try
                    {
                        bags = float.Parse(((TextBox) row.FindControl("txtbags")).Text);
                    }
                    catch (Exception ex)
                    {
                        lblFailure.Text = "Cannot get the new bag count.";
                    }
                    
                    if (bags > 0)
                    {
                        float rbags = float.Parse(((Label) row.FindControl("lblbags")).Text) - bags;
                        if (rbags >= 0)
                        {
                            if (serid > 0)
                            {
                                LoadStockSerial(serid);
                                INVStockSerial tserial = _Serial;
                                tserial.Bags = rbags;

                                new SerialManager().SaveSerial(tserial);
                            }
                            int lid = Convert.ToInt32(commands[2]);

                            if (lid > 0)
                            {
                                LoadStockLoading(lid);
                                _Loading.Bags = float.Parse(commands[0]);
                                INVStockLoading tloading = _Loading;

                                tloading.Bags = rbags;
                                new LoadManager().SaveOrEditLocation(tloading);
                            }
                            lblFailure.Text = "Bags unloaded for" + _Loading.SerialNo + ". Bags remaining: " + (float.Parse(((Label)row.FindControl("lblbags")).Text) - bags);
                            
                        }
                        else
                        {
                            lblFailure.Text = "Please insert less or equal bag in the text field.";
                        }
                        
                    }
                }
                else
                {
                    lblFailure.Text = "Please select a Row.";
                }
                grvStockSerial.DataBind();
            }
        }

        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }

        private int cnt = 0;
        protected void grvStockSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                //Label lblid = (Label)e.Row.FindControl("lblid");
                //lblid.Text = cnt++.ToString();

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgunload");
                btnDelete.CommandArgument = (cnt++) + "@" + ((Label)e.Row.FindControl("lblserid")).Text
                    + "@" + ((Label)e.Row.FindControl("lblli")).Text;
                btnDelete.OnClientClick = "'return confirm('Do you want to unload:'" + ((Label)e.Row.FindControl("lblSerialno")).Text + ");'";
                //CompareValidator cmpval = (CompareValidator)e.Row.FindControl("compval");
                //cmpval.ControlToCompare =( (TextBox) e.Row.FindControl("txtbags")).ID;
                //cmpval.ControlToValidate = ((Label)e.Row.FindControl("lblbags")).ID;

            }
        }
        #endregion
        #region Methods For Button  & ImageButton
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            grvStockSerial.DataBind();
        }
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }
        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvStockSerial.DataBind();
        }
        #endregion

    }
}