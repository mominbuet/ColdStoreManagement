﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IBagLoanDAO : IRepository<SRVBagLoan>
    {
       int getAllBagLoansByparty(int pid);
       // IList<INVBagFair> SearchItem(string typeID, string typeName);
    }
}
