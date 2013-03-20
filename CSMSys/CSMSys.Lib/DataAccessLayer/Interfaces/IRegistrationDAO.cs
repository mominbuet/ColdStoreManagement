using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IRegistrationDAO : IRepository<SRVRegistration>
    {
        SRVRegistration SearchRegistrationByNo(string serialNo);
        SRVRegistration SearchSRVBySerialParty(string serialNo, int partyid);
        IList<SRVRegistration> SearchRegistration(string serialNo, float bag);
        IList<SRVRegistration> SearchRegForTotalLoan(int partyid, string requisitioned);
    }
}
