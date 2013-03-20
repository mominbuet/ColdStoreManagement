using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Sizes
    {
        public Sizes()
        {
        }

        #region Fields
        private int intSizesID = 0;
        private string strSizesName = "";
        #endregion

        #region Properties

        public int SizesID
        {
            get { return intSizesID; }
            set { intSizesID = value; }
        }

        public string SizesName
        {
            get { return strSizesName; }
            set { strSizesName = value; }
        }

        #endregion
    }
}
