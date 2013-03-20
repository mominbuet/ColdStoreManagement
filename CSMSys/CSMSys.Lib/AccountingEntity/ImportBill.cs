using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ImportBill : BaseObject
    {
        public ImportBill() : base() { }
        #region Fields
        private int numBillID;
        private string strBillNo;
        private DateTime dtBillDate;
        private string strBillRefNo;
        private DateTime dtFOBdate;
        private int numLCID;
        private double dblLCvalue;

        private double dblBillAmt;
        private double dblDueAmt;
        #endregion
        #region Properties
        public int BillID
        {
            get { return numBillID; }
            set { numBillID=value; }
        }
        public string BillNo
        {
            get { return strBillNo; }
            set { strBillNo=value; }
        }
        public DateTime BillDate
        {
            get { return dtBillDate; }
            set { dtBillDate=value; }
        }
        public string BillRefNo
        {
            get { return strBillRefNo; }
            set { strBillRefNo=value; }
        }
        public DateTime FOBDate
        {
            get { return dtFOBdate; }
            set { dtFOBdate=value; }
        }
        public int LCID
        {
            get { return numLCID; }
            set { numLCID=value; }
        }
        public double LCValue
        {
            get { return dblLCvalue; }
            set { dblLCvalue=value; }
        }

        public double BillAmount
        {
            get { return dblBillAmt; }
            set { dblBillAmt=value; }
        }
        public double DueAmount
        {
            get { return dblDueAmt; }
            set { dblDueAmt = value; }
        }
        #endregion
    }
}
