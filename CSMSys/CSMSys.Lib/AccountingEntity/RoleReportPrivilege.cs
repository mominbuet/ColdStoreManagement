using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class RoleReportPrivilege
    {
        public RoleReportPrivilege() { }
        #region Fields
        private string strRole;
        private string strRbName;
        private int ynCanView;
        private bool bIsEdit;
        #endregion

        #region Properties
        public new string Role
        {
            get { return strRole; }
            set { strRole = value; }
        }
        public string RbName
        {
            get { return strRbName; }
            set { strRbName = value; }
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
        #endregion
    }
}
