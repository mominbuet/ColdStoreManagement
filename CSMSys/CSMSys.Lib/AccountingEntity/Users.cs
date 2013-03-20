using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    
            #region Users
        public class User : BaseObject
        {
            public User() : base() { }

            #region Fields
            private int numUserID;
            private string strUserName;
            private string strPassword;
            private string strConfirmPassword;
            private string strRole;

            #endregion


            #region Properties
            public new int UserID
            {
                get { return numUserID; }
                set { numUserID = value; }
            }
            public string UserName
            {
                get { return strUserName; }
                set { strUserName = value; }
            }

            public string Password
            {
                get { return strPassword; }
                set { strPassword = value; }
            }
            public string ConfirmPassword
            {
                get { return strConfirmPassword; }
                set { strConfirmPassword = value; }
            }
            public string Role
            {
                get { return strRole; }
                set { strRole = value; }
            }
            #endregion

        }
        #endregion
    
}
