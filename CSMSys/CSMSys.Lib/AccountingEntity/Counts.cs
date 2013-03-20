using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Counts : BaseObject
    {
        public Counts() : base() { }

        #region Fields
        private int intCountsID = 0;
        private string strCountsName = "";

        #endregion

        #region Properties

        public int CountID
        {
            get { return intCountsID; }
            set { intCountsID = value; }
        }

        public string CountName
        {
            get { return strCountsName; }
            set { strCountsName = value; }
        }

        #endregion

    }
}
