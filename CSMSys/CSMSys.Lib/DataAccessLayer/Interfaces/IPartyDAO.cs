using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IPartyDAO : IRepository<INVParty>
    {
        long getnextPartyID();
        IList<INVParty> SearchPartyByCode(string partyCode);
        IList<INVParty> SearchParty(string partyCode, string partyName);
    }
}
