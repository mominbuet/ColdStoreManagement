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


namespace CSMSys.Web.Pages.LoanRequisitionReport
{
    public partial class RequisitionReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime parseFromDate =  Convert.ToDateTime(Request.QueryString["FD"]);
            string fromDate = parseFromDate.ToShortDateString();
            DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
            string toDate = parseToDate.ToShortDateString();
            int intReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
           //  int intSerialID = Convert.ToInt32(Request.QueryString["SID"]);
            
            if (intReportID == 1)
            {
                ShowPreRequisitionReport(fromDate, toDate);
            }

            else if (intReportID == 2)
            {
                ShowRequisitionReport(fromDate, toDate);
            }

            
             }

        #region PreRequisition Report
        private void ShowPreRequisitionReport(string fromDate, string toDate)
        {
       
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            cnn.Open();
            string query = "exec Sp_Requisition_Report '" + fromDate + "','" + toDate + "'";
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            dsLoanRequisition ds = new dsLoanRequisition();
            dscmd.Fill(ds, "LoanRequisitionApproved");
            cnn.Close();

            rptLoanRequisitionReport objRpt = new rptLoanRequisitionReport();
            objRpt.SetDataSource(ds);
            objRpt.SetParameterValue("FromDate", fromDate);
            objRpt.SetParameterValue("ToDate", toDate);
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            objRpt.PrintOptions.PrinterName = printer;
            objRpt.PrintToPrinter(1, true, 0, 0);
            LoanApprovalRreport.ReportSource = objRpt;

            //rptStockReport objRpt = new rptStockReport();
            //objRpt.SetDataSource(ds);
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //objRpt.PrintOptions.PrinterName = "canon";
            //objRpt.PrintToPrinter(1, true, 0, 0); 
        }
        #endregion


        #region PreRequisition Report
        private void ShowRequisitionReport(string fromDate, string toDate)
        {

                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                cnn.Open();
                string query = "exec Sp_After_Requisition_Report '" + fromDate + "','" + toDate + "'";
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                dsLoanRequisition ds = new dsLoanRequisition();
                dscmd.Fill(ds, "RequisitionReportData");
                cnn.Close();


            rptAfterRequisitionReport objRpt = new rptAfterRequisitionReport();
            objRpt.SetDataSource(ds);
            objRpt.SetParameterValue("FromDate", fromDate);
            objRpt.SetParameterValue("ToDate", toDate);
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            objRpt.PrintOptions.PrinterName = printer;
            objRpt.PrintToPrinter(1, true, 0, 0);
            LoanApprovalRreport.ReportSource = objRpt;


            //rptStockReport objRpt = new rptStockReport();
            //objRpt.SetDataSource(ds);
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //objRpt.PrintOptions.PrinterName = "canon";
            //objRpt.PrintToPrinter(1, true, 0, 0); 
        }
        #endregion

     }

        
    }
