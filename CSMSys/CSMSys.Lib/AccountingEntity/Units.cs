using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Units 
    {
        public Units() { }

        #region Fields
        private int intUnitsID = 0;
        private string strUnitsName = "";
        #endregion

        #region Properties

        public int UnitsID
        {
            get { return intUnitsID; }
            set { intUnitsID = value; }
        }

        public string UnitsName
        {
            get { return strUnitsName; }
            set { strUnitsName = value; }
        }

        #endregion
    }
}
