using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class PI_Print_Setting
    {
        public PI_Print_Setting() { }
        
        #region Fields
        /// <summary>
        /// 
        /// </summary>
//private int intSettingID = 0;
        //private string strTermAndCondition = "";
        //private string strItems = "";
        #endregion

        #region Properties
        public int SettingID
        {
            get { return SettingID; }
            set { SettingID = value; }
        }

        public string TermAndCondition
        {
            get { return TermAndCondition; }
            set { TermAndCondition = value; }
        }

        public string Items
        {
            get { return Items; }
            set { Items = value; }
        }
        #endregion
    }
}
