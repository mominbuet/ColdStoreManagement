using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CSMSys.Lib.AccountingEntity
{
   public class BusinessType : BaseObject
    {
       public BusinessType() :base() { }


        #region Fields
           private int numBusinessTypeID;
           private string strName;
        #endregion


        #region Properties
           public int BusinessTypeID
           {
               get { return numBusinessTypeID; }
               set { numBusinessTypeID = value; }
           }
           public string Name
           {
               get { return strName; }
               set { strName = value; }
           }
        #endregion
    }
}
