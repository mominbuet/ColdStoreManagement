using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Purchases_Invoice : BaseObject
    {
        public Purchases_Invoice() : base() { }
        #region Fields
        private int numInvoiceID;
        private string strInvoiceType;
        private string strInvoiceNo;
        private DateTime dtInvoiceDate;
        private int numSupplierAccountID;
        private int numPurchasesAccountID;
        private int numPurchasesAccount2ID;
        private double dbTransAmmount;
        private int numTransrefID;
        private string strRemarks;
        private int numStockRefID;
        private int numCurrencyID = 0;
        private double dbRate;
        #endregion
        #region Properties
        public int InvoiceID
        {
            get { return numInvoiceID; }
            set { numInvoiceID = value; }

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
        public DateTime InvoiceDate
        {
            get { return dtInvoiceDate; }
            set { dtInvoiceDate = value; }
        }
        public int SupplierAccountID
        {
            get { return numSupplierAccountID; }
            set { numSupplierAccountID = value; }
        }
        public int PurchasesAccountID
        {
            get { return numPurchasesAccountID; }
            set { numPurchasesAccountID = value; }
        }
        public int PurchasesAccount2ID
        {
            get { return numPurchasesAccount2ID; }
            set { numPurchasesAccount2ID = value; }
        }
        public double TransAmmount
        {
            get { return dbTransAmmount; }
            set { dbTransAmmount = value; }
        }
        public int TransRefID
        {
            get { return numTransrefID; }
            set { numTransrefID = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public int StockRefID
        {
            get { return numStockRefID; }
            set { numStockRefID = value; }
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
