using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ReqMaster : BaseObject
    {
        public ReqMaster() : base() { }
        #region Fields
        private int numReqMID = 0;
        private string strReqNo = "";
        private DateTime dtReqDate = DateTime.Now;
        private int nReqSectionID = 0;
        private string strReqBy = "";
        private string strRemarks = "";
        #endregion
        #region Properties
        public int ReqMID
        {
            get { return numReqMID; }
            set { numReqMID = value; }
        }
        public string ReqNo
        {
            get { return strReqNo; }
            set { strReqNo = value; }
        }
        public DateTime ReqDate
        {
            get { return dtReqDate; }
            set { dtReqDate = value; }
        }
        public int ReqSectionID
        {
            get { return nReqSectionID; }
            set { nReqSectionID = value; }
        }
        public string ReqBy
        {
            get { return strReqBy; }
            set { strReqBy = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        #endregion
    }
}
