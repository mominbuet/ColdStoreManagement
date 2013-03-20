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

namespace CSMSys.Web.Pages.Administration.Application
{
    public partial class AreaPOSetup : System.Web.UI.Page
    {
        private string strSearch = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grvAreaPO.DataBind();
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

        #region Methods For Bind
        private void BindGrid()
        {
            DataTable dt = new DataTable();
            //bll_Distributor cBLL = new bll_Distributor();
            //dt.Clear();
            //dt = cBLL.RetrieveAllDistributor();

            if (dt.Rows.Count != 0)
            {
                grvAreaPO.DataSource = dt;
                grvAreaPO.DataBind();
            }
            else
            {
                //Other wise add a emtpy "New Row" to the datatable and then hide it after binding.

                dt.Rows.Add(dt.NewRow());
                grvAreaPO.DataSource = dt;
                grvAreaPO.DataBind();
                grvAreaPO.Rows[0].Visible = false;
            }
        }
        #endregion

        #region Methods For Grid
        //protected void grvAreaPO_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.Equals("Delete"))
        //    {
        //        int intDistributorID = Convert.ToInt32(e.CommandArgument.ToString());

        //        if (intDistributorID > 0)
        //        {
        //            bll_Distributor cBLL = new bll_Distributor();
        //            bo_Distributor obj = new bo_Distributor();

        //            obj.DistributorID = intDistributorID;
        //            cBLL.DeleteDistributor(obj);

        //            //BindGrid();

        //            grvAreaPO.DataBind();

        //            //Displaying alert message after successfully deletion of user
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Distributor deleted successfully')", true);
        //        }
        //    }
        //}

        protected void grvAreaPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvAreaPO.PageIndex = e.NewPageIndex;
            grvAreaPO.DataBind();
        }

        protected void grvAreaPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            {
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                btnView.Attributes.Add("onClick", "javascript:ShowViewModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                btnEdit.Attributes.Add("onClick", "javascript:ShowEditModal('" + ((Label)e.Row.Cells[1].Controls[1]).Text + "');");

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text;
            }
        }

        protected void grvAreaPO_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int intDistributorID = Convert.ToInt32(((Label)grvAreaPO.Rows[e.RowIndex].FindControl("lblID")).Text.Trim().ToString());

            if (intDistributorID > 0)
            {
                //bll_Distributor cBLL = new bll_Distributor();
                //bo_Distributor obj = new bo_Distributor();

                //obj.DistributorID = intDistributorID;
                //cBLL.DeleteDistributor(obj);

                //grvAreaPO.DataBind();

                ////Displaying alert message after successfully deletion of user
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Distributor deleted successfully')", true);
            }
        }
        #endregion

        #region Methods For Button
        protected void imgNew_Click(object sender, ImageClickEventArgs e)
        {
            imgNew.Attributes.Add("onClick", "javascript:ShowNewModal();");
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            strSearch = txtSearch.Text;
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            txtSearch.Text = string.Empty;
            grvAreaPO.DataBind();
        }
        #endregion
    }
}