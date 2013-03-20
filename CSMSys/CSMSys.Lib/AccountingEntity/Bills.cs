using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Bills : BaseObject
    {
        public Bills() : base() { }

        #region Fields
        private int numBillsID;
        private string strBillsType;
        private int numCustSuppAccID;
        private int numInvoiceID;
        private string strInvoiceNo;
        private double dblBillAmt;
        private DateTime dtBillDate;
        private double dblDueAmt;
        private string strRemarks="";
        private string strModule = "";
        private int numBillForAccID;
        private int numBillForAcc2ID;
        private int numCurrencyID;
        private double dblRate;
        private int numLcID;
        private double numBillQty;
        private int numTransRefID;
        #endregion

        #region Properties
        public int BillsID
        {
            get { return numBillsID; }
            set { numBillsID = value; }
        }
        public string BillsType
        {
            get { return strBillsType; }
            set { strBillsType=value; }
        }
        public int CustomerSupplierAccountID
        {
            get { return numCustSuppAccID; }
            set { numCustSuppAccID = value; }
        }
        public int RefInvoiceID
        {
            get { return numInvoiceID; }
            set { numInvoiceID = value; }
        }
        public string RefInvoiceNo
        {
            get { return strInvoiceNo; }
            set { strInvoiceNo = value; }
        }
        public double BillAmount
        {
            get { return dblBillAmt; }
            set { dblBillAmt=value; }
        }
        public DateTime BillDate
        {
            get { return dtBillDate; }
            set { dtBillDate = value; }
        }
        public double DueAmount
        {
            get { return dblDueAmt; }
            set { dblDueAmt=value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks=value; }
        }
        public string Module
        {
            get { return strModule; }
            set { strModule = value; }
        }

        public int BillForAccountID
        {
            get { return numBillForAccID; }
            set { numBillForAccID = value; }
        }
        public int BillForAccount2ID
        {
            get { return numBillForAcc2ID; }
            set { numBillForAcc2ID = value; }
        }
        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID=value; }
        }
        public double CurrencyRate
        {
            get { return dblRate; }
            set { dblRate = value; }
        }
        public int LCID
        {
            get { return numLcID; }
            set { numLcID = value; }
        }

        public double BillQuantity
        {
            get { return numBillQty; }
            set { numBillQty = value; }
        }
        public int TransRefID
        {
            get { return numTransRefID; }
            set { numTransRefID = value; }
        }
        #endregion
    }
}
