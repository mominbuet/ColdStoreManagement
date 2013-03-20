using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class SalesReturn
    {
        public SalesReturn() { }
        #region Fields
        private int nReturnMID;
        private int nSalesInvoiceID;
        private string strInvoiceNo;
        private string strChalanNo;
        private DateTime dtReturnDate;
        private int nCustomerAccount;
        private int nSalesAccount;
        private double dblReturnAmount;
        private int nTransRefID;
        private int nStockRefID;
        private int numCurrencyID = 0;
        private string strRemarks;
        private double dbRate;
        /*
        private int nCompanyID;
        private int nUserID;
        private DateTime dtModifiedDate;
        */
        #endregion

        #region Properties
        public int ReturnMID
        {
            get { return nReturnMID; }
            set { nReturnMID = value; }
        }
        public int SalesInvoiceID
        {
            get { return nSalesInvoiceID; }
            set { nSalesInvoiceID = value; }
        }
        public string InvoiceNo
        {
            get { return strInvoiceNo; }
            set { strInvoiceNo = value; }
        }
        public string ChalanNo
        {
            get { return strChalanNo; }
            set { strChalanNo = value; }
        }
        public DateTime ReturnDate
        {
            get { return dtReturnDate; }
            set { dtReturnDate = value; }
        }
        public int CustomerAccount
        {
            get { return nCustomerAccount; }
            set { nCustomerAccount = value; }
        }
        public int SalesAccount
        {
            get { return nSalesAccount; }
            set { nSalesAccount = value; }
        }
        public double ReturnAmount
        {
            get { return dblReturnAmount; }
            set { dblReturnAmount = value; }
        }
        public int TransRefID
        {
            get { return nTransRefID; }
            set { nTransRefID = value; }
        }
        public int StockRefID
        {
            get { return nStockRefID; }
            set { nStockRefID = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID = value; }
        }
        public double Rate
        {
            get { return dbRate; }
            set { dbRate = value; }
        }

        /*
        public int CompanyID
        {
            get { return nCompanyID; }
            set { nCompanyID = value; }
        }
        public int UserID
        {
            get { return nUserID; }
            set { nUserID = value; }
        }
        public DateTime ModifiedDate
        {
            get { return dtModifiedDate; }
            set { dtModifiedDate = value; }
        }
        */
        #endregion

    }
}
