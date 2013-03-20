using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Sales_Invoice : BaseObject
    {
        public Sales_Invoice() : base() { }
        #region Fields
        private int nInvoiceID = 0;
        private string strInvoiceType = "";
        private string strInvoiceNo = "";
        private string strChalanNo = "";
        private DateTime dtInvoiceDate = DateTime.Now;
        private int nCustomerAccount = 0;
        private int nSalesAccount = 0;
        private double dblSalesAmount = 0.0;
        private double dblDiscountRate = 0.0;
        private double dblDiscountAmount = 0.0;
        private double dblTransAmount = 0.0;
        private int nTransRefID = 0;
        private int nStockRefID = 0;
        private string strRemarks = "";
        private int numCurrencyID = 0;
        private double dbRate;
        #endregion
        #region Properties
        public int InvoiceID
        {
            get { return nInvoiceID; }
            set { nInvoiceID = value; }
        }
        public string InvoiceType
        {
            get { return strInvoiceType; }
            set { strInvoiceType = value; }
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
        public DateTime InvoiceDate
        {
            get { return dtInvoiceDate; }
            set { dtInvoiceDate = value; }
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
        public double SalesAmount
        {
            get { return dblSalesAmount; }
            set { dblSalesAmount = value; }
        }
        public double DiscountRate
        {
            get { return dblDiscountRate; }
            set { dblDiscountRate = value; }
        }
        public double DiscountAmount
        {
            get { return dblDiscountAmount; }
            set { dblDiscountAmount = value; }
        }
        public double TransAmount
        {
            get { return dblTransAmount; }
            set { dblTransAmount = value; }
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
        #endregion
    }
}
