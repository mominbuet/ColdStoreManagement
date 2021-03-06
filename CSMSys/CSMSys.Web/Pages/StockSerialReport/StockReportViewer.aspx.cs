﻿using System;
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
    public partial class StockReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["StorePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
             int intSerialID = Convert.ToInt32(Request.QueryString["SID"]);

             SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            cnn.Open();
            string query = "exec SP_Stock_Report " + Request.QueryString["SID"];
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            dsStockSerial ds = new dsStockSerial();
            dscmd.Fill(ds, "ReportData");
            cnn.Close();

            //rptStockReport objRpt = new rptStockReport();
            //objRpt.SetDataSource(ds);
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //objRpt.PrintOptions.PrinterName = printer;
            //objRpt.PrintToPrinter(1, true, 0, 0); 

            //ReportDocument rptDoc = new ReportDocument();
            //rptDoc.Load(Server.MapPath("../Reports/StockSerialReport/rptStockReport.rpt"));
            //rptDoc.FileName = Server.MapPath("../Reports/StockSerialReport\rptStockReport.rpt");
            ////rptStockReport objRpt = new rptStockReport();
            //rptDoc.SetDataSource(ds);
            //StockSerialRreport.ReportSource = rptDoc;  

             }


     }

        
    }
