using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CSMSys.Lib.AccountingEntity
{
   public class BusinessSubType :BaseObject
    {
        public BusinessSubType() : base() { }


       #region Fields
       private int numBusinessSubTypeID;
       private string strName;
       private int numBusinessTypeID;
       #endregion


       #region Properties
       public int BusinessSubTypeID
       {
           get { return numBusinessSubTypeID; }
           set { numBusinessSubTypeID = value; }
       }

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
