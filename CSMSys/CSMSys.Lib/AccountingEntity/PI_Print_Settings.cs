using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
  public  class PI_Print_Settings
    
  {
      public PI_Print_Settings(){ }
      #region Fields
      private int numItemID;
      private string strTerms;
      private string strCon;
      #endregion
      #region Properties
      public int ItemID
      {
          get { return numItemID; }
          set { numItemID = value; }
      }
      public string Terms
      {
          get { return strTerms; }
          set { strTerms = value; }
      }
      public string Condition
      {
          get { return strCon; }
          set { strCon = value; }
      }
      #endregion

  }
}
