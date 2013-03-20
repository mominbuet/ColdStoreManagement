using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class LC_Detail
    {
       public LC_Detail() { }
        #region Fields
       private int numLCDetailID;
       private int numLCID;
       private int numPIID;
       
        #endregion
        #region Properties
       public int LCDetailID
       {
           get { return numLCDetailID; }
           set { numLCDetailID = value; }
       }
       public int LCID
       {
           get { return numLCID; }
           set { numLCID = value; }
       }
       public int PIID
       {
           get { return numPIID; }
           set { numPIID = value; }
       }
      
        #endregion

    }
}
