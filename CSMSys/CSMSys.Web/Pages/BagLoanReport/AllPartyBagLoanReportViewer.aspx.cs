using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace CSMSys.Web.Pages.BagLoanRreport
{
    public partial class AllPartyBagLoanReportViewer : System.Web.UI.Page
    {
        private string printer = ConfigurationManager.AppSettings["ServicePrinter"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime parseFromDate = Convert.ToDateTime(Request.QueryString["FD"]);
            string fromDate = parseFromDate.ToShortDateString();
            DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
            string toDate = parseToDate.ToShortDateString();

            lblfrom.Text = fromDate;
            lblto.Text = toDate;
            //SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            //cnn.Open();
            //string query = "exec SP_Party_BagLoan_Report '" + fromDate + "','" + toDate + "'";
            //SqlDataAdapter dscmd = new SqlDataAdapter(query, cnn);
            //dsBagLoan ds = new dsBagLoan();
            //dscmd.Fill(ds, "AllPartyBagloan");
            dsBagloan.SelectCommand = @"SELECT   sbl.BagLoanID, sbl.PartyID, sbl.BagNumber,isNULL(sbl.AmountPerBag,0) as AmountPerBag, isNULL(sbl.LoanAmount,0) as LoanAmount, sbl.Remarks,CONVERT(VARCHAR(10),sbl.CreatedDate, 103) AS CreatedDate, ip.Bagcount as Bagloaded, 
                                                                ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo,
                                                                (SELECT sum(sbl1.BagNumber) from SRVBagLoan as sbl1 where BagLoanID<=sbl.BagLoanID ) as smbags
                                                                FROM dbo.SRVBagLoan AS sbl INNER JOIN
                                                                dbo.INVParty AS ip ON ip.PartyID = sbl.PartyID
                                                            WHERE (sbl.CreatedDate> '" +fromDate+@"') AND 
                                                                  (sbl.CreatedDate < '" + toDate + @"') order by sbl.BagLoanID asc";
            grvPreRequisition.DataBind();
            //cnn.Close();

            //rptAllPartyBagReport objRpt = new rptAllPartyBagReport();
            //objRpt.SetDataSource(ds);
            //objRpt.SetParameterValue("FromDate", fromDate);
            //objRpt.SetParameterValue("ToDate", toDate);
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //objRpt.PrintOptions.PrinterName = printer;
            //objRpt.PrintToPrinter(1, true, 0, 0);
            //AllPartyBagLoanReport.ReportSource = objRpt;


            }

        }

      
    }
