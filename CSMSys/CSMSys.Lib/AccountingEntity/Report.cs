using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Report
    {
        public Report() { }
        #region Fields
        private int numSI;
        private string strReportName;
        private string strFriendlyName;
        private string strRbName;
        #endregion
        #region Properties
        public int SI
        {
            get { return numSI; }
            set { numSI = value; }
        }
        public string ReportName
        {
            get { return strReportName; }
            set { strReportName = value; }
        }

        public string FriendlyName
        {
            get { return strFriendlyName; }
            set { strFriendlyName = value; }
        }
        public string RbName
        {
            get { return strRbName; }
            set { strRbName = value; }
        }
        #endregion
    }
}
