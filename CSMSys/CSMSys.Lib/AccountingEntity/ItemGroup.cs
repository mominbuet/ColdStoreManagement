using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ItemGroup : BaseObject
    {
        public ItemGroup() : base() { }
        #region Fields
        private int numItemGroupID;
        private string strGroupName;
        private int numParentGroupID;
        private string strGroupCode;
        private int numGroupDepth;
        #endregion

        #region Properties
        public int ItemGroupID
        {
            get { return numItemGroupID; }
            set { numItemGroupID = value; }
        }
        public string GroupName
        {
            get { return strGroupName; }
            set { strGroupName = value; }
        }
        public int ParentGroupID
        {
            get { return numParentGroupID; }
            set { numParentGroupID = value; }
        }
        public string GroupCode
        {
            get { return strGroupCode; }
            set { strGroupCode = value; }
        }
        public int GroupDepth
        {
            get { return numGroupDepth; }
            set { numGroupDepth = value; }
        }
        #endregion



    }
}

