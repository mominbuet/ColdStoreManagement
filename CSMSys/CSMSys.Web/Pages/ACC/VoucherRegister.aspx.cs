using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CSMSys.Web.Pages.ACC
{
    public partial class VoucherRegister : System.Web.UI.Page
    {
        private string strSearch = string.Empty;
        SqlConnection formConn = null;
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;

        private DateTime TransDateFrom
        {
            get
            {
                if (ViewState["TransDateFrom"] == null)
                    ViewState["TransDateFrom"] = -1;
                return (DateTime)ViewState["TransDateFrom"];
            }
            set
            {
                ViewState["TransDateFrom"] = value;
            }
        }

        private DateTime TransDateTo
        {
            get
            {
                if (ViewState["TransDateTo"] == null)
                    ViewState["TransDateTo"] = -1;
                return (DateTime)ViewState["TransDateTo"];
            }
            set
            {
                ViewState["TransDateTo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TransDateFrom = DateTime.Today;
                TransDateTo = DateTime.Today;
                txtDateFrom.Text = TransDateFrom.ToShortDateString();
                txtDateTo.Text = TransDateTo.ToShortDateString();
                grvRegister.DataBind();
            }
            //else
            //{
            //    for (int i = 0; i < this.grvRegister.Rows.Count; i++)
            //    {
            //        //After Postback ID's get lost.  Javascript will not work without it,
            //        //so we must set them back.
            //        this.grvRegister.Rows[i].Cells[0].ID = "CellInfo" + i.ToString();
            //    }

            //    //If it is a postback that is not from the grid, we have to expand the rows
            //    //the user had expanded before.  We have to check first who called this postback
            //    //by checking the Event Target.  The reason we check this, is because we don't need 
            //    //to expand if it is changing the page of the datagrid, or sorting, etc...
            //    if (Request["__EVENTTARGET"] != null)
            //    {
            //        string strEventTarget = Request["__EVENTTARGET"].ToString().ToLower();

            //        //gvTransaction is the name of the grid.  If you modify the grid name,
            //        //make sure to modify this if statement.
            //        if (strEventTarget.IndexOf("grvRegister") == -1)
            //        {
            //            if (!Page.IsStartupScriptRegistered("HierarchicalGrid"))
            //            {
            //                Page.RegisterStartupScript("HierarchicalGrid", "<script>ShowExpandedDivInfo('" + this.txtExpandedDivs.ClientID + "','" + this.grvRegister.ClientID + "');</script>");
            //            }
            //        }
            //    }
            //}
        }

        #region Methods For Grid
        protected void grvRegister_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Delete"))
            {
                int intAttendanceID = Convert.ToInt32(e.CommandArgument.ToString());

                if (intAttendanceID > 0)
                {
                    //DeleteClass(intAttendanceID);
                }
            }
            else if (e.CommandName.Equals("Edit"))
            {
                string text = e.CommandArgument.ToString();
                string[] words = text.Split('@');

                if (words[0] != string.Empty && words[1] != string.Empty)
                {
                    int voucher = int.Parse(words[1].Trim());
                    int transaction = int.Parse(words[0].Trim());
                    Response.Redirect("~/Pages/ACC/VoucherNew.aspx?Voucher=" + voucher + "&TMID=" + transaction);
                }
            }
        }

        protected void grvRegister_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRegister.PageIndex = e.NewPageIndex;
            grvRegister.DataBind();
        }

        protected void grvRegister_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[2].Controls.Count > 0))
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                //btnEdit.Attributes.Add("onClick", "Response.Redirect("~/Pages/VoucherNew.aspx?Voucher=1")");
                btnEdit.CommandArgument = ((Label)e.Row.Cells[1].Controls[1]).Text+ "@" +((Label)e.Row.Cells[4].Controls[1]).Text;

                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                btnDelete.CommandArgument = ((Label)e.Row.Cells[2].Controls[1]).Text;

                string TMID = ((Label)e.Row.Cells[1].Controls[1]).Text;
                string strSQL = "SELECT ta.AccountNo + ' - ' + ta.AccountTitle AS Description, td.CreditAmt AS [Cr Amount], td.DebitAmt AS [Dr Amount], td.Comments FROM T_Transaction_Detail AS td INNER JOIN T_Account AS ta ON td.AccountID = ta.AccountID WHERE (td.TransMID = " + int.Parse(TMID) + ")";
                SqlConnection SqlCon = new SqlConnection(connstring);
                SqlDataAdapter da = new SqlDataAdapter(strSQL, SqlCon);

                DataSet dsDetail = new DataSet();

                da.Fill(dsDetail);

                //Create a new GridView for displaying the expanded details
                GridView gv = new GridView();

                gv.DataSource = dsDetail;
                gv.ID = "dsDetail" + e.Row.RowIndex; //Since a gridview is being created for each row they each need a unique ID, so append the row index
                gv.AutoGenerateColumns = true;
                gv.Width = Unit.Percentage(100.00);
                //gv.CssClass = "tablesorterBlue";
                gv.DataBind();

                SetProps(gv);

                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
                gv.RenderControl(htw);

                string DivStart = "<div id='uniquename" + e.Row.RowIndex.ToString() + "' style='DISPLAY: block;'>";
                string DivBody = sw.ToString();
                string DivEnd = "</div>";
                string FullDIV = DivStart + DivBody + DivEnd;

                int LastCellPosition = e.Row.Cells.Count - 1;
                int NewCellPosition = e.Row.Cells.Count - 2;

                e.Row.Cells[0].ID = e.Row.RowIndex.ToString();  //"CellInfo" + e.Row.RowIndex.ToString();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[LastCellPosition].Text = e.Row.Cells[LastCellPosition].Text + "</td><tr><td bgcolor='f5f5f5'></td><td colspan='" + NewCellPosition + "'>" + FullDIV;
                }
                else
                {
                    e.Row.Cells[LastCellPosition].Text = e.Row.Cells[LastCellPosition].Text + "</td><tr><td bgcolor='d3d3d3'></td><td colspan='" + NewCellPosition + "'>" + FullDIV;
                }

                //e.Row.Cells[0].Attributes["onclick"] = "HideShowPanel('uniquename" + e.Row.RowIndex.ToString() + "'); ChangePlusMinusText('" + e.Row.Cells[0].ClientID + "'); SetExpandedDIVInfo('" + e.Row.Cells[0].ClientID + "','" + TMID + "', 'uniquename" + e.Row.RowIndex.ToString() + "');";
                //e.Row.Cells[0].Attributes["onmouseover"] = "this.style.cursor='pointer'";
                //e.Row.Cells[0].Attributes["onmouseout"] = "this.style.cursor='pointer'";
            }
        }

        public void SetProps(GridView gv)
		{
			/****************************************************************************/
			gv.Font.Size = 8;
			gv.Font.Bold = false;
			gv.Font.Name = "tahoma";

			/*******************************Professional 2**********************************/
			//Border Props 
			gv.GridLines = GridLines.Both;
			gv.CellPadding = 3;
			gv.CellSpacing = 0;
			gv.BorderColor = System.Drawing.Color.FromName("#CCCCCC");
			gv.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);

			
			//Header Props
            gv.HeaderStyle.BackColor = System.Drawing.Color.White;
            //gv.HeaderStyle.ForeColor = System.Drawing.Color.White;
            gv.HeaderStyle.BorderColor = System.Drawing.Color.FromName("#CCCCCC");
            gv.HeaderStyle.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
            gv.HeaderStyle.Font.Bold = false;
			gv.HeaderStyle.Font.Size = 8;
			gv.HeaderStyle.Font.Name = "tahoma";

            //gv.ItemStyle.BackColor = System.Drawing.Color.LightSteelBlue;

		}
        #endregion

        #region SqlDataSource Control Event Handlers
        protected void dsRegister_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@TransDateFrom"].Value = TransDateFrom;
            e.Command.Parameters["@TransDateTo"].Value = TransDateTo;
        }
        #endregion

        #region Methods For Button
        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            TransDateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
            TransDateTo = DateTime.Parse(txtDateTo.Text.ToString());
            grvRegister.DataBind();
        }
        #endregion
    }
}