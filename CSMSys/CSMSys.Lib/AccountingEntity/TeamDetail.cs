using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class TeamDetail
    {
        public TeamDetail()
        {

        }
        #region Fields
        private int MemberID = 0;
        private int TeamID = 0;
        private string MemberName = "";
        private int DesignationID = 0;
        private int DeptID = 0;
        private string ContactNo = "";
        private string Remarks = "";
        #endregion

        #region Properties
        public int intMemberID
        {
            get { return MemberID; }
            set { MemberID = value; }
        }

        public int intTeamID
        {
            get { return TeamID; }
            set { TeamID = value; }
        }

        public string strMemberName
        {
            get { return MemberName; }
            set { MemberName = value; }
        }

        public int intDesignationID
        {
            get { return DesignationID; }
            set { DesignationID = value; }
        }

        public int intDeptID
        {
            get { return DeptID; }
            set { DeptID = value; }
        }

        public string strContactNo
        {
            get { return ContactNo; }
            set { ContactNo = value; }
        }

        public string strRemarks
        {
            get { return Remarks; }
            set { Remarks = value; }
        }
        #endregion
    }
}
