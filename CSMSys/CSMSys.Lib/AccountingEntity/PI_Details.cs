using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class PI_Details
    {
        public PI_Details() { }
        
        #region Fields
        private int intPIDID = 0;
        private int intPIMID = 0;
        private int intOrderID = 0;
        private double intOrderQty = 0;
        private double dbOrderValue;
     
        #endregion
        
        #region Properties
        public int PIDID
        {
            get { return intPIDID; }
            set { intPIDID = value; }
        }

        public int PIMID
        {
            get { return intPIMID; }
            set { intPIMID = value; }
        }

        public int OrderID
        {
            get { return intOrderID; }
            set { intOrderID = value; }
        }

        public double OrderQty
        {
            get { return intOrderQty; }
            set { intOrderQty = value; }
        }

        public double OrderValue
        {
            get { return dbOrderValue; }
            set { dbOrderValue = value; }
        }

        
        #endregion
    }
}
