using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class PurchaseReturn : BaseObject
    {
        public PurchaseReturn() : base() { }
        #region Fields
        private int numReturnMID;
        private int numPurchaseInvoiceID;
        private string strInvoiceNo;
        private DateTime dtReturnDate;
        private int numSupplierAccountID;
        private int numPurchaseAccountID;
        private double dbReturnAmount;
        private int numTransRefID;
        private int numStockRefID;
        private string strRemarks;
        private int numCurrencyID = 0;
        private double dbRate;
        #endregion
        #region Properties
        public int ReturnMID
        {
            get { return numReturnMID; }
            set { numReturnMID = value; }
        }
        public int PurchaseInvoiceID
        {
            get { return numPurchaseInvoiceID; }
            set { numPurchaseInvoiceID = value; }
        }
        public string InvoiceNo
        {
            get { return strInvoiceNo; }
            set { strInvoiceNo = value; }
        }
        public DateTime ReturnDate
        {
            get { return dtReturnDate; }
            set { dtReturnDate = value; }
        }
        public int SupplierAccountID
        {
            get { return numSupplierAccountID; }
            set { numSupplierAccountID = value; }
        }
        public int PurchaseAccountID
        {
            get { return numPurchaseAccountID; }
            set { numPurchaseAccountID = value; }
        }
        public double ReturnAmount
        {
            get { return dbReturnAmount; }
            set { dbReturnAmount = value; }
        }
        public int TransRefID
        {
            get { return numTransRefID; }
            set { numTransRefID = value; }
        }
        public int StockRefID
        {
            get { return numStockRefID; }
            set { numStockRefID = value; }

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
