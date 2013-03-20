using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class StoreInOutType
    {
       public StoreInOutType() { }

       private int numInOutTypeID;
       private string strInOutType;
       private string strVoucherPrefix;
       private string strSelectionMode;

       public int InOutTypeID
       {
           get { return numInOutTypeID; }
           set { numInOutTypeID = value; }
       }
       public string InOutType
       {
           get { return strInOutType; }
           set { strInOutType = value; }
       }
       public string VoucherPreFix
       {
           get { return strVoucherPrefix; }
           set { strVoucherPrefix = value; }
       }
       public string SelectionMode
       {
           get { return strSelectionMode; }
           set { strSelectionMode = value; }
       }
    }
}
