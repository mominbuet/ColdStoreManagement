using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Web.Pages.StockSerialReport;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using CSMSys.Lib.Model;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.INV;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;


namespace CSMSys.Web.Pages.StockSerialReport
{
    public partial class SerialReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["StorePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime parseFromDate =  Convert.ToDateTime(Request.QueryString["FD"]);
            string fromDate = parseFromDate.ToShortDateString();
            DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
            string toDate = parseToDate.ToShortDateString();
           //  int intSerialID = Convert.ToInt32(Request.QueryString["SID"]);

            lblfrom.Text = fromDate;
            lblto.Text = toDate;
            dsBagloan.SelectCommand = @"SELECT ROW_NUMBER() OVER (ORDER BY invss.serialID asc) As SlNo,invss.SerialNo,invss.Bags,ip.PartyName,ip.PartyCode,ip.fathername,ip.areavillagename,ip.ContactNo,convert(varchar(10),invss.CreatedDate,103) as cdate,
                                    (select sum(bags) from INVStockSerial as invss1 where Serial<=invss.Serial) as smbags
                                    from INVStockSerial as invss
                                    INNER JOIN INVParty as ip on invss.partyID=ip.partyID
                                        WHERE (invss.CreatedDate> '" + fromDate + @"') AND 
                                        (invss.CreatedDate < '" + toDate + @"') order by invss.Serial";
            grvPreRequisition.DataBind();

        }
        #region grid
        protected void grvPreRequisition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPreRequisition.PageIndex = e.NewPageIndex;
            grvPreRequisition.DataBind();
        }
        int sumbags=0;
        protected void grvPreRequisition_databind(object sender, GridViewRowEventArgs e)
        {
            //if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.Cells[1].Controls.Count > 0))
            //{
            //    sumbags += int.Parse(DataBinder.Eval(e.Row.DataItem, "bags").ToString());
            //    Label lblsumbags = (Label)e.Row.FindControl("lblsumbags");
            //    lblsumbags.Text = sumbags.ToString();
            //}
        }
        #endregion

    }

        
    }
