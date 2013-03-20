using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class TeamMaster : BaseObject
    {
        public TeamMaster() : base() { }

        #region Fields
        private int TeamID = 0;
        private string TeamNo = "";
        private string TeamName = "";
        private int BranchID = 0;
        #endregion

        #region Properties

        public int intTeamID
        {
            get { return TeamID; }
            set { TeamID = value; }
        }

        public string strTeamNo
        {
            get { return TeamNo; }
            set { TeamNo = value; }
        }

        public string strTeamName
        {
            get { return TeamName; }
            set { TeamName = value; }
        }

        public int intBranchID
        {
            get { return BranchID; }
            set { BranchID = value; }
        }
        #endregion

    }
}
