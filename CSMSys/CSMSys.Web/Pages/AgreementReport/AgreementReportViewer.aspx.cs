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
using CSMSys.Web.Pages;
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


namespace CSMSys.Web.Pages.AgreementReport
{
    public partial class AgreementReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            int intRegistrationID = Convert.ToInt32(Request.QueryString["RID"]);

             SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            cnn.Open();
            string query = "exec SP_Agreement_Report " + intRegistrationID;
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            dsAgreementReport ds = new dsAgreementReport();
            dscmd.Fill(ds, "ReportData");
            cnn.Close();

            cnn.Open();
            string itemQuery = @" SELECT     itd.RegistrationID,itd.ItemDetailID, it.TypeName, itd.BagNo, itd.BagWeight, itd.BagFair, itd.TotalFair, itd.Advance
                                                                            FROM          dbo.INVItemDetail as itd INNER JOIN dbo.INVItemType AS it ON itd.ItemTypeID = it.TypeID
                                                                            WHERE itd.RegistrationID = " + intRegistrationID;
            SqlDataAdapter daItem = new SqlDataAdapter(itemQuery, cnn);
            //dsAgreementReport ds = new dsAgreementReport();
            daItem.Fill(ds, "ItemDetail");
            cnn.Close();

            //ReportDocument rptDoc = new ReportDocument();
            //rptDoc.Load(Server.MapPath("../Reports/AgreementReport/rptAgreementReport.rpt"));
            //rptDoc.FileName = Server.MapPath("../Reports/AgreementReport/rptAgreementReport.rpt");
            //rptAgreementReport objRpt = new rptAgreementReport();
            //rptDoc.SetDataSource(ds);
            //AgreementRreport.ReportSource = rptDoc;
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //rptDoc.PrintOptions.PrinterName = "canon";
            //rptDoc.PrintToPrinter(1, true, 0, 0);

            rptAgreementReport objRpt = new rptAgreementReport();
            objRpt.SetDataSource(ds);
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            objRpt.PrintOptions.PrinterName = printer;
            objRpt.PrintToPrinter(1, true, 0, 0);
            AgreementRreport.ReportSource = objRpt;

             }


     }

        
    }
