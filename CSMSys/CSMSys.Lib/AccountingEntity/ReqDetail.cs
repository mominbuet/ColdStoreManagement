using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ReqDetail
    {
        public ReqDetail() { }
        #region Fields
        private int numReqDID = 0;
        private int numReqMID = 0;
        private int numItemID = 0;
        private double numReqQty = 0;
        private int numUnitID = 0;
        private string strRemarks = "";
        private string strColorCode = "";
        private string strLabdip = "";
        private int nCountID;
        private int nSizeID;
        private int nColorID;
        #endregion
        #region Properties
        public int ReqDID
        {
            get { return numReqDID; }
            set { numReqDID = value; }
        }
        public int ReqMID
        {
            get { return numReqMID; }
            set { numReqMID = value; }
        }
        public int ItemID
        {
            get { return numItemID; }
            set { numItemID = value; }
        }
        public double ReqQty
        {
            get { return numReqQty; }
            set { numReqQty = value; }
        }
        public int UnitID
        {
            get { return numUnitID; }
            set { numUnitID = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public string ColorCode
        {
            get { return strColorCode; }
            set { strColorCode = value; }
        }
        public string Labdip
        {
            get { return strLabdip; }
            set { strLabdip = value; }
        }
        public int CountID
        {
            get { return nCountID; }
            set { nCountID = value; }
        }
        public int SizeID
        {
            get { return nSizeID; }
            set { nSizeID = value; }
        }
        public int ColorID
        {
            get { return nColorID; }
            set { nColorID = value; }
        }
        #endregion
    }
}
