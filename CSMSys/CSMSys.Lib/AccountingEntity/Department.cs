using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Department
    {
        public Department() { }
        #region Fields
        private int nDeptID;
        private string strDeptName;
        #endregion

        #region Properties
        public int DeptID
        {
            get { return nDeptID; }
            set { nDeptID = value; }
        }
        public string DeptName
        {
            get { return strDeptName; }
            set { strDeptName = value; }
        }
        #endregion
    }
}
