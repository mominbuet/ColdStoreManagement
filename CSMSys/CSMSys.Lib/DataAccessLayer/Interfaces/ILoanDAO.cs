using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSMSys.Lib.Model;
namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface ILoanDAO : IRepository<SRVLoanDisburse>
    {
        int getNextCaseNo();
        IList<SRVLoanDisburse> getAllLoansByParty(int partyid);
        IList<SRVLoanDisburse> getAllLoansLikeSerialID(int serialid);
        IList<SRVRegistration> getAllRequisitionByParty(int partyid, string req);
    }
}
