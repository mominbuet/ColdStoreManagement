using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IBagDAO : IRepository<INVBagFair>
    {
       // IList<INVBagFair> SearchItem(string typeID, string typeName);
    }
}
