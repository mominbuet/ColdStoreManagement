using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ExportBill : BaseObject
    {
        public ExportBill() : base() { }
        #region Fields
        private int numBillID;
        
        private string strBillType;
        private string strTypeNo;
        
        private int numLCID;
        private double dblLCValue;
        private DateTime dtBillDate;
        private double dblBillAmount;
        
        private string strLoanNo;
        private double dblRealisedAmt;
        private double dblRealisedLoss;
        private DateTime dtRealisedDate;
        private string strremarks;
        private DateTime dtAcceptDate;
        private DateTime dtMaturityDate;
        private int numTransRefID=-1;
        private int numRealTransRefID=-1;
        private DateTime dtPurchaseDate;
        private double dblPurchaseAmt;
        private double dblLCQty;
        private int numUnitID;
        private int numCurrencyID;
        private double dblRate;
        private string strRealizedRefIDs="";
        #endregion

        #region Properties
        public int BillID
        {
            get { return numBillID; }
            set { numBillID = value; }
        }
       
        public string BillType
        {
            get { return strBillType; }
            set { strBillType = value; }
        }
        public string BillNo
        {
            get { return strTypeNo; }
            set { strTypeNo = value; }
        }
        
        public int LCID
        {
            get { return numLCID; }
            set { numLCID = value; }
        }
        public double LCValue
        {
            get { return dblLCValue; }
            set { dblLCValue = value; }
        }
        public DateTime BillDate
        {
            get { return dtBillDate; }
            set { dtBillDate = value; }
        }
        public double BillAmount
        {
            get { return dblBillAmount; }
            set { dblBillAmount = value; }
        }
       
        public string LoanNo
        {
            get { return strLoanNo; }
            set { strLoanNo = value; }
        }
        public double RealisedAmount
        {
            get { return dblRealisedAmt; }
            set { dblRealisedAmt = value; }
        }
        public double RealisedLoss
        {
            get { return dblRealisedLoss; }
            set { dblRealisedLoss = value; }
        }
        public DateTime RealisedDate
        {
            get { return dtRealisedDate; }
            set { dtRealisedDate = value; }
        }
        public string remarks
        {
            get { return strremarks; }
            set { strremarks = value; }
        }
        public DateTime AcceptDate
        {
            get { return dtAcceptDate; }
            set { dtAcceptDate = value; }
        }
        public DateTime MaturityDate
        {
            get { return dtMaturityDate; }
            set { dtMaturityDate = value; }
        }

        public int TransactionRefID
        {
            get { return numTransRefID; }
            set { numTransRefID = value; }
        }
        public int RealizedTransactionRefID
        {
            get { return numRealTransRefID; }
            set { numRealTransRefID = value; }
        }
        public string RealizedTransactionRefIDs
        {
            get { return strRealizedRefIDs; }
            set { strRealizedRefIDs = value; }
        }
        public DateTime PurchaseDate
        {
            get { return dtPurchaseDate; }
            set { dtPurchaseDate=value; }
        }
        public double PurchaseAmount
        {
            get { return dblPurchaseAmt; }
            set { dblPurchaseAmt = value; }
        }
        public double LCQuantity
        {
            get { return dblLCQty; }
            set { dblLCQty = value; }
        }
        public int UnitID
        {
            get { return numUnitID; }
            set { numUnitID = value; }
        }
        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID = value; }
        }
        public double CurrencyRate
        {
            get { return dblRate; }
            set { dblRate = value; }
        }
        #endregion
    }
}
