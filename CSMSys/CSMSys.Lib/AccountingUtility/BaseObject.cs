using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingUtility
{
    public delegate void Refreshing();
    public delegate void Refreshing1(DateTime dt);

   public class BaseObject
    {
        public BaseObject()
        {

            _dModifiedDate = DateTime.Now;
            _nCompanyID = 0;
            _nUserID = 0;
        }

        #region Fields
        private DateTime _dModifiedDate;
        private int _nCompanyID;
        private int _nUserID;
        #endregion

        #region Properties
        public int UserID
        {
            get { return _nUserID; }
            set { _nUserID = value; }
        }

        public int CompanyID
        {
            get { return _nCompanyID; }
            set { _nCompanyID = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _dModifiedDate; }
            set { _dModifiedDate = value; }
        }
        #endregion
    }
}
