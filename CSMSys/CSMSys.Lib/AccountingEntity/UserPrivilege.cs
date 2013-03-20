using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class UserPrivilege
    {
        public UserPrivilege() { }

        #region Fields
        private int numUserID;
        private string strFriendlyName;
        private int ynCanEdit;
        private int ynCanDelete;
        private int ynCanAdd;
        private int ynCanView;
        private bool bIsEdit;
        private int nCompanyID;
        #endregion

        #region Properties
        public new int UserID
        {
            get { return numUserID; }
            set { numUserID = value; }
        }
        public string FriendlyName
        {
            get { return strFriendlyName; }
            set { strFriendlyName = value; }
        }

        public int CanEdit
        {
            get { return ynCanEdit; }
            set { ynCanEdit = value; }
        }
        public int CanDelete
        {
            get { return ynCanDelete; }
            set { ynCanDelete = value; }
        }
        public int CanAdd
        {
            get { return ynCanAdd; }
            set { ynCanAdd = value; }
        }
        public int CanView
        {
            get { return ynCanView; }
            set { ynCanView = value; }
        }
        public bool IsEdit
        {
            get { return bIsEdit; }
            set { bIsEdit = value; }
        }
        public int CompanyID
        {
            get { return nCompanyID; }
            set { nCompanyID = value; }
        }
        #endregion
    }
}
