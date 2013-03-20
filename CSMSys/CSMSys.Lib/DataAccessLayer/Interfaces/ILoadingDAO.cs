using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface ILoadingDAO : IRepository<INVStockLoading>
    {
        INVStockSerial GetPartyBySerial(string serialNo);
        INVStockLoading GetLoadBySerial(string serialNo);
        INVStockLoading checklocation(int chamber, int floor, int pocket);
    }
}
