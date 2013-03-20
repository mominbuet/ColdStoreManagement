using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class LCAcceptance
    {
        public LCAcceptance() { }
        #region  Fields
        private int numSlNo;
        private int numLCID;
        private DateTime dtacceptDate;
        private double dblacceptQty;
        private double dblacceptValue;
        private DateTime dtActualShipmentDate;
        private DateTime dtMaturityDate;
        private DateTime dtPaidDate;
        private string strremarks;
        #endregion

        #region Properties
        public int SlNo
        {
            get { return numSlNo; }
            set { numSlNo = value; }
        }
        public int LCID
        {
            get { return numLCID; }
            set { numLCID = value; }
        }
        public DateTime acceptDate
        {
            get { return dtacceptDate; }
            set { dtacceptDate = value; }
        }
        public double acceptQty
        {
            get { return dblacceptQty; }
            set { dblacceptQty = value; }
        }
        public double acceptValue
        {
            get { return dblacceptValue; }
            set { dblacceptValue = value; }
        }
        public DateTime ActualShipmentDate
        {
            get { return dtActualShipmentDate; }
            set { dtActualShipmentDate = value; }
        }
        public DateTime MaturityDate
        {
            get { return dtMaturityDate; }
            set { dtMaturityDate = value; }
        }
        public DateTime PaidDate
        {
            get { return dtPaidDate; }
            set { dtPaidDate = value; }
        }
        public string remarks
        {
            get { return strremarks; }
            set { strremarks = value; }
        }
        #endregion
    }
}
