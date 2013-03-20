using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Factory
    {
        public Factory()
        {
        }
        #region Fields
        private int intFactoryID = 0;
        private string strFactoryName = "";
        private string strAddress = "";
        private int intCustomerID = 0;
        #endregion

        #region Properties

        public int FactoryID
        {
            get { return intFactoryID; }
            set { intFactoryID = value; }
        }

        public string FactoryName
        {
            get { return strFactoryName; }
            set { strFactoryName = value; }
        }

        public string Address
        {
            get { return strAddress; }
            set { strAddress = value; }
        }

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        #endregion
    }
}
