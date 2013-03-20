using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class BillAdjust
    {
        public BillAdjust() { }


        #region Fields
        private int numBillAdjustID;
        private int numBillsID;
        private DateTime dtAdjustDate;
        private double dblAdjustAmt;
        private int numAdjustAccID;
        private int numAdjustMethodID;
        private int numAdjustRefLCID;
        private string strAdjustRefNo;
        private string strRemarks;
        private int numTransRefID;
        private string strTransVoucherNo;
        private DateTime dtAcceptDate;
        private DateTime dtMaturityDate;
        private int numBillAccID;
        private int numCurrencyID;
        private double dblRate;
        #endregion

        #region Properties
        public int BillAdjustID
        {
            get { return numBillAdjustID; }
            set { numBillAdjustID = value; }
        }
        public int BillsID
        {
            get { return numBillsID; }
            set { numBillsID = value; }
        }
        public DateTime AdjustDate
        {
            get { return dtAdjustDate; }
            set { dtAdjustDate = value; }
        }
        public double AdjustAmount
        {
            get { return dblAdjustAmt; }
            set { dblAdjustAmt = value; }
        }
        public int AdjustAccountID
        {
            get { return numAdjustAccID; }
            set { numAdjustAccID = value; }
        }
        public int AdjustMethodID
        {
            get { return numAdjustMethodID; }
            set { numAdjustMethodID = value; }
        }
        public int AdjustRefLCID
        {
            get { return numAdjustRefLCID; }
            set { numAdjustRefLCID = value; }
        }
        public string AdjustRefNo
        {
            get { return strAdjustRefNo; }
            set { strAdjustRefNo = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        public int TransRefID
        {
            get { return numTransRefID; }
            set { numTransRefID = value; }
        }
        public string TransVoucherNo
        {
            get { return strTransVoucherNo; }
            set { strTransVoucherNo = value; }
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

        public int BillAccountID
        {
            get { return numBillAccID; }
            set { numBillAccID=value; }
        }
        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID=value; }
        }
        public double CurrencyRate
        {
            get { return dblRate; }
            set { dblRate=value; }
        }
        #endregion

    }
}
