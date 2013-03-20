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
using CSMSys.Web.Pages.BagLoanReport;

namespace CSMSys.Web.Pages.BagLoanRreport
{
    public partial class BagLoanReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            int intPartyID = Convert.ToInt32(Request.QueryString["PID"]);
            int bagLoanID = Convert.ToInt32(Request.QueryString["BID"]);


            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);


            cnn.Open();
            string PartyQuery = "exec SP_Party_Information " + intPartyID;
            SqlDataAdapter daItem = new SqlDataAdapter(PartyQuery, cnn);
            dsBagLoan ds = new dsBagLoan();
            daItem.Fill(ds, "PartyDetails");
            cnn.Close();


            cnn.Open();
            string query = "exec SP_Bag_LoanReport " + bagLoanID;
            SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            //dsPartyReport ds = new dsPartyReport();
            dscmd.Fill(ds, "SRVBagLoan");
            cnn.Close();


            rptBagLoanReport objRpt = new rptBagLoanReport();
            //objRpt.SetDataSource(ds);
            //BagLoanReport.ReportSource = objRpt;
            objRpt.SetDataSource(ds);
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //string printer = "canon"; ;
            objRpt.PrintOptions.PrinterName = printer;
            objRpt.PrintToPrinter(1, true, 0, 0); 

            }

        }

      
    }
