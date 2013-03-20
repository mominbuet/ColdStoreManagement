using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CSMSys.Lib.Model;
namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface ILoanCollectionDAO : IRepository<SRVLoanCollection>
    {
        IList<SRVLoanCollection> getAllLoansByParty(int partyid);
        IList<SRVLoanCollection> getAllLoansLikeSerialID(int serialid);
    }
}
