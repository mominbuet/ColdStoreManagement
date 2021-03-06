﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingDataAccess;
using CSMSys.Web.Utility;
using CSMSys.Lib.Utility;
using CSMSys.Web.Pages.BagLoanRreport;

namespace CSMSys.Web.Pages.PartyLedger
{
    public partial class PartyLedgerReport : System.Web.UI.Page
    {
        #region Private Properties
        private string connstring = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;
        private decimal TotalDebit = (decimal)0.0;
        private decimal TotalCredit = (decimal)0.0;

        SqlConnection formCon = null;

        private int VoucherType
        {
            get
            {
                if (ViewState["VoucherType"] == null)
                    ViewState["VoucherType"] = -1;
                return (int)ViewState["VoucherType"];
            }
            set
            {
                ViewState["VoucherType"] = value;
            }
        }

        private int TransactionID
        {
            get
            {
                if (ViewState["TransactionID"] == null)
                    ViewState["TransactionID"] = -1;
                return (int)ViewState["TransactionID"];
            }
            set
            {
                ViewState["TransactionID"] = value;
            }
        }

        DaTransaction objDaTrans = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.Target = "_blank";
            MultiViewBagLoan.ActiveViewIndex = 0;
            GridViewHelper helper = new GridViewHelper(this.grvPartyLedger);
            helper.RegisterGroup("CreatedDate", true, true);
            helper.RegisterSummary("BagLoaded", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("TotalBagLoan", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("BagLoanAmount", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("TotalCarry", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("Loan", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("TotalLoan", SummaryOperation.Sum, "CreatedDate");
            helper.RegisterSummary("BagLoaded", SummaryOperation.Sum);
            helper.RegisterSummary("TotalBagLoan", SummaryOperation.Sum);
            helper.RegisterSummary("BagLoanAmount", SummaryOperation.Sum);
            helper.RegisterSummary("TotalCarry", SummaryOperation.Sum);
            helper.RegisterSummary("Loan", SummaryOperation.Sum);
            helper.RegisterSummary("TotalLoan", SummaryOperation.Sum);

            if (!IsPostBack)
            {

                DateTime parseFromDate = Convert.ToDateTime(Request.QueryString["FD"]);
                string fromDate = parseFromDate.ToShortDateString();
                DateTime parseToDate = Convert.ToDateTime(Request.QueryString["TD"]);
                string toDate = parseToDate.ToShortDateString();
                lblFromDate.Text = fromDate;
                lblToDate.Text = toDate;
                //VoucherType = string.IsNullOrEmpty(Request.QueryString["Voucher"]) ? 1 : int.Parse(Request.QueryString["Voucher"]);
                //TransactionID = string.IsNullOrEmpty(Request.QueryString["TMID"]) ? 0 : int.Parse(Request.QueryString["TMID"]);
                //LoadValue()
                //LoadDataGrid(fromDate, toDate);
            }
        }
        
        #region Method for Load
        public void LoadDataGrid(string fromDate, string toDate)
            {

//                string query = @"SELECT   sbl.BagLoanID, sbl.PartyID, sbl.BagNumber, sbl.Amount, sbl.Remarks,  CONVERT(VARCHAR(10),sbl.CreatedDate, 103) AS cdates, 
//                                                                ip.PartyType, ip.PartyCode, ip.PartyName, ip.ContactNo    
//                                                                FROM         dbo.SRVBagLoan AS sbl INNER JOIN
//                                                                dbo.INVParty AS ip ON ip.PartyID = sbl.PartyID
//                                                            where  (sbl.CreatedDate>='" + fromDate + "' and sbl.CreatedDate<= '" + toDate + "')    ORDER BY sbl.BagLoanID DESC";
//                dsBagloans.SelectCommand = query;
//                grvBagloanRegister.DataBind();

            }
        #endregion

        protected void grvPartyLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPartyLedger.PageIndex = e.NewPageIndex;
            grvPartyLedger.DataBind();
        }



       }
}