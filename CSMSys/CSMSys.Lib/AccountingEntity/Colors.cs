using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Colors 
    {
        public Colors() { }

        #region Fields
        private int intColorsID = 0;
        private string strColorsName = "";
        private int nCompanyID = 0;
        private int nUserID = 0;
        #endregion

        #region Properties

        public int ColorsID 
        {
            get { return intColorsID; }
            set { intColorsID = value; }
        }

        public string ColorsName
        {
            get { return strColorsName; }
            set { strColorsName = value; }
        }
        public int CompanyID
        {
            get { return nCompanyID; }
            set { nCompanyID = value; }
        }
        public int UserID
        {
            get { return nUserID; }
            set { nUserID = value; }
        }

        #endregion
    }
}
