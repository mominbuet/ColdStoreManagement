using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class LedgerType
    {
        public LedgerType() { }
        private int numLedgerTypeID;
        private string strLedgerType;

        public int LedgerTypeID
        {
            get {return  numLedgerTypeID; }
            set { numLedgerTypeID=value; }
        }
         public string LedgerTypeName
         {
             get { return strLedgerType; }
             set { strLedgerType=value; }
         }
    }
}
