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


namespace CSMSys.Web.Pages.Delivery
{
    public partial class DeliveryReportViewer : System.Web.UI.Page
    {
        private INVParty _Party;
        protected void Page_Load(object sender, EventArgs e)
        {

            int intPartyID = Convert.ToInt32(Request.QueryString["code"]);
            int intDeliveryID = Convert.ToInt32(Request.QueryString["DID"]);
            //string bagLoans = Request.QueryString["bagLoans"];
            //string carryingCost = Request.QueryString["carryingCost"];
            if (intPartyID > 0)
            {
                ReportManager rptManager = new ReportManager();
                //_Party = new PartyManager.GetPartyByID(intPartyID);
                _Party = rptManager.GetPartyByID(intPartyID);

                //  txtCode.Text = _Party.PartyID;
                //txtserid.Text=_Party
                //txtpartycode.Text = _Party.PartyID.ToString();
                //txtCode.Text = _Party.PartyCode;
                String name = _Party.PartyName;
                String fName = _Party.FatherName;
                string partType = _Party.PartyType;
                String village = _Party.AreaVillageName;
                String po = _Party.AreaPOName;
                String cellNo = _Party.Cell;
                string district = rptManager.getdist(Convert.ToInt32(_Party.DistrictID)).DistrictName;
                string upazila = rptManager.getupzilla(Convert.ToInt32(_Party.UpazilaPSID)).UpazilaPSName;

                ReportDocument rptDoc = new ReportDocument();
                dsAgreementReport ds = new dsAgreementReport();
                DataTable dt = new DataTable();
                // dt.TableName = "Crystal Report Example";
                //// dt = getAllOrders(); //This function is located below this function
                // dt = getFakeDbOrders();

                // ds.Tables[0].Merge(dt);
                rptDoc.Load(Server.MapPath("../Delivery/rptDeliveryReport.rpt"));
                rptDoc.FileName = Server.MapPath("../Delivery/rptDeliveryReport.rpt");
                rptDoc.SetDataSource(ds);
                //report.SetParameterValue("@Userid", userid);
                rptDoc.SetParameterValue("CustomerCode", intPartyID);
                rptDoc.SetParameterValue("CustomerName", name);
                rptDoc.SetParameterValue("FatherName", fName);
                rptDoc.SetParameterValue("MobileNo", cellNo);
                rptDoc.SetParameterValue("village", village);
                rptDoc.SetParameterValue("PO", po);
                //rptDoc.SetParameterValue("Bag Loans", bagLoans);
                //rptDoc.SetParameterValue("Carrying Cost", carryingCost);
                rptDoc.SetParameterValue("District", district);
                rptDoc.SetParameterValue("Upazila", upazila);
                CrystalReportViewer1.ReportSource = rptDoc;

            }
            #region Testing
            //string code = Request.QueryString["code"];
            //String name = Request.QueryString["name"];
            //String fName = Request.QueryString["fName"];
            //String village = Request.QueryString["village"];
            //String cellNo = Request.QueryString["cellNo"];
            //String po = Request.QueryString["po"];
            //string bagLoans = Request.QueryString["bagLoans"];
            //string carryingCost = Request.QueryString["carryingCost"];
            //string agreementNo = Request.QueryString["agreementNo"];
            //string district = Request.QueryString["district"];
            //string upazila = Request.QueryString["upazila"];
            // string carryingCost = Request.QueryString["carryingCost"];
            // string carryingCost = Request.QueryString["carryingCost"];

            // ReportDocument rptDoc = new ReportDocument();
            // dsAgreementReport ds = new dsAgreementReport();
            // DataTable dt = new DataTable();
            //// dt.TableName = "Crystal Report Example";
            ////// dt = getAllOrders(); //This function is located below this function
            //// dt = getFakeDbOrders();

            //// ds.Tables[0].Merge(dt);
            // rptDoc.Load(Server.MapPath("../Reports/rptAgreementReport.rpt"));
            // rptDoc.FileName = Server.MapPath("../Reports/rptAgreementReport.rpt");
            // rptDoc.SetDataSource(ds);
            // //report.SetParameterValue("@Userid", userid);
            // rptDoc.SetParameterValue("CustomerCode", code);
            // rptDoc.SetParameterValue("CustomerName", name);
            // rptDoc.SetParameterValue("FatherName", fName);
            // rptDoc.SetParameterValue("MobileNo", cellNo);
            // rptDoc.SetParameterValue("village", village);
            // rptDoc.SetParameterValue("PO", po);
            // rptDoc.SetParameterValue("Bag Loans", bagLoans);
            // rptDoc.SetParameterValue("Carrying Cost", carryingCost);
            // rptDoc.SetParameterValue("District", district);
            // rptDoc.SetParameterValue("Upazila", upazila);

            // CrystalReportViewer1.ReportSource = rptDoc;
            #endregion
        }


    }
}