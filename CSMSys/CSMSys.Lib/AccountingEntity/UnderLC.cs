using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class UnderLC
    {
        public UnderLC() { }
        #region Fields
        private int nSLNo;
        private int nLCID;
        private int nUnderLCID;
        private string strUnderLCNo;
        private DateTime dtUnderLCDate;
        private DateTime dtShipmentDate;
        private DateTime dtExpDate;
        #endregion

        #region Properties
        public int SLNo
        {
            get { return nSLNo; }
            set { nSLNo = value; }
        }
        public int LCID
        {
            get { return nLCID; }
            set { nLCID = value; }
        }
        public int UnderLCID
        {
            get { return nUnderLCID; }
            set { nUnderLCID = value; }
        }
        public string UnderLCNo
        {
            get { return strUnderLCNo; }
            set { strUnderLCNo = value; }
        }
        public DateTime UnderLCDate
        {
            get { return dtUnderLCDate; }
            set { dtUnderLCDate = value; }
        }
        public DateTime ShipmentDate
        {
            get { return dtShipmentDate; }
            set { dtShipmentDate = value; }
        }
        public DateTime ExpDate
        {
            get { return dtExpDate; }
            set { dtExpDate = value; }
        }
        #endregion
    }
}
