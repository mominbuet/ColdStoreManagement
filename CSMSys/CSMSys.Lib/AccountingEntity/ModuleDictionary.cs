using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ModuleDictionary
    {
        public ModuleDictionary()
        { 
        }
        #region Fields
        private int numTotalPrivilege;
        private string strMenuName;
        #endregion
        #region Properties
        public int TotalPrivilege
        {
            get { return numTotalPrivilege; }
            set { numTotalPrivilege = value; }
        }
        public string MenuName
        {
            get { return strMenuName; }
            set { strMenuName = value; }
        }
        #endregion
    }
}
