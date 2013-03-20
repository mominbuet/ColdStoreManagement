using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingEntity
{
   public class Designation : BaseObject
    {
       public Designation(): base()
       {
           DesignationID = 0;
           DesignationName = "";
           PayScaleID = 0;
       }

        #region Fields
       private int _numDesgID;
       private string _strDesgName;
       private int _numPayScaleID;
        #endregion

        #region Properties
       public int DesignationID
       {
           get { return _numDesgID; }
           set { _numDesgID = value; }
       }
       public string DesignationName
       {
           get { return _strDesgName; }
           set { _strDesgName = value; }
       }
       public int PayScaleID
       {
           get { return _numPayScaleID; }
           set { _numPayScaleID = value; }
       }
        #endregion
    }
}
