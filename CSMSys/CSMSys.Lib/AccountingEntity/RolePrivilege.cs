using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class RolePrivilege
    {
        public RolePrivilege() { }

        #region Fields
        private string strRole;
        private string strFriendlyName;
        private int ynCanEdit;
        private int ynCanDelete;
        private int ynCanAdd;
        private int ynCanView;
        #endregion

        #region Properties
        public string Role
        {
            get { return strRole; }
            set { strRole = value; }
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

        #endregion
    }
}
