using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface ISerialDAO : IRepository<INVStockSerial>
    {
        long getNextSerialNo();
        IList<INVStockSerial> SearchSerialByNo(string serialNo);
        IList<INVStockSerial> SearchSerialByParty(int partyid);
        IList<INVStockSerial> SearchSerial(string serialNo, float bag);
        long GetSumByParty(string sql);
    }

}
