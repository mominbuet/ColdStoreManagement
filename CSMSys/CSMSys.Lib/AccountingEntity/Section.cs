using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Section
    {
        public Section() { }
        #region Fields
        private int nSectionID;
        private string strName;
        private string strDescription;
        /*
        private int nCompanyID;
        private int nUserID;
        private DateTime dtModifiedDate;
         * */
        #endregion

        #region Properties
        public int SectionID
        {
            get { return nSectionID; }
            set { nSectionID = value; }
        }
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }
        /*
        public int CompanyID
        {
            get { return nCompanyID; }
            set { nCompanyID = value; }
        }
        public int UserID
        {
            get { return nUserID; }
            set { nUserID = value; }
        }
        public DateTime ModifiedDate
        {
            get { return dtModifiedDate; }
            set { dtModifiedDate = value; }
        }
         * */
        #endregion
    }
}
