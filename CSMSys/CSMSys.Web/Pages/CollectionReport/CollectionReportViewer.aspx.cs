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
using System.Data.OleDb;

namespace CSMSys.Web.Pages.CollectionReport
{
    public partial class CollectionReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();

        private INVParty _Party;
        ReportDocument rptDoc = new ReportDocument();
        protected void Page_Load(object sender, EventArgs e)
        {
            int intPartyID = Convert.ToInt32(Request.QueryString["partyID"]);
            string serialID = Request.QueryString["serialID"];
              
           
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            cnn.Open();
            string query = "exec SP_Loan_Collection " + intPartyID + ",'" + serialID +"'";
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            dsCollection ds = new dsCollection();
            dscmd.Fill(ds, "SRVLoanCollection");
            cnn.Close();

            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(Server.MapPath("../Reports/CollectionReport/rptCollectionReport.rpt"));
            rptDoc.FileName = Server.MapPath("../Reports/CollectionReport/rptCollectionReport.rpt");
           // rptAgreementReport objRpt = new rptAgreementReport();
            rptDoc.SetDataSource(ds);
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            rptDoc.PrintOptions.PrinterName = printer;
            rptDoc.PrintToPrinter(1, true, 0, 0); 

            //rptCollectionReport objRpt = new rptCollectionReport();
            //objRpt.SetDataSource(ds);
            //CollectionReport.ReportSource = objRpt;

            }

        }

      
    }
