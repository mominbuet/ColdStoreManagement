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
using CSMSys.Web.Pages.PartyReports;
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


namespace CSMSys.Web.Pages.PartyReports
{
    public partial class PartyReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            int intPartyID = Convert.ToInt32(Request.QueryString["PID"]);

            // int intPartyID = Convert.ToInt32(Request.QueryString["PID"]);
            // string intPartyID = Convert.ToInt32(Request.QueryString["PID"]);
            int intReportID = Convert.ToInt32(Request.QueryString["ReportID"]);

            if (intReportID == 1)
            {
                ShowAgreementReport(intPartyID);
            }

            else if (intReportID == 2)
            {
                ShowRelocationReport(intPartyID);
            }

            else if (intReportID == 3)
            {
                ShowCollectionReport(intPartyID);
            }

            else if (intReportID == 4)
            {
                ShowLoanReport(intPartyID);
            }

            else if (intReportID == 5)
            {
                ShowDeliveryReport(intPartyID);
            }

            else
            {

            }

            #region Test
            //SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            //cnn.Open();
            //string query = "exec SP_Relocate_Stock " + intPartyID;
            //SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            //dsPartyReport ds = new dsPartyReport();
            //dscmd.Fill(ds, "ReportData");
            //cnn.Close();

            //cnn.Open();
            //string PartyQuery = "exec SP_Party_Information " + intPartyID;
            //SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
            ////dsPartyReport ds = new dsPartyReport();
            //daItem.Fill(ds, "PartyDetails");
            //cnn.Close();

            //rptRelocationReport objRpt = new rptRelocationReport();
            //objRpt.SetDataSource(ds);
            //PartyRreport.ReportSource = objRpt;

            // ReportDocument rptDoc = new ReportDocument();
            // rptDoc.Load(Server.MapPath("../Reports/PartyReports/rptRelocationReport.rpt"));
            // rptDoc.FileName = Server.MapPath("../Reports/PartyReports/rptRelocationReport.rpt");
            //// rptAgreementReport objRpt = new rptAgreementReport();
            // rptDoc.SetDataSource(ds);
            // //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            // //rptDoc.PrintOptions.PrinterName = "canon";
            // //rptDoc.PrintToPrinter(1, true, 0, 0); 
            // PartyRreport.ReportSource = rptDoc;
            #endregion


        }

        #region Agreement Report
        private void ShowAgreementReport(int intPartyID)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                cnn.Open();
                string query = "exec SP_Party_Agreement_Report " + intPartyID;
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                dsPartyReport ds = new dsPartyReport();
                dscmd.Fill(ds, "AgreementReport");
                cnn.Close();

                cnn.Open();
                string PartyQuery = "exec SP_Party_Information " + intPartyID;
                SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                daItem.Fill(ds, "PartyDetails");
                cnn.Close();

                //rptPartyAgreementReport objRpt = new rptPartyAgreementReport();
                //objRpt.SetDataSource(ds);
                //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                //objRpt.PrintOptions.PrinterName = printer;
                //objRpt.PrintToPrinter(1, true, 0, 0);

                //PartyRreport.ReportSource = objRpt;
            }

            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Relocation Report
        private void ShowRelocationReport(int intPartyID)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                cnn.Open();
                string query = "exec SP_Relocate_Stock " + intPartyID;
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                dsPartyReport ds = new dsPartyReport();
                dscmd.Fill(ds, "RelocationReport");
                cnn.Close();

                cnn.Open();
                string PartyQuery = "exec SP_Party_Information " + intPartyID;
                SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                //dsPartyReport ds = new dsPartyReport();
                daItem.Fill(ds, "PartyDetails");
                cnn.Close();

                //rptRelocationReport objRpt = new rptRelocationReport();
                //objRpt.SetDataSource(ds);
                //PartyRreport.ReportSource = objRpt;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Collection Report
        private void ShowCollectionReport(int intPartyID)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                cnn.Open();
                string query = "exec SP_Relocate_Stock " + intPartyID;
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                dsPartyReport ds = new dsPartyReport();
                dscmd.Fill(ds, "ReportData");
                cnn.Close();

                cnn.Open();
                string PartyQuery = "exec SP_Party_Information " + intPartyID;
                SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                //dsPartyReport ds = new dsPartyReport();
                daItem.Fill(ds, "PartyDetails");
                cnn.Close();

                //rptRelocationReport objRpt = new rptRelocationReport();
                //objRpt.SetDataSource(ds);
                //PartyRreport.ReportSource = objRpt;
            }

            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Loan Report
        private void ShowLoanReport(int intPartyID)
        {
            try
            {


                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);


                cnn.Open();
                string PartyQuery = "exec SP_Party_Information " + intPartyID;
                SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                dsPartyReport ds = new dsPartyReport();
                daItem.Fill(ds, "PartyDetails");
                cnn.Close();


                cnn.Open();
                string query = "exec SP_Party_LoanDisburse_Report " + intPartyID;
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                //dsPartyReport ds = new dsPartyReport();
                dscmd.Fill(ds, "LoanDisburseReport");
                cnn.Close();

                //cnn.Open();
                //string PartyQuery = "exec SP_Party_Information " + intPartyID;
                //SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                ////dsPartyReport ds = new dsPartyReport();
                //daItem.Fill(ds, "PartyDetails");
                //cnn.Close();

                //rptPartyLoanReport objRpt = new rptPartyLoanReport();
                //objRpt.SetDataSource(ds);
                //PartyRreport.ReportSource = objRpt;
            }

            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Delivery Report
        private void ShowDeliveryReport(int intPartyID)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                cnn.Open();
                string query = "exec SP_Relocate_Stock " + intPartyID;
                SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
                dsPartyReport ds = new dsPartyReport();
                dscmd.Fill(ds, "ReportData");
                cnn.Close();

                cnn.Open();
                string PartyQuery = "exec SP_Party_Information " + intPartyID;
                SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
                //dsPartyReport ds = new dsPartyReport();
                daItem.Fill(ds, "PartyDetails");
                cnn.Close();

                //rptRelocationReport objRpt = new rptRelocationReport();
                //objRpt.SetDataSource(ds);
                //PartyRreport.ReportSource = objRpt;
            }

            catch (Exception ex)
            {
            }
        }
        #endregion

    }


}
