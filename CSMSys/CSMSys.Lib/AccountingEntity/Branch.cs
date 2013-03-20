using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Branch
    {
        public Branch() { }
        #region fields
        private int nBranchID;
        private string strBranchName;
        private string strAddress;
        #endregion

        #region Properties
        public int BranchID
        {
            get { return nBranchID; }
            set { nBranchID = value; }
        }
        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }
        #endregion

    }
}
